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
        public bool SplitEdge(int index,int width)
        {
            if (index >= EdgeList.Count) return false;

            Edge ed = EdgeList[index];
            if (EdgeLength(ed) < width) return false;

            int nextindex = VertexList.Max(m => m.Index) + 1;

            Tuple<Vertex, Vertex> newpoints = GetHoleCoordinates(ed, width, .5F);

            // add the new points
            newpoints.Item1.Index = nextindex++;
            VertexList.Add(newpoints.Item1);

            newpoints.Item2.Index = nextindex;
            VertexList.Add(newpoints.Item2);

            EdgeList.Add(new Edge() { Height = ed.Height, p1 = newpoints.Item2.Index, p2 = ed.p2 });

            // modify existing edge and insert new edge
            ed.p2 = newpoints.Item1.Index;

            return true;        

        }

        public Tuple<Vertex, Vertex> GetHoleCoordinates(Edge oEdge,int HoleSize, float fraction)
        {
            Vertex v1 = VertexList[oEdge.p1];
            Vertex v2 = VertexList[oEdge.p2];

            // If the hole size is 0 return the point at this fraction

            if (HoleSize <= 0)
            {
                Vertex p = GetProportionalPoint(oEdge, fraction);
                return new Tuple<Vertex, Vertex>(p, p);
            }


            // what percentage of the length of this edge is this hole?

            var percentlength = (float)HoleSize / EdgeLength(oEdge);

            // now we know the percentage. Get the point that is before and after this percentage length

            Vertex nf = GetProportionalPoint(oEdge,fraction - percentlength);
            Vertex nt = GetProportionalPoint(oEdge,fraction + percentlength);

            return new Tuple<Vertex, Vertex>(nf, nt);

        }

        private float Distance(Vertex v1, Vertex v2)
        {
            float x = v1.X - v2.X;
            float y = v1.Y - v2.Y;

            return (float)Math.Sqrt(x * x + y * y);
        }

        private Vertex GetProportionalPoint(Edge oEdge, float fraction)
        {

            Vertex v1 = VertexList[oEdge.p1];
            Vertex v2 = VertexList[oEdge.p2];

            return new Vertex()
            {
                X = v1.X + fraction * (v2.X - v1.X),
                Y = v1.Y + fraction * (v2.Y - v1.Y)
            };
        }
    }
}
