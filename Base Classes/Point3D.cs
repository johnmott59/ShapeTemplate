using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public class Point3D : ILoadAndSaveProperties
    {
        public Point3D()
        {

        }
        public Point3D(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public XElement GetProperties()
        {
            return new XElement("point", new XAttribute("x", this.X), new XAttribute("y", this.Y), new XAttribute("z", this.Z));
        }

        public XElement GetProperties(string ParentName)
        {
            return new XElement(ParentName, new XAttribute("x", this.X), new XAttribute("y", this.Y), new XAttribute("z", this.Z));
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
