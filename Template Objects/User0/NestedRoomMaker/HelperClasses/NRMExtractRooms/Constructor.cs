using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShapeTemplateLib.Templates.User0
{
    // This holds a point and a set of edges that connect at that point

    class PointAndSpokes
    {
        public PointF CenterPoint { get; set; }
        public List<NRMRoomEdge> ConnectedEdges { get; set; } = new List<NRMRoomEdge>();

        // for each edge store the angle that its vector makes and the index into the connected edges
        public List<Tuple<double, NRMRoomEdge, Vector>> EdgesOrderedByAngle { get; set; } = new List<Tuple<double, NRMRoomEdge, Vector>>();

        // arrange out list of edges so that they are arranged clockwise. This arrangement will allow
        // us to navigate through the point to the edge that is the most acute angle

        public NRMRoomEdge GetClockwiseOutgoingEdge(NRMRoomEdge incomingEdge)
        {
            // if there is only one edge coming into this point return null

            if (EdgesOrderedByAngle.Count == 1) return null;

            // find the angle associated with this edge and then get the edge that is next clockwise

            for (int i=0; i < EdgesOrderedByAngle.Count; i++)
            {
                if (incomingEdge == EdgesOrderedByAngle[i].Item2)
                {
                    // we found the incoming edge. Get the next index, which represents the next clockwise spoke
                    int next = (i + 1) % EdgesOrderedByAngle.Count;

                    return EdgesOrderedByAngle[next].Item2;
                }
            }

            // this shouldn't happen
            throw new Exception("Unable to find outgoing edge in room finder");
        }

        public NRMRoomEdge GetCounterClockwiseOutgoingEdge(NRMRoomEdge incomingEdge)
        {
            // if there is only one edge coming into this point return null

            if (EdgesOrderedByAngle.Count == 1) return null;

            // find the angle associated with this edge and then get the edge that is next clockwise

            for (int i = 0; i < EdgesOrderedByAngle.Count; i++)
            {
                if (incomingEdge == EdgesOrderedByAngle[i].Item2)
                {
                    int previous;

                    if (i == 0)
                    {
                        // we found the incoming edge. Get the previous index, which represents the next clockwise spoke
                        previous = EdgesOrderedByAngle.Count - 1;
                    } else
                    {
                        // we found the incoming edge. Get the previous index, which represents the next clockwise spoke
                        previous = i - 1;
                    }

                    return EdgesOrderedByAngle[previous].Item2;
                }
            }

            // this shouldn't happen
            throw new Exception("Unable to find outgoing edge in room finder");
        }
        public void ArrangeClockWise()
        {
            List<Vector> vlist = new List<Vector>();

            for (int ndx = 0; ndx < ConnectedEdges.Count; ndx++)
            {
                NRMRoomEdge nre = ConnectedEdges[ndx];
                Vector vEdge;
                // Build a normalized vector of this edge where the center point
                if (nre.From == CenterPoint)
                {
                    vEdge = new Vector() { X = nre.To.X - nre.From.X, Y = nre.To.Y - nre.From.Y };
                }
                else
                {
                    vEdge = new Vector() { X = nre.From.X - nre.To.X, Y = nre.From.Y - nre.To.Y };
                }

                vEdge.Normalize();

                /*
                 * Get the angle between the (1,0) vector and each edge. The 'anglebetween' call produces 
                 * value 0 to -180 clockwise from the incoming vector and 0 to 180 counterclockwise from the incoming vector.
                 * After we adjust we will have positive values as they go clockwise.
                 */
#if false
                double a45 = Vector.AngleBetween(new Vector() { X = 1, Y = 0 }, new Vector() { X = 1, Y = 1 });
                double a90 = Vector.AngleBetween(new Vector() { X = 1, Y = 0 }, new Vector() { X = 0, Y = 1 });
                double a180 = Vector.AngleBetween(new Vector() { X = 1, Y = 0 }, new Vector() { X = -1, Y = 0 });
                double a225 = Vector.AngleBetween(new Vector() { X = 1, Y = 0 }, new Vector() { X = -1, Y = -1 });
                double a270 = Vector.AngleBetween(new Vector() { X = 1, Y = 0 }, new Vector() { X = 0, Y = -1 });

                a270 = a270 + 360;
#endif

                double theta = Vector.AngleBetween(new Vector() { X = 1, Y = 0 }, vEdge);

                // convert -180, 0 to 10,360
                if (theta < 0) theta += 360;
#if false
                if (theta < 0) theta = -theta;      // convert 0,-180 to 0,180
                else theta = 360 - theta;           // convert 0, 180 to 360,180
#endif

                EdgesOrderedByAngle.Add(new Tuple<double, NRMRoomEdge, Vector>(theta, nre, vEdge));
            }

            // sort so that the edges go clockwise. decreasing angles represent clockwise spokes

            EdgesOrderedByAngle = EdgesOrderedByAngle.OrderByDescending(m => m.Item1).ToList();

        }
    }
    public partial class NRMExtractRooms
    {
        List<PointAndSpokes> PointAndSpokeList { get; set; } = new List<PointAndSpokes>();

        public List<NRMEdgeList> RoomEdgeList { get; set; } = new List<NRMEdgeList>();
        public List<NRMEdgeList> OpenAreaEdgeList { get; set; } = new List<NRMEdgeList>();
        public List<NRMRoom> RoomList = new List<NRMRoom>();
        public List<NRMOpenArea> OpenAreaList = new List<NRMOpenArea>();

        // provided with lists of edges that intersec
        public NRMExtractRooms(NRMEdgeList Outline, List<NRMEdgeList> OpenAreaList, NRMEdgeList WallSectionList)
        {
            /*
             * Create a list of unique points and the set of edges that go with those points
             */
            AddEdgeList(Outline);
            OpenAreaList.ForEach(m => AddEdgeList(m));
            AddEdgeList(WallSectionList);

            // remove any hubs that contain a single point; they can't be used to create rooms

            foreach (var pas in this.PointAndSpokeList.Where(m=>m.ConnectedEdges.Count < 2).ToList())
            {
                // get this edge
                var ed = pas.ConnectedEdges[0];

                // locate the spoke on the other side and remove this edge

                var other = this.PointAndSpokeList.Where(m => m.ConnectedEdges.Contains(ed)).FirstOrDefault();
                other.ConnectedEdges.Remove(ed);

                // remove this hub
                PointAndSpokeList.Remove(pas);
            }

            // Go through each of the center points, arranging the edges in clockwise order

            StringBuilder sb = new StringBuilder();

            foreach (PointAndSpokes p in PointAndSpokeList)
            {
                p.ArrangeClockWise();

                sb.AppendLine("---------------------");
                foreach (var v in p.EdgesOrderedByAngle)
                {
                    sb.AppendLine($"angle is {v.Item1} vector is {v.Item3.X}, {v.Item3.Y}");
                }
            }

            string x = sb.ToString();

            /*
             * Now find rooms by navigating the points and collecting all lists of edges. 
             * we only need to example starting hubs that have more than two spokes.
             */

            foreach (var PointAndSpoke in PointAndSpokeList)
            {
                foreach (var ed in PointAndSpoke.EdgesOrderedByAngle)
                {
                    bool openarea = true;

                    List<NRMRoomEdge> SingleRoom1 = CollectShapeForEdge(PointAndSpoke,ed.Item2,false);

                    if (SingleRoom1 != null)
                    {  
                        foreach (var v in SingleRoom1) if (v.IsOpenSpaceEdge != 1) openarea = false;

                        if (!openarea)
                        {
                            AddIfNotExist(this.RoomEdgeList, SingleRoom1);
                            // this.RoomList.Add(new NRMEdgeList() { NRMRoomEdgeList = SingleRoom1 });
                        }
                        else
                        {
                            AddIfNotExist(this.OpenAreaEdgeList, SingleRoom1);
                            // this.OpenAreaList.Add(new NRMEdgeList() { NRMRoomEdgeList = SingleRoom1 });
                        }
                    }

                    List<NRMRoomEdge> SingleRoom2 = CollectShapeForEdge(PointAndSpoke, ed.Item2, true);
                    if (SingleRoom2 != null)
                    {
                        openarea = true;
                        foreach (var v in SingleRoom2) if (v.IsOpenSpaceEdge != 1) openarea = false;

                        if (!openarea)
                        {
                            AddIfNotExist(this.RoomEdgeList, SingleRoom2);
                            // this.RoomList.Add(new NRMEdgeList() { NRMRoomEdgeList = SingleRoom2 });
                        }
                        else
                        {
                            AddIfNotExist(this.OpenAreaEdgeList, SingleRoom2);
                            //  this.OpenAreaList.Add(new NRMEdgeList() { NRMRoomEdgeList = SingleRoom2 });
                        }
                    }

                }
            }

            x = sb.ToString();

            // Find out if any rooms are contained by any other rooms. If so discard the outer roms
            // the encloses logic isn't working on some of the rooms.
         
            foreach (var Encloser in this.RoomEdgeList.ToList())
            {
                foreach (var Enclosee in this.RoomEdgeList.Where(m=>m != Encloser))
                {
                    int v1 = Encloser.NRMRoomEdgeList.Count;
                    int w1 = Enclosee.NRMRoomEdgeList.Count;
                   
                    if (Encloser.Encloses(Enclosee))
                    {
                        RoomEdgeList.Remove(Encloser);
                        break;
                    }
                }
            }


            foreach(var v in this.RoomEdgeList)
            {
                RoomList.Add(new NRMRoom()
                {
                    WallSegments = new List<NRMRoomEdge>(v.NRMRoomEdgeList)
                });
            }

            foreach (var v in this.OpenAreaEdgeList)
            {
                this.OpenAreaList.Add(new NRMOpenArea(v, 1));
            }
        }
        // check to see if a set of rooms
        private void AddIfNotExist(List<NRMEdgeList> list, List<NRMRoomEdge> Prospect)
        {
            foreach (var v in list)
            {
                // does this list have the same length?
                if (v.NRMRoomEdgeList.Count != Prospect.Count) continue;

                bool exist = true;
                // make sure they contain the same edges. We know that they have the same count so if
                // they are the same they will contain the same edges, even if they are not in order

                foreach (var ed in v.NRMRoomEdgeList)
                {
                    if (!Prospect.Contains(ed))
                    {
                        exist = false;
                        break;
                    }
                }

                // if this exists return
                if (exist) return;
               
            }

            // if we're here then the prospect is not on the list

            list.Add(new NRMEdgeList() { NRMRoomEdgeList = Prospect });
           
            return;
        }

        // Collect a closed shape for this edge

        private List<NRMRoomEdge> CollectShapeForEdge(PointAndSpokes PointAndSpoke,NRMRoomEdge CurrentEdge,bool Clockwise)
        {
            // get the starting point and the first edge of this room
            PointF StartPoint = PointAndSpoke.CenterPoint;

            PointAndSpokes NextPointAndSpoke = PointAndSpoke;
            List<NRMRoomEdge> singleRoom = new List<NRMRoomEdge>();
            List<PointAndSpokes> VisitedNodes = new List<PointAndSpokes>();

            singleRoom.Add(CurrentEdge);
            // extract edges that are the most acute angle in this graph. This will either lead to a dead end
            // or to a closed polygon
            VisitedNodes.Add(NextPointAndSpoke);
          
            while (true)
            {
                // locate the point and spokes for the other end of this edge

                PointF NextPoint = NextPointAndSpoke.CenterPoint == CurrentEdge.From ? CurrentEdge.To : CurrentEdge.From;

                NextPointAndSpoke = PointAndSpokeList.Where(m => m.CenterPoint == NextPoint).FirstOrDefault();
                if (NextPointAndSpoke == null)
                {
                    throw new Exception("Failed to find point in spoke list");
                }
                // If we've already seen this hub stop, we're not getting a clean polygon
                if (VisitedNodes.Contains(NextPointAndSpoke))
                {
                    return null;
                }
                // add this to the visited nodes. we only want to see each node once
                VisitedNodes.Add(NextPointAndSpoke);

                // get the next edge and add it to the list
                if (Clockwise)
                {
                    CurrentEdge = NextPointAndSpoke.GetClockwiseOutgoingEdge(CurrentEdge);
                } else
                {
                    CurrentEdge = NextPointAndSpoke.GetCounterClockwiseOutgoingEdge(CurrentEdge);
                }

                // we ran out of edges, there isn't a polygon
                if (CurrentEdge == null) break;
                // add this to the list of edges forming this polygon
                singleRoom.Add(CurrentEdge);

                // does this edge contain the starting point?

                if (CurrentEdge.From == StartPoint || CurrentEdge.To == StartPoint)
                {
                    return singleRoom;
                }
            }

            return singleRoom;
        }


        // Add this edge list to the graph
        private void AddEdgeList(NRMEdgeList el)
        {
            // add both end points to the list, either new points or existing ones

            foreach (var ed in el.NRMRoomEdgeList)
            {
                //
                // We don't want duplicate edges, so make a pass through these edges to see if these endpoints are
                // already in the list. If they are then we do want to copy over both the openarea and outline properties
                // Copying over open space and exterior edge properties will allow us to correctly idenfify rooms and open area.
                //
                bool FoundDuplicate = false;
                foreach (var p in PointAndSpokeList)
                {
                    NRMRoomEdge tmp = p.ConnectedEdges.Where(m => m.SameEndPoints(ed)).FirstOrDefault();
                    if (tmp != null)
                    {
                        // copy over the open area and outline flags
                        tmp.IsExteriorEdge = ed.IsExteriorEdge == 1 ? 1 : 0;
                        tmp.IsOpenSpaceEdge = ed.IsOpenSpaceEdge == 1 ? 1 : 0;
                        FoundDuplicate = true;
                    }
                }

                // if we found a duplicate return

                if (FoundDuplicate) continue;

                PointAndSpokes pe = PointAndSpokeList.Where(m => m.CenterPoint == ed.From).FirstOrDefault();
                if (pe == null)
                {
                    pe = new PointAndSpokes();
                    pe.CenterPoint = ed.From;
                    PointAndSpokeList.Add(pe);
                }
                pe.ConnectedEdges.Add(ed);

                pe = PointAndSpokeList.Where(m => m.CenterPoint == ed.To).FirstOrDefault();
                if (pe == null)
                {
                    pe = new PointAndSpokes();
                    pe.CenterPoint = ed.To;
                    PointAndSpokeList.Add(pe);
                }
                pe.ConnectedEdges.Add(ed);

            }
        }
    }
}
