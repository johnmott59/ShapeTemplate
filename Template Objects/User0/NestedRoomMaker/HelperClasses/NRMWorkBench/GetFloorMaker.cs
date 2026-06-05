using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    // This work class collects together the items that will go into creating one set of rooms, an outline, the open areas within that outline,
    // and the wall segments that connect the rooms

    public partial class NRMRoomWorkBench
    {
        public FloorMaker GetFloorMaker()
        {
            FloorMaker oFloorMaker = new FloorMaker();
            /*
             * Locate all unique points in the roomedgegroup
             */
            List<PointF> vlist = new List<PointF>();
            foreach (NRMRoom r in RoomList)
            {
                foreach (NRMRoomEdge edge in r.WallSegments)
                {
                    if (!vlist.Contains(edge.From)) vlist.Add(edge.From);
                    if (!vlist.Contains(edge.To)) vlist.Add(edge.To);
                }
            }

            // Add the list of vertices to the floor, edges will have indices to the vertices
            oFloorMaker.VertexList = new Vertex[vlist.Count];
            for (int i = 0; i < vlist.Count; i++)
            {
                oFloorMaker.VertexList[i] = new Vertex() { Index = i, X = vlist[i].X, Y = vlist[i].Y };
            }

            /*
             * Now create the unique list of edges so they are in a list
             */
            List<NRMRoomEdge> elist = new List<NRMRoomEdge>();
            foreach (NRMRoom r in RoomList)
            {
                foreach (NRMRoomEdge edge in r.WallSegments)
                {
                    // See if the endpoints in this edge object match ones in our list. If two edges
                    // have the same from and to values they are the same edge
                    int count = elist.Where(m => m.SameEndPoints(edge)).Count();
                    if (count == 0)
                    {
                        elist.Add(edge);
                    }
                }
            }
            /*
             * Add these edges to the floor in order, a room will use index values to retrieve them
             */
            oFloorMaker.EdgeList = new FMEdge[elist.Count];
            for (int i = 0; i < elist.Count; i++)
            {
                var pe = elist[i];  // get edge to work with

                var o = new FMEdge()
                {
                    oFloor = oFloorMaker,
                    Index = i,
                    p1 = vlist.IndexOf(elist[i].From),
                    p2 = vlist.IndexOf(elist[i].To),
                    // The only intelligence about the edges that the algo passes is whether the are exterior edges or open space edges
                    IsExteriorEdge = pe.IsExteriorEdge, 
                    IsOpenSpaceEdge = pe.IsOpenSpaceEdge 
                };

                /*
                 * Based on the knowledge of whether something is an exterior edge or an open space we can identify 
                 * door and window candidates. We can probably remove the exterior and openspace edge properties
                 */
                o.InteriorDoorCandidate = o.IsOpenSpaceEdge;
                o.ExteriorWindowCandidate = o.IsExteriorEdge;

                oFloorMaker.EdgeList[i] = o;
            }

            oFloorMaker.AssembledRoomList = new FMAssembledRoom[RoomList.Count];

            for (int i = 0; i < RoomList.Count; i++)
            {
                NRMRoom r = RoomList[i];
                FMAssembledRoom o = new FMAssembledRoom();
                oFloorMaker.AssembledRoomList[i] = o;

                o.oFloor = oFloorMaker;
                List<int> EdgeIndexList = new List<int>();
                /*
                 * Collect the list of edges indices from the list of edges for this room
                 */
                int EdgesConnectingToOpenArea = 0;  // keep count of how many open area edges there are
                for (int j = 0; j < r.WallSegments.Count; j++)
                {
                    NRMRoomEdge p = r.WallSegments[j];

                    // get the index of this edge
                    for (int k = 0; k < elist.Count; k++)
                    {
                        if (elist[k].SameEndPoints(p))
                        {
                            // Note that the first time we build this list we can use the straight up indices, but
                            // ongoing management will require understanding that the index 
                            EdgeIndexList.Add(k);
                            EdgesConnectingToOpenArea += oFloorMaker.EdgeList[k].IsOpenSpaceEdge;
                        }
                    }
                }
                if (EdgesConnectingToOpenArea > 0)
                {
                    o.ConnectsToOpenArea = 1;
                }
                else
                {
                    o.BackRoom = 1;
                }

                o.EdgeIndexList = EdgeIndexList.ToArray();
            }
            /*
             * Now that we have all rooms and all edges we can further identify the walls in back rooms that are candidates
             * for doors
             */
            while (true)
            {
                var list = oFloorMaker.AssembledRoomList.Where(m => m.ConnectsToOpenArea == 0).ToList();

                foreach (var room in list)
                {
                    /*
                     * For each edge in these rooms see if any connect to a room that connects to an open area
                     */
                    for (int i = 0; i < room.EdgeIndexList.Length; i++)
                    {
                        // Get the index of this edge
                        int ndx = room.EdgeIndexList[i];

                        // now get this edge
                        FMEdge e = oFloorMaker.EdgeList[ndx];

                        // an exterior edge isn't a door candidate
                        if (e.IsExteriorEdge == 1) continue;

                        // Get the list of rooms that this edge connects to
                        var oelist = oFloorMaker.AssembledRoomList.Where(m =>
                                m != room                               // not this room
                                && m.ConnectsToOpenArea == 1            // does connect to an open area
                                && m.EdgeIndexList.Contains(i)).ToList();    // has this edge

                        // do any of these connect to the open area? if so then we can get to the open area by way of this edge
                        if (oelist.Count > 0)
                        {
                            e.InteriorDoorCandidate = 1;
                            room.ConnectsToOpenArea = 1;
                        }
                    }
                }

                return oFloorMaker;
            }
        }
    }
}
