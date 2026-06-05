using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class NRMRoomFinder
    {
        /// <summary>
        /// This will search for polygons along one direction of an edge. There should only be one polygon that we want,
        /// it will be the one with the shortest number of segments.
        /// </summary>
        /// <param name="pCurrentEdge"></param>
        /// <param name="StartPosition"></param>
        /// <param name="Depth"></param>
        /// <returns> true - we found a polygon, stop looking. 
        /// false - we didn't find a polygon </returns>
        public bool Recurse(NRMRoomEdge pCurrentEdge, eStartPosition StartPosition, int Depth)
        {
            if (DebugSteps)
            {
                CreationSteps.Add(CurrentEdgeList.Reverse().ToList());
            }

            /*
             * Does the other side of this edge connect to the start point of the first edge? If so we have
             * a complete room/
             */
            if (StartPosition == eStartPosition.FromPoint)
            {
                if (pCurrentEdge.To == StartingPoint)
                {
                    if (AddCurrentEdgeListToRoomList()) return true;
                }

                // ok, the to edge didn't close it, make sure its not already on the list so we don't cross
                int count = CurrentEdgeList.Where(m => m != pCurrentEdge && (m.From == pCurrentEdge.To || m.To == pCurrentEdge.To)).Count();
                if (count > 0)
                {
                    return false;
                }
            }
            else
            {
                if (pCurrentEdge.From == StartingPoint)
                {
                    if (AddCurrentEdgeListToRoomList()) return true;
                }

                // ok, the From edge didn't close it, make sure its not already on the list so we don't cross
                int count = CurrentEdgeList.Where(m => m != pCurrentEdge && (m.From == pCurrentEdge.From || m.To == pCurrentEdge.From)).Count();
                if (count > 0)
                {
                    return false;
                }
            }

            /*
             * Get the list of all edges that connect to the other side of this edge, then sort them by angle, the
             * most acute angle first. We will navigate to the actute side until we find a closed polygon
             */

            if (StartPosition == eStartPosition.FromPoint)
            {
                // Get the edges that connect to the 'to' point and sort them by angle. lower angles will form acute
                // triangles -- the 'leftmost' as you're looking at the edge going straight up. We will take the leftmost 
                // angle to start

                List<Tuple<double, NRMRoomEdge>> OrderedEdgeList = GetEdgesByAngle(pCurrentEdge, StartPosition);
                foreach (Tuple<double, NRMRoomEdge> p in OrderedEdgeList)
                {
                    CurrentEdgeList.Push(p.Item2);
                    // if the 'to' point is the connector then its our starting point
                    if (p.Item2.To == pCurrentEdge.To)
                    {
                        if (Recurse(p.Item2, eStartPosition.ToPoint, Depth + 1))
                        {
                            CurrentEdgeList.Pop();
                            return true;
                        }
                    }
                    else
                    {
                        if (Recurse(p.Item2, eStartPosition.FromPoint, Depth + 1))
                        {
                            CurrentEdgeList.Pop();
                            return true;
                        }
                    }
                    CurrentEdgeList.Pop();
                }
            }
            else
            {
                // Get the edges that connect to the 'From' point and sort them by angle. lower angles will form acute
                // triangles -- the 'leftmost' as you're looking at the edge going straight up. We will take the leftmost 
                // angle to start

                List<Tuple<double, NRMRoomEdge>> OrderedEdgeList = GetEdgesByAngle(pCurrentEdge, StartPosition);

                foreach (Tuple<double, NRMRoomEdge> p in OrderedEdgeList)
                {
                    CurrentEdgeList.Push(p.Item2);
                    // if the 'to' point is the connector then its our starting point
                    if (p.Item2.To == pCurrentEdge.From)
                    {
                        if (Recurse(p.Item2, eStartPosition.ToPoint, Depth + 1))
                        {
                            CurrentEdgeList.Pop();
                            return true;
                        }
                    }
                    else
                    {
                        if (Recurse(p.Item2, eStartPosition.FromPoint, Depth + 1))
                        {
                            CurrentEdgeList.Pop();
                            return true;
                        }
                    }
                    CurrentEdgeList.Pop();
                }
            }

            return false;

        }


    }
}
