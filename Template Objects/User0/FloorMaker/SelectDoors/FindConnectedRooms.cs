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
        // Random number generator for door selection
        private static Random _doorRandom = new Random();

        /// <summary>
        /// Locate all rooms that connect to this room and ensure that there is a path to get here
        /// </summary>
        /// <param name="oRoom"></param>
        ///
        private void FindConnectedRooms(FMAssembledRoom oRoom, List<FMAssembledRoom> VisitedList,
            List<FMAssembledRoom> ConnectedToOpenArea,
            List<FMEdge> CandidateList)
        {
            //add us to the visited room
            VisitedList.Add(oRoom);

            System.Diagnostics.Debug.WriteLine($"  FindConnectedRooms visiting room with {oRoom.EdgeIndexList.Length} edges");

            List<FMAssembledRoom> ConnectedRoomList = new List<FMAssembledRoom>();
            foreach (var ed in oRoom.EdgeIndexList)
            {
                // get all rooms that have this edge that are not this room
                var ConnectedRoomEdgeList = this.AssembledRoomList.Where(m => m.EdgeIndexList.Contains(ed) && !VisitedList.Contains(m)).ToList();

                System.Diagnostics.Debug.WriteLine($"    Edge {ed}: Found {ConnectedRoomEdgeList.Count} connected unvisited rooms");

                // for each of the connected rooms, get the shared edges and pick one
                foreach (var rm in ConnectedRoomEdgeList)
                {
                    List<int> CommonEdgeIndex = GetCommonEdges(rm, oRoom);
                    System.Diagnostics.Debug.WriteLine($"      Room has {CommonEdgeIndex.Count} common edges with current room");

                    // if there are common edges pick one
                    if (CommonEdgeIndex.Count > 0)
                    {
                        // if this room is not already connected to an open area add this opening so that this
                        // room can reach the open area open area and then remember that we added it; once we do
                        // that we won't need to add it again

                        if (!ConnectedToOpenArea.Contains(rm))
                        {
                            // Randomly select one of the common edges as a doorway
                            int randomIndex = _doorRandom.Next(CommonEdgeIndex.Count);
                            int selectedEdgeIndex = CommonEdgeIndex[randomIndex];
                            FMEdge f = EdgeList[selectedEdgeIndex];

                            System.Diagnostics.Debug.WriteLine($"      Randomly selected Edge {f.Index} from {CommonEdgeIndex.Count} common edges (index {randomIndex}) for doorway (was in candidate list: {CandidateList.Contains(f)})");

                            // Assign HoleGroupID to actually mark this as a door!
                            if (string.IsNullOrEmpty(f.HoleGroupID))
                            {
                                f.HoleGroupID = "door";  // Use a default door pattern
                                System.Diagnostics.Debug.WriteLine($"      -> Assigned HoleGroupID='door' to Edge {f.Index}");
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine($"      -> Edge {f.Index} already has HoleGroupID='{f.HoleGroupID}'");
                            }

                            if (!CandidateList.Contains(f)) CandidateList.Add(f);

                            ConnectedToOpenArea.Add(rm);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"      Room already connected to open area, skipping");
                        }
                    }

                    // now recurse to visit each room that this is connected to
                    FindConnectedRooms(rm, VisitedList, ConnectedToOpenArea, CandidateList);
                }

            }

        }
    }
}
