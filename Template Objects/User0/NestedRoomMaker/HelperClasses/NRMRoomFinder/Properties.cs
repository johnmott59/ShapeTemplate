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
        public List<NRMRoomEdge> AllEdgeList { get; set; }
        public Stack<NRMRoomEdge> CurrentEdgeList { get; set; }
        public NRMRoomEdge StartingEdge { get; set; }
        public PointF StartingPoint { get; set; }

        public List<List<NRMRoomEdge>> CreationSteps { get; set; }
        public bool DebugSteps { get; set; } = false;

        // There are two lists of rooms, one that we collect for each edge we process and one for all the edges we see

        // completed rooms for the edge we are processing
        public List<NRMRoom> CompletedEdgeRoomList { get; set; }

        // All the unique rooms for all the edges
        public List<NRMRoom> CompletedRoomList { get; set; }

        public enum eStartPosition
        {
            FromPoint,
            ToPoint
        }
        
    }
}
