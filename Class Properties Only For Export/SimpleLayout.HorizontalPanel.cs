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
    /// The HorizontalPanel class is used in a SimpleLayout to define a horizontal panel that can connect to the wall
    /// panels. The connection can be to the inside of the walls, to the center line of the walls or to the outside
    /// line of the walls.
    /// </summary>
    [HelpItem(eItemFlavor.TemplateSupport, "HorizontalPanel")]
    public partial class HorizontalPanel
    {
        public HorizontalPanel()
        {
            Height = 0;
            Thickness = 5;
            DescriptorList = new string[0];
            HoleGroupID = "";
        }
        /// <summary>
        /// Height to place this panel. Wall panels normally have a base height of 0.
        /// </summary>
        [HelpProperty(SampleValue = "40", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public float Height { get; set; }

        /// <summary>
        /// Thickness of this panel. 
        /// </summary>
        [HelpProperty(SampleValue = "4", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public float Thickness { get; set; }

        /// <summary>
        /// Descriptor list. This is a list of points that make up the horizontal panel. The descriptor has 3 components.
        /// The first component is which edge this point is associated with. The second component is the position of the point,
        /// the inside edge, the center edge and the outside edge. The third component is which point on the edge, the left or
        /// right point. Left and Right are defined relative to the front of the edge. The front of the edge is defined
        /// by its points p1 and p2. You face the front of the edge, p1 is on the left and p2 is on the right.
        /// </summary>
        [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.XElement)]
        public string[] DescriptorList { get; set; }

        /// <summary>
        /// The horizontal panel can have holes. This is the ID of the HoleGroup within the SimpleLayout that contains
        /// the hole definitions.
        /// </summary>
        [HelpProperty(SampleValue = "40", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public string HoleGroupID { get; set; }
    }
}
