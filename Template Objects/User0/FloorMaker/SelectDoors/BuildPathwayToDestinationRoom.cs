using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorMaker
    {
        private void BuildPathwayToDestinationRoom(FMDestinationRoom dRoom, List<FMEdge> FMEdgeCandidateList)
        {
            // find all rooms that connect to this open area

            var aRoomList = this.AssembledRoomList.Where(m => m.DestinationRoomList.Contains(dRoom)).ToList();

            // keep a list of the rooms that have a connection to this open area, we don't want to form more than one connection

            List<FMAssembledRoom> ConnectedToDestinationRoom = new List<FMAssembledRoom>();

            // do a breadth first search, locating all rooms that directly connect to the destination area, either by enclosing it
            // or by having a common edge. We want to prefer that a room that directly connects will have a door
            // that connects. Only if a room doesn't directly connect do we create a path through another room

            // The rooms that remain do not have a direct connection to the open area, so they are back rooms.

#if true
            // int evenodd = 0;
            foreach (var aRoom in aRoomList)
            {
                // we may have already marked this room as connected via a connection from another room. If so we're done.
                // we only want one connection to the destination
                // 

                if (ConnectedToDestinationRoom.Contains(aRoom)) continue;

                List<int> CommonEdgeList = new List<int>();
                // does this room have a common edge with the open area?

                foreach (var dEdge in dRoom.EdgeIndexList)
                {
                    foreach (var aEdge in aRoom.EdgeIndexList)
                    {
                        if (dEdge == aEdge) CommonEdgeList.Add(dEdge);
                    }
                }

                // if there were no common edges the room encloses the open area. In that case pick one of the edges of the
                // open area for a door.

                int ndx = 0;

                if (CommonEdgeList.Count == 0)
                {
                    // Randomly pick an edge in the open area
                    int randomIndex = FloorMaker._doorRandom.Next(dRoom.EdgeIndexList.Length);
                    ndx = dRoom.EdgeIndexList[randomIndex];
                    System.Diagnostics.Debug.WriteLine($"  Room encloses open area - randomly selected edge {ndx} from {dRoom.EdgeIndexList.Length} open area edges (index {randomIndex})");
                }
                else
                {
                    // Of the edges that connect to the open area, randomly pick 1
                    int randomIndex = FloorMaker._doorRandom.Next(CommonEdgeList.Count);
                    ndx = CommonEdgeList[randomIndex];
                    System.Diagnostics.Debug.WriteLine($"  Randomly selected edge {ndx} from {CommonEdgeList.Count} common edges (index {randomIndex})");
                }

                // Select this edge as a doorway and assign HoleGroupID
                FMEdge f = this.EdgeList[ndx];

                // Assign HoleGroupID to actually mark this as a door!
                if (string.IsNullOrEmpty(f.HoleGroupID))
                {
                    f.HoleGroupID = "door";  // Use a default door pattern
                    System.Diagnostics.Debug.WriteLine($"  -> Assigned HoleGroupID='door' to Edge {f.Index}");
                }

                if (!FMEdgeCandidateList.Contains(f)) FMEdgeCandidateList.Add(f);

                // Mark this room as being connected to the open area. Once its connected it doesn't require a second
                // connection

                ConnectedToDestinationRoom.Add(aRoom);

                /*
                 * The presence of this code to recurse creates depth first processing, which will create more back
                 * rooms, even if a room is directly connected to an open area. This is neither good nor bad but 
                 * is something to note. Initially I decided to go with a breadth first because it has a tidier look,
                 * but having a mix of rooms which directly open and some that do not can be interesting.
   
                 * There shuold be a way to allow this code to be selectively called in a way that produces
                 * a variety of back rooms and non-back rooms. My first attempt wasn't satisfying but I think 
                 * that controlling the depth of searching for back rooms and such can produce interesting layouts
                 */
#if false
                evenodd = 1 - evenodd;
                if (evenodd == 0)
                {
                    List<FMRoom> VisitedList = new List<FMRoom>();
                    Recurse4(rm, VisitedList, ConnectedToOpenArea, FMEdgeCandidateList);
                }
#endif

            }
#endif

            foreach (var rm in aRoomList)
            {
                /*
                 * Find all of the rooms connected to this room and select a door to this room. This will ensure 
                 * connectivity
                 */
                List<FMAssembledRoom> VisitedList = new List<FMAssembledRoom>();
                FindConnectedRooms(rm, VisitedList, ConnectedToDestinationRoom, FMEdgeCandidateList);

            }
        }


    }
}
