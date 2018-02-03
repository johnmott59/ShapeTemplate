using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{

    // An edge is a from and to point, possibly a list of holes, a width and a height
    /// <summary>
    /// An Edge in the layout maps to a vertical wall, the endpoints of the edge are vertices in the layout. An edge can share vertices with
    /// other vertices, making the network a graph data structure. As a wall panel this edge can have a HoleGroup so that it can have holes.
    /// The 'width' of an edge is how thick the resulting wall is and the 'height' is how tall the wall is. 
    /// An edge has a front side and a back side. If you are looking down on the edge with the 'p1' point to the left and the 'p2' point to the
    /// right then the front side is up. This matters for hole placement because holes are placed at offsets of the front edge.
    /// </summary>
    [HelpItem(eItemFlavor.TemplateSupport, "Edge")]
    public partial class Edge
    {
        /// <summary>
        /// An edge can be identified by an ID value for purposes of code that manages the layout
        /// </summary>
        [HelpProperty(SampleValue = "SideEdge", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public string ID { get; set; } = "";

        /// <summary>
        /// The wall resulting from an edge can have a set of holes, and this ID value is used to identify them
        /// </summary>
        [HelpProperty(SampleValue = "hgSideWallWindows", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public string HoleGroupID { get; set; } = "";

        /// <summary>
        /// First endpoint of this edge. This the index value of the vertex object. The edge keeps the indices of the points, not the x,y values.
        /// </summary>
        [HelpProperty(SampleValue = "3", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public int p1 { get; set; }

        /// <summary>
        /// Second endpoint of this edge. This the index value of the vertex object. The edge keeps the indices of the points, not the x,y values.
        /// </summary>
        [HelpProperty(SampleValue = "3", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public int p2 { get; set; }

        /// <summary>
        /// Width of this edge. This is how thick the vertical wall is
        /// </summary>
        [HelpProperty(SampleValue = "20", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public int Width { get; set; } = 5;
        /// <summary>
        /// Height of this edge. This is the vertical height of the wall created by this edge
        /// </summary>
        [HelpProperty(SampleValue = "20", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public int Height { get; set; } = 20;

        // The CopyFrom method is used in Javascript to convert a version of the object retrieved via JSON
        // into a full object of this type. An object retrieved via JSON.parse() will have the correct property
        // names but not be an object of this type, this will take that property-only version and create a full
        // object
         public static Edge CopyFrom(Edge oSource)
        {
            Edge e = new Edge();
            e.ID = oSource.ID;
            e.HoleGroupID = oSource.HoleGroupID;
            e.p1 = oSource.p1;
            e.p2 = oSource.p2;
            e.Width = oSource.Width;
            e.Height = oSource.Height;

            return e;
        }


    }

    
    
}
