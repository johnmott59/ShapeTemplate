using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplate
{
    public class Point3D : IXElement
    {
        public Point3D(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public XElement GetXElement()
        {
            return new XElement("point", new XAttribute("x", this.X), new XAttribute("y", this.Y), new XAttribute("z", this.Z));
        }

        public XElement GetXElement(string ParentName)
        {
            return new XElement(ParentName, new XAttribute("x", this.X), new XAttribute("y", this.Y), new XAttribute("z", this.Z));
        }
    }

}
