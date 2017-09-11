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

        public class Hole : ILoadAndSaveProperties
        {
            public Point3D Offset { get; set; } = new Point3D(0, 0, 0);
            public BoundaryRoot Boundary { get; set; }
            public string ID { get; set; } = "";             // this is used to identify this hole structure with a larger shape

            public XElement GetProperties()
            {
                XElement hole = new XElement("hole",new XAttribute("id",this.ID));

                hole.Add(Offset.GetProperties("offset"));
                hole.Add(Boundary.GetProperties());

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

                if (ele.Element("offset") != null)
                {
                    if (!Offset.LoadProperties(ele.Element("offset"), out message)) return false;
                }

                BoundaryRoot bound;
                if (!BoundaryRoot.LoadXElement(ele.Element("boundary"), out bound, out message)) return false;
                this.Boundary = bound;

                return true;
            }
        }

     
       }
    }
