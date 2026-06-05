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
        public float EdgeLength(Edge e)
        {
            Vertex v1 = VertexList.Where(m => m.Index == e.p1).FirstOrDefault();
            Vertex v2 = VertexList.Where(m => m.Index == e.p2).FirstOrDefault();

            return (float) Math.Sqrt((v2.Y - v1.Y) * (v2.Y - v1.Y) + (v2.X - v1.X) * (v2.X - v1.X));
        }

    }
}
