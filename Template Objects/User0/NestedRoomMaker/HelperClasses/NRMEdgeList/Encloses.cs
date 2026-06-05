using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ShapeTemplateLib.Templates.User0
{
    // This is a list of edges that used in polygon processing, although they may not form a polygon

    public partial class NRMEdgeList
    {
        /// <summary>
        ///  See if this polygon encloses the target
        /// </summary>
        /// <param name="PotentialEnclosee"></param>
        /// <returns></returns>
        public bool Encloses(NRMEdgeList PotentialEnclosee)
        {
            /*
             * To establish whether or not this encloses another polygon we answer two questions:
             * 
             * 1. do any of the edges of the target intersect us?
             * 2. is every point of the target and is every center of every edge inside us?
             */
             foreach (var PotentialEncloseeEdge in PotentialEnclosee.NRMRoomEdgeList)
            {
                // does this edge intersect any of our edges
                foreach(var pe in this.NRMRoomEdgeList)
                {
                    PointF? intersect = PolygonEdge.FindInsideIntersectionPoint(PotentialEncloseeEdge, pe);
                    // there is an intersection, cant be totally inside
                    if (intersect != null) return false;
                }
            }

            // if we made it here then there are no intersections. If any of the target points are outside
            // our polygon then the polygon isn't enclosed

            StringBuilder sb = new StringBuilder();
            foreach (var s in this.NRMRoomEdgeList)
            {
                sb.AppendLine($"{s.From.X},{s.From.Y} -> {s.To.X},{s.To.Y} ");
            }
            string encloser = sb.ToString();

            foreach (var PotentialEncloseeEdge in PotentialEnclosee.NRMRoomEdgeList)
            {
                if (!NRMRoomEdge.IsPointInsidePolygon(PotentialEncloseeEdge.From, this.NRMRoomEdgeList)) return false;
            }

            // Now that we've checked the points check the center points. Are the center points inside? 

            // first identify the edges in our potential enclosee that don't match edges in the bounding box. 

            List<NRMRoomEdge> temp = new List<NRMRoomEdge>(PotentialEnclosee.NRMRoomEdgeList);

            foreach (var P1 in temp.ToList())
            {
                foreach (var P2 in this.NRMRoomEdgeList)
                {
                    if (P2.SameEndPoints(P1)) temp.Remove(P1);
                }
            }

            // now test the potential enclosed edges that do not contain the same end points against the bounding polygon
            foreach (var PotentialEncloseeEdge in temp)
            {
                if (!NRMRoomEdge.IsPointInsidePolygon(PotentialEncloseeEdge.CenterPoint, this.NRMRoomEdgeList)) return false;
            }

            return true;
        }

    }
}
