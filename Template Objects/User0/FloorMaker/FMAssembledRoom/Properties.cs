using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    /*
     * A room is a list of edges and properties about that room.
     */
    public partial class FMAssembledRoom : ILoadAndSaveProperties
    {
        // The heirarchy of floor/room/edges must be maintained so that access to point data can be kept
        public FloorMaker oFloor { get; set; }

        // These fields are set by the room algorithm to indicate whether or not a room connects to an open area
        // or is one away from an open area. This can be used by the door finder to locate edges which could have
        // doors. 
        public int ConnectsToOpenArea { get; set; } 
        public int BackRoom { get; set; } 

        // list of the edge indices used for this room
        public int[] EdgeIndexList { get; set; }

        // list of the destinatio areas that this room touches. A room can touch more than one open area, and each room
        // needs a door to each distinct open area in order to ensure that a floor is navigable

        public List<FMDestinationRoom> DestinationRoomList { get; set; } = new List<FMDestinationRoom>();
    }

 
}
