using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{

    public partial class BoundaryEllipse : BoundaryRoot
    {

        public BoundaryEllipse(float Width, float Height,float ZDepth = 0) : this()
        {
            this.Width = Width;
            this.Height = Height;
            this.ZDepth = ZDepth;
        }

        public override XElement GetProperties(string PropertyName = "")
        {
            return new XElement("boundaryellipse", new XAttribute("prop",PropertyName), 
                    new XAttribute(nameof(Width).ToLower(), Width), 
                    new XAttribute(nameof(Height).ToLower(), Height),
                    new XAttribute(nameof(ZDepth).ToLower(), ZDepth)
                );
        }
        /// <summary>
        /// This load routine is called by boundary root, the boundary element has already been processed
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public override bool LoadProperties(XElement ele, out string message)
        {
            message = "OK";

            float value = 0;
            if (!Utilities.GetFloatAttribute(ele, nameof(Width).ToLower(), out value, out message)) return false;
            Width = value;

            value = 0;
            if (!Utilities.GetFloatAttribute(ele, nameof(Height).ToLower(), out value, out message)) return false;
            Height = value;

            value = 0;
            if (!Utilities.GetFloatAttribute(ele, nameof(ZDepth).ToLower(), out value, out message)) return false;
            ZDepth = value;

            return true;
        }
    }




}