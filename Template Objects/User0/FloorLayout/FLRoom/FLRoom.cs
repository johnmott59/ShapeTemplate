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
    public partial class FLRoom : ILoadAndSaveProperties
    {
        // The heirarchy of floor/room/edges must be maintained so that access to point data can be kept
        public FloorLayout oFloor { get; set; }

        // These fields are set by the room algorithm to indicate whether or not a room connects to an open area
        // or is one away from an open area. This can be used by the door finder to locate edges which could have
        // doors. 
        public int ConnectsToOpenArea { get; set; } 
        public int BackRoom { get; set; } 

        // list of the edge indices used for this room
        public int[] EdgeIndexList { get; set; }

        /*
         * Get a list of edge objects for this room
         */
        public List<FLEdge> GetEdgeList() 
        {
            List<FLEdge> list = new List<FLEdge>();
            foreach (int ndx in this.EdgeIndexList)
            {
                // Retrieve this room from the list in the floor
                list.Add(oFloor.EdgeList.Where(m => m.Index == ndx).FirstOrDefault());
            }
            return list;
         }
    }

 
}
