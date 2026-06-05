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

        /*
         * Get a list of edge objects for this room
         */
        public List<FMEdge> GetEdgeList() 
        {
            List<FMEdge> list = new List<FMEdge>();
            foreach (int ndx in this.EdgeIndexList)
            {
                // Retrieve this room from the list in the floor
                list.Add(oFloor.EdgeList.Where(m => m.Index == ndx).FirstOrDefault());
            }
            return list;
         }
    }

 
}
