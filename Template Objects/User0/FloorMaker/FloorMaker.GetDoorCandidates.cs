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
        /// <summary>
        /// Get the list of edges in this room that are candidates for being a door. This is used internally
        /// to randomly select a door and can be used externally to pick the edge for a door
        /// </summary>
        /// <param name="oRoom"></param>
        /// <param name="PreferOpenAreaEdge"></param>
        /// <returns></returns>
        public List<FMEdge> GetDoorCandidates(FMAssembledRoom oRoom, bool PreferOpenAreaEdge = false)
        {
            // If we want to prefer open areas just select those
            if (PreferOpenAreaEdge)
            {
                return oRoom.GetEdgeList().Where(m => m.IsOpenSpaceEdge == 1).ToList();
            }

            // return the list of edges that are candidates
            // we won't put a door in a window candidate. window candidates are the most exterior 

            return oRoom.GetEdgeList().Where(m=> m.ExteriorWindowCandidate == 0 && m.InteriorDoorCandidate == 1).ToList();
        }
    }
}
