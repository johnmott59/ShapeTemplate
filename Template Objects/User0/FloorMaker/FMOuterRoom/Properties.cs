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
    public partial class FMOuterRoom : ILoadAndSaveProperties
    {
        // The heirarchy of floor/room/edges must be maintained so that access to point data can be kept
        public FloorMaker oFloor { get; set; }

        // list of the edge indices used for this room
        public int[] EdgeIndexList { get; set; }

    }
}
