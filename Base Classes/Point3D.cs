using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    /// <summary>
    /// This class holds an X,Y and Z value representing a point in 3D space
    /// The name used for this element to the API is the field name for whatever contains it.
    /// </summary>
    [HelpItem(eItemFlavor.Data,"point3d")]
    public class Point3D : ILoadAndSaveProperties
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Point3D()
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        public Point3D(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        [HelpProperty(XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue ="13.4")]
        public float X { get; set; }
        [HelpProperty(XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue = "2")]
        public float Y { get; set; }
        [HelpProperty(XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue = "-34.2")]
        public float Z { get; set; }

        public XElement GetProperties(string PropertyName="")
        {
            return new XElement("point3d", 
                new XAttribute("prop", PropertyName), 
                new XAttribute("x", this.X), 
                new XAttribute("y", this.Y), 
                new XAttribute("z", this.Z));
        }

        public bool LoadProperties(XElement ele, out string message)
        {
            message = "OK";

            float value = 0;
            if (!Utilities.GetFloatAttribute(ele, "x", out value, out message)) return false;
            this.X = value;

            if (!Utilities.GetFloatAttribute(ele, "y", out value, out message)) return false;
            this.Y = value;

            if (!Utilities.GetFloatAttribute(ele, "z", out value, out message)) return false;
            this.Z = value;

            return true;
        }
    }

}
