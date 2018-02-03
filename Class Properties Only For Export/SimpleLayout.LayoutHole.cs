using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// The LayoutHole class is used to hold a single hole for a wall or floor panel in a layout. A Hole can be BoundaryRectangle,
    /// BoundraryEllipse or BoundaryPolygon. A hole has an offset and the description of the shape.
    /// For purposes of definition a hole has an offset and values that are X/Y as if you were looking at a 2D cartesion coordinate system.
    /// The X,Y is mapped to the panel where 0,0 is the lower left of the panel. 
    /// </summary>
    [HelpItem(eItemFlavor.TemplateSupport, "LayoutHole")]
    public partial class LayoutHole
    {
        /// <summary>
        /// X Offset of this hole. 
        /// </summary>
        [HelpProperty(SampleValue = "30", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public float OffsetX { get; set; }
        /// <summary>
        /// Y Offset of this hole. 
        /// </summary>
        [HelpProperty(SampleValue = "30", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public float OffsetY { get; set; }

        /// <summary>
        /// The type of the hole indicates whether its a rectangle, ellipse or polygon
        /// </summary>
        [HelpProperty(SampleValue = "rect", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public string HoleType { get; set; }

        /// <summary>
        /// Within a layout the outlines for the rectangle, ellipse and polygon are stored in separate arrays. This index value
        /// is the index value of the specific outline within its array
        /// </summary>
        [HelpProperty(SampleValue = "4", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public int HoleTypeIndex { get; set; }

        public LayoutHole()
        {

        }

        public static LayoutHole CopyFrom(LayoutHole oFrom)
        {
            LayoutHole oHole = new LayoutHole();
            oHole.OffsetX = oFrom.OffsetX;
            oHole.OffsetY = oFrom.OffsetY;

            oHole.HoleType = oFrom.HoleType;
            oHole.HoleTypeIndex = oFrom.HoleTypeIndex;

            return oHole;
        }
    }

}
