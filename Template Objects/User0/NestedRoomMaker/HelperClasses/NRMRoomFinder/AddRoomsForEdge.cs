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
        public void AddRoomsForEdge(NRMRoomEdge pCurrentEdge,eStartPosition StartPosition)
        {
            // Initialize the edge list with the current edge

            CurrentEdgeList = new Stack<NRMRoomEdge>();

            // Inittialise
            CompletedEdgeRoomList = new List<NRMRoom>();

            // Remember the starting edge
            StartingEdge = pCurrentEdge;
            StartingPoint = StartPosition == eStartPosition.FromPoint ? StartingEdge.From : StartingEdge.To;

            CurrentEdgeList.Push(pCurrentEdge);

            Recurse(pCurrentEdge, StartPosition, 1);
            /*
             * We should now have one room.
             */
            if (CompletedEdgeRoomList.Count > 0)
            {
                // Add these rooms to the completed room list
                CompletedRoomList.AddRange(CompletedEdgeRoomList);
            }
        }
    }
}
