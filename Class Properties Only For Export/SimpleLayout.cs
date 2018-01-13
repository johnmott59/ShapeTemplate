using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SimpleLayout 
    {
        /// <summary>
        /// The HorizontalScale describes how the layout is scaled in the plane of the layout objects; how much to stretch the 
        /// layout points before rendering them as panels
        /// </summary>
        [HelpProperty(SampleValue = "1.0", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public float HorizontalScale { get; set; } = 1;

        /// <summary>
        /// The VertialScale describes how the layout is scaled in in the vertical direction; how much to stretch the height. 
        /// </summary>
        [HelpProperty(SampleValue = "1.0", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public float VerticalScale { get; set; } = 1;

        // List of the hole groups
        [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public List<HoleGroup> HoleGroupList { get; set; } = new List<HoleGroup>();

        // The hole definitions are maintained in lists referenced by index. We do this this way so that this entire
        // structure can be serialized
        [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public List<BoundaryRectangle> BoundaryRectangleList { get; set; } = new List<BoundaryRectangle>();
        [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public List<BoundaryEllipse> BoundaryEllipseList { get; set; } = new List<BoundaryEllipse>();
        [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public List<BoundaryPolygon> BoundaryPolygonList { get; set; } = new List<BoundaryPolygon>();

        // This is the list of Horizontal panels
        [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public List<HorizontalPanel> HorizontalPanelList { get; set; } = new List<HorizontalPanel>();

        // This is the list of vertices for this layout
        [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public List<Vertex> VertexList { get; set; } = new List<Vertex>();
        [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public List<Edge> EdgeList { get; set; } = new List<Edge>();

    }

}
