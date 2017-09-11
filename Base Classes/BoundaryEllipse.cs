using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{

    public class BoundaryEllipse : BoundaryRoot
    {
        public float Width { get; set; } = 20;
        public float Height { get; set; } = 20;
        public float ZDepth { get; set; } = 0;

        public BoundaryEllipse()
        {
        }

        public BoundaryEllipse(float Width, float Height,float ZDepth = 0)
        {
            this.Width = Width;
            this.Height = Height;
            this.ZDepth = ZDepth;
        }

        public override XElement GetProperties()
        {
            return new XElement("boundary", 
                new XElement("ellipse", 
                    new XAttribute("width", Width), 
                    new XAttribute("height", Height),
                    new XAttribute("zdepth",ZDepth)
                    )
                );
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

            value = 0;
            if (!Utilities.GetFloatAttribute(ele, "zdepth", out value, out message)) return false;
            ZDepth = value;

            return true;
        }
    }




}