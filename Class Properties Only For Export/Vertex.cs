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
    public partial class Vertex
    {
        public Vertex()
        {
            Index = 0;
            X = 0;
            Y = 0;
        }

        public int Index { get; set; }
        public float X { get; set; }
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
