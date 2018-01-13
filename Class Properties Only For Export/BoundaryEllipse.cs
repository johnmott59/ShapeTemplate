using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public partial class BoundaryEllipse 
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

        // The CopyFrom method is used in Javascript to convert a version of the object retrieved via JSON
        // into a full object of this type. An object retrieved via JSON.parse() will have the correct property
        // names but not be an object of this type, this will take that property-only version and create a full
        // object

        public static BoundaryEllipse CopyFrom(BoundaryEllipse oFrom)
        {
            BoundaryEllipse oEllipse = new BoundaryEllipse();
            oEllipse.Width = oFrom.Width;
            oEllipse.Height = oFrom.Height;
            oEllipse.ZDepth = oFrom.ZDepth;

            return oEllipse;
        }

    }
}