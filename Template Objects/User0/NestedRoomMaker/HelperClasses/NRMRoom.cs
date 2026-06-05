using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShapeTemplateLib.Templates.User0
{
    public class NRMRoom
    {
        public List<NRMRoomEdge> WallSegments { get; set; }

        public List<NRMOpenArea> ConnectedOpenAreas { get; set; } = new List<NRMOpenArea>();

        // This flag is used when exporting the connection information. The list of open areas is great for us 
        // but we don't need a list of open areas, we only need to know that the rooms do connect and to what.
        // each open area will be represented by a flag value

        public long ConnectedOpenAreaFlag { get; set; }

        // see if this room contains this open area but doesn't share common edges
        public bool Encloses(NRMOpenArea open)
        {
            NRMEdgeList elRoom = new NRMEdgeList(this.WallSegments);
            NRMEdgeList elOpen = new NRMEdgeList(open.WallSegments);

            return elRoom.Encloses(elOpen);

        }

        private class wrkClockwise
        {
            public NRMRoomEdge oWallSegment { get; set; }
            public float Angle { get; set; }
        }

        public void Cleanup()
        {
            //  create new edge objects for this clas
            List<NRMRoomEdge> temp = new List<NRMRoomEdge>();
            foreach (var re in WallSegments)
            {
                NRMRoomEdge nre = new NRMRoomEdge(re.From, re.To);
                nre.ID = re.ID;

                nre.InteriorDoorCandidate = re.InteriorDoorCandidate;
                nre.ExteriorWindowCandidate = re.ExteriorWindowCandidate;

                // These values are used when building rooms
                nre.IsExteriorEdge = re.IsExteriorEdge;
                nre.IsOpenSpaceEdge = re.IsOpenSpaceEdge; 

                temp.Add(nre);
            }

            WallSegments.Clear();
            WallSegments.AddRange(temp);

            int count = WallSegments.Count();

            temp.Clear();
            temp.Add(WallSegments[0]);
            // arbitrarily start at the 'from' point of the 0th. This may wind things the wrong way but we can correct that
            NRMRoomEdge StartEdge = temp[0];
            WallSegments.RemoveAt(0);


            while (temp.Count < count)
            {
                // find this next segment
                var next = WallSegments.Where(m => m.From == StartEdge.To || m.To == StartEdge.To).FirstOrDefault();
                if (next == null) break;

                if (next.To == StartEdge.To)
                {
                    PointF tmp = next.To;
                    next.To = next.From;
                    next.From = tmp;   
                }

                temp.Add(next);
                WallSegments.Remove(next);

                StartEdge = next;

            }
#if false
            StringBuilder sb = new StringBuilder();
            foreach (var ed in temp)
            {
                sb.AppendLine($"{ed.From.X},{ed.From.Y} -> {ed.To.X},{ed.To.Y}");
            }
            string edges = sb.ToString();
#endif

            // Now order these so that they are counter clockwise

            RewindCounterClockwise(temp);
#if false
            sb = new StringBuilder();
            foreach (var ed in temp)
            {
                sb.AppendLine($"{ed.From.X},{ed.From.Y} -> {ed.To.X},{ed.To.Y}");
            }
            string xxx = sb.ToString();
#endif
            WallSegments = temp;
           
            
        }

#if true

        //
        // From an algorithm on stackoverflow:
        // https://stackoverflow.com/questions/1165647/how-to-determine-if-a-list-of-polygon-points-are-in-clockwise-order
        // take the cross product of each pair of edges. If the cross product is negative the edges are counterclockwise
        // if the edges are positive the edges are clockwise
        //
        public void RewindCounterClockwise(List<NRMRoomEdge> RoomEdgeList)
        {
            // we know that the list is end to end. find out if its clockwise
            List<double> CrossProductList = new List<double>();

            for (int i=0; i < RoomEdgeList.Count; i++)
            {
                NRMRoomEdge v1 = RoomEdgeList[i];
                Vector edge1 = new Vector(v1.To.X - v1.From.X, v1.To.Y - v1.From.Y);

                NRMRoomEdge v2 = RoomEdgeList[(i+1) % RoomEdgeList.Count];
                Vector edge2 = new Vector(v2.To.X - v2.From.X, v2.To.Y - v2.From.Y);

                double lsquared = Math.Sqrt(edge1.LengthSquared + edge2.LengthSquared);

                double CrossProduct = edge2.X * edge1.Y - edge1.X * edge2.Y;

                CrossProductList.Add(CrossProduct/ lsquared);

            }

            // If this is clockwise reverse the order
            if (CrossProductList.Sum() > 0)
            {
                RoomEdgeList.Reverse();
            }
        }


#endif
        // Get area of an irregular polygon
        // https://www.wikihow.com/Calculate-the-Area-of-a-Polygon
        // the wallsegments are assumed to connect and be counterclockwise in order. This was accomplished earlier by calling 'cleanup'
        // after the room was constructed

        public float Area
        {
            get
            {
                if (WallSegments.Count == 0) return 0;

                float[] Xlist = new float[WallSegments.Count + 1];
                float[] YList = new float[WallSegments.Count + 1];

                for (int i=0; i < WallSegments.Count; i++) 
                {
                    Xlist[i] = WallSegments[i].From.X;
                    YList[i] = WallSegments[i].From.Y;
                }
                // copy the 0th point to the end
                Xlist[WallSegments.Count] = WallSegments[0].From.X;
                YList[WallSegments.Count] = WallSegments[0].From.Y;

                // Multiply the x coordinate of each vertex by the y coordinate of the next vertex. Add the results. 
                // Multiply the y coordinate of each vertex by the x coordinate of the next vertex. Add these results. 

                float sum1 = 0;
                float sum2 = 0;
                for (int i=0; i < WallSegments.Count; i++)
                {
                    sum1 += Xlist[i] * YList[i + 1];
                    sum2 += Xlist[i + 1] * YList[i];
                }

                // Subtract the sum of the second products from the sum of the first products

                float sum3 = sum1 - sum2;

                // Divide this difference by 2 to get the area of the polygon

                return sum3 / 2;
               
            }
        }

    
        private bool IsClockwise(List<Point2D> vertices)
        {
            /*
             * These vertices are spokes and can come in with different lengths, which can produce false positives, since the
             * logic of this code assumes that its being given the outline of a polygon.
             * In order to use this function we want to compare these as equal length vectors around 
             * the origin, as if it were creating a regular polygon centered at the origin, since that is 
             * the intent of its usage. Normalizing the vectors should take care of it.
             */
            List<Vector> nlist = new List<Vector>();
            foreach (var p in vertices)
            {
                Vector v = new Vector(p.X, p.Y);
                v.Normalize();
                nlist.Add(v);
            }

            double sum = 0.0;
            for (int i = 0; i < nlist.Count; i++)
            {
                Vector v1 = nlist[i];
                Vector v2 = nlist[(i + 1) % vertices.Count]; // % is the modulo operator
                sum += (v2.X - v1.X) * (v2.Y + v1.Y);
            }
            return sum > 0.0;

        }
    }
}
