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

            List<FMAssembledRoom> ConnectedRoomList = new List<FMAssembledRoom>();
            foreach (var ed in oRoom.EdgeIndexList)
            {
                // get all rooms that have this edge that are not this room
                var ConnectedRoomEdgeList = this.AssembledRoomList.Where(m => m.EdgeIndexList.Contains(ed) && !VisitedList.Contains(m)).ToList();

                // for each of the connected rooms, get the shared edges and pick one
                foreach (var rm in ConnectedRoomEdgeList)
                {
                    List<int> CommonEdgeIndex = GetCommonEdges(rm, oRoom);
                    // if there are common edges pick one
                    if (CommonEdgeIndex.Count > 0)
                    {
                        // if this room is not already connected to an open area add this opening so that this
                        // room can reach the open area open area and then remember that we added it; once we do
                        // that we won't need to add it again

                        if (!ConnectedToOpenArea.Contains(rm))
                        {
                            // Note that this edge is a candidate for a doorway
                            FMEdge f = EdgeList[CommonEdgeIndex[0]];
                            if (!CandidateList.Contains(f)) CandidateList.Add(f);

                            ConnectedToOpenArea.Add(rm);
                        }
                    }

                    // now recurse to visit each room that this is connected to
                    FindConnectedRooms(rm, VisitedList, ConnectedToOpenArea, CandidateList);
                }

            }

        }
    }
}
