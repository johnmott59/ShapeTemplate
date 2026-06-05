using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class NRMRoomFinder
    {
        // Add the current edge list to the set of rooms, avoiding duplicates and overlap
        bool AddCurrentEdgeListToRoomList()
        {
            NRMRoom rNew = new NRMRoom()
            {
                WallSegments = new List<NRMRoomEdge>(CurrentEdgeList.Reverse())
            };
            /*
             * Make sure there are no interior wall segments that are inside this room; that would indicate that we had
             * enclosed a wall section, and a room doesn't contain any wall sections
             */
             foreach(NRMRoomEdge p in AllEdgeList.Except(rNew.WallSegments).Where(m=>m.IsInteriorWallSection))
            {
                if (NRMRoomEdge.IsPointInsidePolygon(p.CenterPoint, rNew.WallSegments))
                {
                    return false;
                }
            }

            // if this room isn't already in the completed room list then add it

            if (!CompletedRoomListContains(rNew))
            {
                // increment the room count for each edge that is in a room
                rNew.WallSegments.ForEach(m => m.RoomCount++);

                CompletedEdgeRoomList.Add(rNew);
            }


            return false;
        }

    }
}
