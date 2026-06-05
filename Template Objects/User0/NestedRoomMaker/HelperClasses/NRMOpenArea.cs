using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public class NRMOpenArea
    {

        // The open area flag is used to identify an open area via a bitflag (1,2,4,8). 
        // we use this bitflag to identify both the rooms and the edges that connect to this
        // open area. When we're selecting doors to form a fully navigable space we have 
        // to ensure that if a room connects to more than one open area there will be 
        // a door that can reach them.
        // These values are assigned per layer after the final set of open areas are created

        public int IDFlag { get; set; }

        public List<NRMRoomEdge> WallSegments { get; set; }

        public List<NRMRoom> ConnectedRooms { get; set; }

        public NRMOpenArea(NRMEdgeList EdgeList,int IDFlag)
        {
            WallSegments = EdgeList.NRMRoomEdgeList;
            this.IDFlag = IDFlag;
            ConnectedRooms = new List<NRMRoom>();
        }

    }
}
