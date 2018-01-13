using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    /// <summary>
    /// The BoundaryRectangle class is used to define a rectangle with a width and a height. This width and height are X and Y values;
    /// boundaries are defined in an XY plane that the viewer is facing, with X to the right, Y up and Z coming out. 
    /// </summary>
    [HelpItem(eItemFlavor.Data, "rectangle")]
    public partial class BoundaryRectangle : BoundaryRoot
    {

        public BoundaryRectangle(float Width, float Height, float zDepth = 0)
        {
            this.Width = Width;
            this.Height = Height;
            this.ZDepth = zDepth;
        }

        public override XElement GetProperties(string PropertyName = "")
        {
            return new XElement("boundary", new XAttribute("prop", PropertyName),
                new XElement("rectangle", 
                    new XAttribute(nameof(Width).ToLower(), Width), 
                    new XAttribute(nameof(Height).ToLower(), Height),
                    new XAttribute(nameof(ZDepth).ToLower(), ZDepth)
                    ));
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