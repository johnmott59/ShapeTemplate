using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    /// <summary>
    /// This class holds an X,Y  value representing a point in 2D. One use is to define edge positions in a simple layout
    /// The name used for this element to the API is the field name for whatever contains it.
    /// </summary>
    [HelpItem(eItemFlavor.Data, "point2d")]
    public partial class Point2D 
    {
        [HelpProperty(XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue ="13.4")]
        public float X { get; set; }
        [HelpProperty(XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue = "2")]
        public float Y { get; set; }

        // The CopyFrom method is used in Javascript to convert a version of the object retrieved via JSON
        // into a full object of this type. An object retrieved via JSON.parse() will have the correct property
        // names but not be an object of this type, this will take that property-only version and create a full
        // object

        public static Point2D CopyFrom(Point2D oFrom)
        {
            Point2D oPoint = new Point2D();

            oPoint.X = oFrom.X;
            oPoint.Y = oFrom.Y;

            return oPoint;
        }

    }

}
