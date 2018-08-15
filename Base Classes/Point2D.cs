using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
{

    public partial class Point2D : ILoadAndSaveProperties
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Point2D()
        {

        }

        public Point2D(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public XElement GetProperties(string PropertyName="")
        {
            return new XElement("point2d", 
                new XAttribute("prop", PropertyName), 
                new XAttribute("x", this.X), 
                new XAttribute("y", this.Y));
        }

        public bool LoadProperties(XElement ele, out string message)
        {
            message = "OK";

            float value = 0;
            if (!Utilities.GetFloatAttribute(ele, "x", out value, out message)) return false;
            this.X = value;

            if (!Utilities.GetFloatAttribute(ele, "y", out value, out message)) return false;
            this.Y = value;

            return true;
        }

        public bool Equals(Point2D input)
        {
            return this.X == input.X && this.Y == input.Y;
        }
    }

}
