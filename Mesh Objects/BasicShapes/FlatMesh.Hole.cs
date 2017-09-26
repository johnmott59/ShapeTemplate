using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// a simple panel has a front, back connecting mesh and one or more holes

namespace ShapeTemplateLib.BasicShapes
{
    public partial class FlatMesh 
    {
        /// <summary>
        /// The hole in a flat mesh describes its placement and dimension. A hole is defined by a boundary and an offset from a
        /// (0,0) position on the mesh in XY space. A mesh can have any orientation, of course, but for the purposes of placing
        /// holes the mesh is positioned to an XY plane with +Z looking towards you, and the hole is defined as an offset and
        /// boundary in that XY space
        /// </summary>
        [HelpItem(eItemFlavor.Data,"hole")]
        public class Hole : ILoadAndSaveProperties
        {
            /// <summary>
            /// The offset of a hole is where it is positioned
            /// </summary>
            [HelpProperty]
            public Point3D Offset { get; set; } = new Point3D(0, 0, 0);

            /// <summary>
            /// The boundary for a hole can be a rectangle, ellipse or line segment
            /// </summary>
            [HelpProperty]
            public BoundaryRoot Boundary { get; set; }

            /// <summary>
            /// The ID value identifies this hole to other structures that may need it. It is not required
            /// </summary>
            [HelpProperty(XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue ="Window")]
            public string ID { get; set; } = "";             // this is used to identify this hole structure with a larger shape

            public XElement GetProperties(string PropertyName="")
            {
                XElement hole = new XElement("hole",
                    new XAttribute("prop",PropertyName),
                    new XAttribute("id",this.ID));

                hole.Add(Offset.GetProperties("offset"));
                hole.Add(Boundary.GetProperties("boundary"));

                return hole;
            }

            public bool LoadProperties(XElement ele, out string message)
            {
                message = "OK";

                // Possibly get ID

                if (ele.Attribute("id") != null)
                {
                    this.ID = ele.Attribute("id").Value;
                }

                XElement xOffset = Utilities.GetNamedElementWithPropAttribute(ele, "point3d", "offset");
                if (xOffset != null)
                {
                    if (!Offset.LoadProperties(xOffset, out message)) return false;
                }

                XElement xBoundary = Utilities.GetNamedElementWithPropAttribute(ele, "boundary", "boundary");

                BoundaryRoot bound;
                if (!BoundaryRoot.LoadXElement(xBoundary, out bound, out message)) return false;
                this.Boundary = bound;

                return true;
            }
        }

     
       }
    }
