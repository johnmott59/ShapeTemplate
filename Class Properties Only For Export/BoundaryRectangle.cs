using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public partial class BoundaryRectangle 
    {
        public BoundaryRectangle()
        {
            // Set the type in the base class, the JSON serialization will use this
            this.BoundaryType = "rectangle";
        }
        /// <summary>
        /// Width of rectangle
        /// </summary>
        [HelpProperty(SampleValue = "30", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public float Width { get; set; } = 20;

        /// <summary>
        /// Height of rectangle
        /// </summary>
        [HelpProperty(SampleValue = "30", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public float Height { get; set; } = 20;

        /// <summary>
        /// ZDepth of Rectangle when placing this shape, either as a boundary or as a hole. Using ZDepth as part of the 
        /// a boundary for a panel mesh lets you displace the mesh and create separation. Varying the Z Depth for a hole can produce
        /// interesting effects and variations on shapes but should be done with caution as it can easily cause a shape to 'break'
        /// or otherwise render as something unrecognizable.
        /// </summary>
        [HelpProperty(SampleValue = "0", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public float ZDepth { get; set; } = 0;


        // The CopyFrom method is used in Javascript to convert a version of the object retrieved via JSON
        // into a full object of this type. An object retrieved via JSON.parse() will have the correct property
        // names but not be an object of this type, this will take that property-only version and create a full
        // object

        public static BoundaryRectangle CopyFrom(BoundaryRectangle oFrom)
        {
            BoundaryRectangle oRectangle = new BoundaryRectangle();
            oRectangle.Width = oFrom.Width;
            oRectangle.Height = oFrom.Height;
            oRectangle.ZDepth = oFrom.ZDepth;

            return oRectangle;
        }

    }




}