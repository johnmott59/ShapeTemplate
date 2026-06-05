using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ShapeTemplateLib;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{

    public partial class NestedRoomMaker
    {
        // public List<Room> RoomList { get; set; }
        // retrieve a floor layout. this might need to be compile?
 

        public void FindFloorMaker(List<FloorMaker> AllLayouts)
        {
            // Recurse

            foreach (var fli in Children)
            {
                fli.FindFloorMaker(AllLayouts);
            }

            // if this is an open area return
            if (this.OutlineType == eFLInput3OutsideType.OpenArea) return;

            foreach (NRMRoomWorkBench rws in oWorkBench.rwslist)
            {
                FloorMaker fl = GetFloorMaker(rws.RoomList,rws.OpenAreaList);

                oWorkBench.RoomLayoutList.Add(fl);
                AllLayouts.Add(fl);
            }

        }

        public FloorMaker GetFloorMaker(List<NRMRoom> RoomList,List<NRMOpenArea> OpenAreaList)
        {

            FloorMaker fl = new FloorMaker();
            /*
             * Locate all unique points in the rooms and in the open areas
             */
            List<PointF> VertexList = new List<PointF>();

            foreach (NRMRoom r in RoomList) 
            {
                foreach (PolygonEdge edge in r.WallSegments)
                {
                    if (!VertexList.Contains(edge.From)) VertexList.Add(edge.From);
                    if (!VertexList.Contains(edge.To)) VertexList.Add(edge.To);
                }
            }

            // add points from open area list. If the edges in the open area are part of a room they will get added.
            // if open areas are free standing this will add those points
            foreach (NRMOpenArea r in OpenAreaList)
            {
                foreach (PolygonEdge edge in r.WallSegments)
                {
                    if (!VertexList.Contains(edge.From)) VertexList.Add(edge.From);
                    if (!VertexList.Contains(edge.To)) VertexList.Add(edge.To);
                }
            }

            // Add the list of vertices to the floor, edges will have indices to the vertices
            fl.VertexList = new Vertex[VertexList.Count];
            for (int i = 0; i < VertexList.Count; i++)
            {
                fl.VertexList[i] = new Vertex() { Index = i, X = VertexList[i].X, Y = VertexList[i].Y };
            }

            /*
             * Now create the unique list of edges so they are in a list
             */
            List<NRMRoomEdge> EdgeList = new List<NRMRoomEdge>();
            foreach (NRMRoom r in RoomList)
            {
                foreach (NRMRoomEdge edge in r.WallSegments)
                {
                    // See if the endpoints in this edge object match ones in our list. If two edges
                    // have the same from and to values they are the same edge
                    int count = EdgeList.Where(m => m.SameEndPoints(edge)).Count();
                    if (count == 0)
                    {
                        EdgeList.Add(edge);
                    }
                }
            }

            // Add edges from open areas. If the edges are part of a room definition they will be part of the room.
            // if an open area doesn't touch any rooms it will add new edges
            foreach (NRMOpenArea r in OpenAreaList)
            {
                foreach (NRMRoomEdge edge in r.WallSegments)
                {
                    // See if the endpoints in this edge object match ones in our list. If two edges
                    // have the same from and to values they are the same edge
                    int count = EdgeList.Where(m => m.SameEndPoints(edge)).Count();
                    if (count == 0)
                    {
                        EdgeList.Add(edge);
                    }
                }
            }
            /*
             * Add these edges to the floor in order, a room will use index values to retrieve them
             */
            fl.EdgeList = new FMEdge[EdgeList.Count];
            for (int i = 0; i < EdgeList.Count; i++)
            {
                var pe = EdgeList[i];  // get edge to work with

                var o = new FMEdge()
                {
                    oFloor = fl,
                    Index = i,
                    p1 = VertexList.IndexOf(EdgeList[i].From),
                    p2 = VertexList.IndexOf(EdgeList[i].To),
                    ID = pe.ID
                };

                /*
                 * bring forward the door, window and edge properties
                 */
                o.InteriorDoorCandidate = pe.InteriorDoorCandidate ? 1 : 0;
                o.ExteriorWindowCandidate = pe.ExteriorWindowCandidate ? 1 : 0;
                o.IsExteriorEdge = pe.IsExteriorEdge;
                o.IsOpenSpaceEdge = pe.IsOpenSpaceEdge;

                fl.EdgeList[i] = o;
            }

            // Collect the array of open areas

            fl.DestinationRoomList = new FMDestinationRoom[OpenAreaList.Count];
            for (int i=0; i < OpenAreaList.Count; i++)
            {
                NRMOpenArea r = OpenAreaList[i];
                FMDestinationRoom o = new FMDestinationRoom();
                fl.DestinationRoomList[i] = o;

                o.oFloor = fl;

                // collect the list of edges for this open area
                List<int> EdgeIndexList = new List<int>();

                for (int j = 0; j < r.WallSegments.Count; j++)
                {
                    NRMRoomEdge p = r.WallSegments[j];

                    // get the index of this edge
                    for (int k = 0; k < EdgeList.Count; k++)
                    {
                        if (EdgeList[k].SameEndPoints(p))
                        {
                            // Note that the first time we build this list we can use the straight up indices, but
                            // ongoing management will require understanding that the index 
                            EdgeIndexList.Add(k);
                        }
                    }
                }

                o.EdgeIndexList = EdgeIndexList.ToArray();

            }


            // Collect the array of rooms
            fl.AssembledRoomList = new FMAssembledRoom[RoomList.Count];

            for (int i = 0; i < RoomList.Count; i++)
            {
                NRMRoom r = RoomList[i];
                FMAssembledRoom o = new FMAssembledRoom();
                fl.AssembledRoomList[i] = o;

                o.oFloor = fl;
                List<int> EdgeIndexList = new List<int>();

                // note whether this room connects to an open area. if it doesn't connect to an open area its a back room
                // and will have to have a room open to something that is connected to an open area

                o.ConnectsToOpenArea = r.ConnectedOpenAreas.Count() == 0 ? 0 : 1;
                if (o.ConnectsToOpenArea == 0)
                {
                    o.BackRoom = 1;
                }
                /*
                 * Collect the list of edges indices from the list of edges for this room
                 */
                int EdgesConnectingToOpenArea = 0;  // keep count of how many open area edges there are
                for (int j = 0; j < r.WallSegments.Count; j++)
                {
                    NRMRoomEdge p = r.WallSegments[j];

                    // get the index of this edge
                    for (int k = 0; k < EdgeList.Count; k++)
                    {
                        if (EdgeList[k].SameEndPoints(p))
                        {
                            // Note that the first time we build this list we can use the straight up indices, but
                            // ongoing management will require understanding that the index 
                            EdgeIndexList.Add(k);
                            EdgesConnectingToOpenArea += fl.EdgeList[k].IsOpenSpaceEdge;
                        }
                    }
                }
                o.EdgeIndexList = EdgeIndexList.ToArray();
            }

            // now that the rooms and open areas are identified we can further note which rooms are connected to which open areas
            // and vice versa. we have this information in the NRMRoom and NRMOpenarea lists, we have to transfer it to
            // the FMOpen and FMRoom lists

            for (int iRoom =0; iRoom < RoomList.Count; iRoom++)
            {
                NRMRoom nroom = RoomList[iRoom];
                FMAssembledRoom froom = fl.AssembledRoomList[iRoom];

                // for this room locate the connected areas

                foreach (var o in nroom.ConnectedOpenAreas)
                {
                    // get the index of this open are in the list
                    int ondx = OpenAreaList.IndexOf(o);

                    FMDestinationRoom fopen = fl.DestinationRoomList[ondx];

                    // add this open space to this room and this room to this open space
                    froom.DestinationRoomList.Add(fopen);
                    fopen.RoomList.Add(froom);      
                }    
            }

            /*
             * Now that we have all rooms and all edges we can further identify the walls in back rooms that are candidates
             * for doors
             */
            while (true)
            {
                var list = fl.AssembledRoomList.Where(m => m.ConnectsToOpenArea == 0).ToList();

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
                        FMEdge e = fl.EdgeList[ndx];

                        // an exterior edge isn't a door candidate
                        if (e.IsExteriorEdge == 1) continue;

                        // Get the list of rooms that this edge connects to
                        var oelist = fl.AssembledRoomList.Where(m =>
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

                return fl;
            }
        }
    }
}
