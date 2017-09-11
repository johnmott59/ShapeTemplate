using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public class BoundaryRectangle : BoundaryRoot
    {
        public float Width { get; set; } = 20;
        public float Height { get; set; } = 20;

        public BoundaryRectangle() {
        }

        public BoundaryRectangle(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        public override XElement GetProperties()
        {
            return new XElement("boundary", new XElement("rectangle", new XAttribute("width", Width), new XAttribute("height", Height)));
        }

        public override bool LoadProperties(XElement ele, out string message)
        {
            message = "OK";

            float value = 0;
            if (!Utilities.GetFloatAttribute(ele, "width", out value, out message)) return false;
            Width = value;

            value = 0;
            if (!Utilities.GetFloatAttribute(ele, "height", out value, out message)) return false;
            Height = value;

            return true;
        }
    }




}