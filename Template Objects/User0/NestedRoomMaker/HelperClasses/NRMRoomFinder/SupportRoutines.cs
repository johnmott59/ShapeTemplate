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
       

        //----------------------------
        bool CompletedRoomListContains(NRMRoom newRoom)
        {
            // see if any rooms contain this exact set of edges
            foreach (NRMRoom r in CompletedRoomList)
            {
                if (IdenticalEdgeList(r.WallSegments, newRoom.WallSegments)) return true;
            }

            return false;
        }

        // See if two edge lists are identical
        bool IdenticalEdgeList(List<NRMRoomEdge> list1, List<NRMRoomEdge> list2)
        {
            // if they don't contain the same number they aren't the same
            if (list1.Count != list2.Count) return false;

            // See if every edge in list1 is in list2

            foreach (NRMRoomEdge p1 in list1)
            {
                if (!list2.Contains(p1)) return false;
            }

            // see if every edge in list2 is in list1
            foreach (NRMRoomEdge p2 in list2)
            {
                if (!list1.Contains(p2)) return false;

            }

            return true;
        }
    }
}
