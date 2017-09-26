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
    /// The BoundaryEllipse class is used to define an ellipse with a width and a height. This width and height are X and Y values;
    /// boundaries are defined in an XY plane that the viewer is facing, with X to the right, Y up and Z coming out. 
    /// The XML name for this node will be the property name of the class that contains it
    /// </summary>
    [HelpItem(eItemFlavor.Data, "ellipse")]
    public class BoundaryEllipse : BoundaryRoot
    {
        /// <summary>
        /// Width of Ellipse
        /// </summary>
        [HelpProperty(SampleValue ="30", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public float Width { get; set; } = 20;
        /// <summary>
        /// Height of ellipse
        /// </summary>
        [HelpProperty(SampleValue ="30", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public float Height { get; set; } = 20;
        /// <summary>
        /// ZDepth of Ellipse when placing this shape, either as a boundary or as a hole. Using ZDepth as part of the 
        /// a boundary for a panel mesh lets you displace the mesh and create separation. Varying the Z Depth for a hole can produce
        /// interesting effects and variations on shapes but should be done with caution as it can easily cause a shape to 'break'
        /// or otherwise render as something unrecognizable.
        /// </summary>
        [HelpProperty(SampleValue ="0", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public float ZDepth { get; set; } = 0;


        /// <summary>
        /// Constructor
        /// </summary>
        public BoundaryEllipse()
        {
        }

        public BoundaryEllipse(float Width, float Height,float ZDepth = 0)
        {
            this.Width = Width;
            this.Height = Height;
            this.ZDepth = ZDepth;
        }

        public override XElement GetProperties(string PropertyName = "")
        {
            return new XElement("boundary",new XAttribute("prop",PropertyName), 
                new XElement("ellipse", 
                    new XAttribute(nameof(Width).ToLower(), Width), 
                    new XAttribute(nameof(Height).ToLower(), Height),
                    new XAttribute(nameof(ZDepth).ToLower(), ZDepth)
                    )
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