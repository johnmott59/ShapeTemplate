using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// This is an edge used in the FloorLayout template.
    /// </summary>
    public partial class FLEdge : Edge
    {
        /*
         * Edges are shared between rooms, they keep an index, but all edges are associated with a floor so we can
         * retrieve their point values
         */
         public FloorLayout oFloor { get; set; }

        public int Index { get; set; }
        /*
         * These flags will be used to tell us whether this edge is an exterior wall or is next to open spac;
         * They are set by the room algorithm as starting points for creating a floor layout
         */
        public int IsExteriorEdge { get; set; }
        public int IsOpenSpaceEdge { get; set; }
        /*
         * These flags are used in processing rooms and windows to guide placement. They are set by the 
         * template methods that automatically identify the edges that can be doors and windows
         */
        public int DoorCandidate { get; set; }
        public int DoorPresent { get; set; }
        public int WindowCandidate { get; set; }

        public Tuple<Vertex,Vertex> Vertices
        {
            get
            {
                Vertex v1 = oFloor.VertexList.Where(m => m.Index == p1).FirstOrDefault();
                Vertex v2 = oFloor.VertexList.Where(m => m.Index == p2).FirstOrDefault();
                return new Tuple<Vertex, Vertex>(v1, v2);
            }
        }

        public bool SameEndPoints(FLEdge r)
        {
            return Equals(r) || (p1 == r.p2 && p2 == r.p1);
        }

        public bool Equals(FLEdge input)
        {
            return p1 == input.p1 && p2 == input.p2;
        }

        /// <summary>
        /// Get the length of this edge
        /// </summary>
        public float EdgeLength
        {
            get
            {
                if (oFloor == null) return 0;

                Vertex v1 = oFloor.VertexList[p1];
                Vertex v2 = oFloor.VertexList[p2];

                var x2 = (v1.X - v2.X) * (v1.X - v2.X);
                var y2 = (v1.Y - v2.Y) * (v1.Y - v2.Y);

                return (float)Math.Sqrt(x2 + y2);
            }
        }

        public Vertex GetProportionalPoint(int numerator, int denominator)
        {
            var fraction = (float)numerator / (float)denominator;

            return GetProportionalPoint(fraction);
        }

        public Vertex GetProportionalPoint(float fraction)
        {
            if (oFloor == null) return null;

            Vertex v1 = oFloor.VertexList[p1];
            Vertex v2 = oFloor.VertexList[p2];

            return new Vertex()
            {
                X = v1.X + fraction * (v2.X - v1.X),
                Y = v1.Y + fraction * (v2.Y - v1.Y)
            };
        }
        /// <summary>
        /// FLip the from and to points, used to wind a set of edges so that they are from->to, from->to
        /// </summary>
        public void Flip()
        {
            int tmp = p1;
            p1 = p2;
            p2 = p1;

        }
#if false
        // turned out that I didn't need this but I love the algorithm so hang on to it
        /// <summary>
        /// Check to see if a point 'c' is inside the line segment formed by a and b
        /// Algorithm from 
        /// https://stackoverflow.com/questions/328107/how-can-you-determine-a-point-is-between-two-other-points-on-a-line-segment
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool PointInSegment(Point2D a, Point2D b, Point2D c)
        {
            var crossproduct = (c.Y - a.Y) * (b.X - a.X) - (c.X - a.X) * (b.Y - a.Y);
            if (crossproduct != 0) return false;

            var dotproduct = (c.X - a.X) * (b.X - a.X) + (c.Y - a.Y) * (b.Y - a.Y);
            if (dotproduct < 0) return false;

            var squaredlengthba = (b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y);
            if (dotproduct > squaredlengthba) return false;

            return true;

        }
#endif

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HoleSize">size of the hole</param>
        /// <param name="numerator">The fractional starting point of the hole, 1/2, 1/4 etc</param>
        /// <param name="denominator">The fractional starting point of the hole, 1/2, 1/4 etc</param>
        /// <returns></returns>
        public Tuple<Vertex, Vertex> GetHoleCoordinates(int HoleSize, int numerator, int denominator)
        {
            if (oFloor == null) return null;

            Vertex v1 = oFloor.VertexList[p1];
            Vertex v2 = oFloor.VertexList[p2];

            // If the hole size is >= the length of the edge return the edge

            if (HoleSize >= EdgeLength) return new Tuple<Vertex, Vertex>(v1, v2);

            // If the hole size is 0 return the point at this fraction

            if (HoleSize <= 0)
            {
                Vertex p = GetProportionalPoint(numerator, denominator);
                return new Tuple<Vertex, Vertex>(p, p);
            }

            var fraction = (float)numerator / (float)denominator;

            // what percentage of the length of this edge is this hole?

            var percentlength = (float)HoleSize / EdgeLength;

            // now we know the percentage. Get the point that is before and after this percentage length

            Vertex nf = GetProportionalPoint(fraction - percentlength);
            Vertex nt = GetProportionalPoint(fraction + percentlength);

            if (Distance(v1,nf) < 5 || Distance(v2,nt) < 5) 
            {
                string x = "asd";
                
            }

            return new Tuple<Vertex, Vertex>(nf, nt);


        }

        private float Distance(Vertex v1, Vertex v2)
        {
            float x = v1.X - v2.X;
            float y = v1.Y - v2.Y;

            return (float) Math.Sqrt(x * x + y * y);
        }

    }
}
