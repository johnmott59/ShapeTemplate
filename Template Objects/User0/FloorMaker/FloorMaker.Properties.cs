using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorMaker
    {
        // All room edges have vertices and are specified via their index
        public Vertex[] VertexList { get; set; }

        // Rooms have edges that can be shared and are referred to by index. The list of edges is kept here.
        public FMEdge[] EdgeList { get; set; }

        // This is the list of rooms on this floor
        public FMAssembledRoom[] AssembledRoomList { get; set; }

        // this is the list of open spaces on this floor
        public FMDestinationRoom[] DestinationRoomList { get; set; }

        // this is the list of exteriors for this floor. A floor can be composed of separated, distinct rooms
        public FMOuterRoom[] OuterRoomList { get; set; }

    }
}
