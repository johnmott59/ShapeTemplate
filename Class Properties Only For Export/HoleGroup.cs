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
    /// The Holegroup class is used to hold an array of holes for a panel. A group of holes is identified by an ID
    /// value. Holes can be BoundaryRectangle, BoundraryEllipse or BoundaryPolygon, and a given hole group can contain
    /// an arbitrary number of each.
    /// </summary>
    [HelpItem(eItemFlavor.TemplateSupport, "HoleGroup")]
    public partial class HoleGroup
    {
        public HoleGroup()
        {
            HoleGroupID = "";
            HoleList = new Hole[0];
        }
        /// <summary>
        /// ID of this holegroup. This ID value is used to tie this hole group to a panel. Hole groups can be shared amongst different 
        /// panels in a layout.
        /// </summary>
        [HelpProperty(SampleValue = "hgFrontPanel", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public string HoleGroupID { get; set; }
        /// <summary>
        /// Array of Holes. The Hole descriptors has an offset, a type, and an index into the description of the hole specifics
        /// </summary>
        [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public Hole[] HoleList { get; set; }
    }
}
