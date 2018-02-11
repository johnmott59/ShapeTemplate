using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{
    // A vertex is a position 
    /// <summary>
    /// The Vertex class is used to hold a position value for an edge in the layout template. These values are X,Y values in a 2D cartesion
    /// coordinate system as viewed from above, looking down on the layout. The network of walls formed by the layout is a 2D graph, with edges
    /// and vertices
    /// </summary>
    [HelpItem(eItemFlavor.TemplateSupport, "Vertex")]
    public partial class Vertex
    {

        public Vertex()
        {
            Index = 0;
            X = 0;
            Y = 0;
        }
        /// <summary>
        /// Index of this vertex. Edges can share a vertex, so vertices are maintained in a list by the layout and referred to via an index value
        /// </summary>
        [HelpProperty(SampleValue = "5", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public int Index { get; set; }

        /// <summary>
        /// X value of this vertex. A vertex is defined in a 2D coordinate system looking down onto the network of walls/edges
        /// </summary>
        [HelpProperty(SampleValue = "25", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public float X { get; set; }

        /// <summary>
        /// Y value of this vertex. A vertex is defined in a 2D coordinate system looking down onto the network of walls/edges
        /// </summary>
        ///   
        [HelpProperty(SampleValue = "25", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent)]
        public float Y { get; set; }

        // The CopyFrom method is used in Javascript to convert a version of the object retrieved via JSON
        // into a full object of this type. An object retrieved via JSON.parse() will have the correct property
        // names but not be an object of this type, this will take that property-only version and create a full
        // object
        public static Vertex CopyFrom(Vertex oSource)
        {
            Vertex v = new Vertex();

            v.Index = oSource.Index;
            v.X = oSource.X;
            v.Y = oSource.Y;

            return v;
        }
    }
}
