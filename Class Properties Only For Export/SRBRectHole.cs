using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// This is a descriptor for a hole in the SingleRoomBuilding template, which can be a window or a door.
    /// </summary>
    public partial class SRBRectHole
    {
        [HelpProperty(SampleValue = "30", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public bool Visible { get; set; }

        [HelpProperty(SampleValue = "30", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.XElement)]
        public BoundaryRectangle Boundary { get; set; }

        [HelpProperty(SampleValue = "30", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.XElement)]
        public Point2D Offset { get; set; }

        public SRBRectHole()
        {
            Visible = false;

            Boundary = new BoundaryRectangle();
            Boundary.Width = 0;
            Boundary.Height = 0;
            Boundary.ZDepth = 0;

            Offset = new Point2D();
            Offset.X = 0;
            Offset.Y = 0;
        }


        // The CopyFrom method is used in Javascript to convert a version of the object retrieved via JSON
        // into a full object of this type. An object retrieved via JSON.parse() will have the correct property
        // names but not be an object of this type, this will take that property-only version and create a full
        // object
        public static SRBRectHole CopyFrom(SRBRectHole oFrom)
        {
            SRBRectHole oSRB = new SRBRectHole();
            oSRB.Boundary = BoundaryRectangle.CopyFrom(oFrom.Boundary);
            oSRB.Offset = Point2D.CopyFrom(oFrom.Offset);
            oSRB.Visible = oFrom.Visible;

            return oSRB;
        }
    }
}
