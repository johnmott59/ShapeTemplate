using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public partial class Point3D 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Point3D()
        {

        }

        [HelpProperty(XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue ="13.4")]
        public float X { get; set; }
        [HelpProperty(XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue = "2")]
        public float Y { get; set; }
        [HelpProperty(XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue = "-34.2")]
        public float Z { get; set; }

      
        public static Point3D CopyFrom(Point3D oFrom)
        {
            Point3D p = new Point3D();
            p.X = oFrom.X;
            p.Y = oFrom.Y;
            p.Z = oFrom.Z;

            return p;
        }
    }

}
