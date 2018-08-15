using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorLayout
    {
        /// <summary>
        /// Get the list of edges in this room that are candidates for being a door. This is used internally
        /// to randomly select a door and can be used externally to pick the edge for a door
        /// </summary>
        /// <param name="oRoom"></param>
        /// <param name="PreferOpenAreaEdge"></param>
        /// <returns></returns>
        public List<FLEdge> GetDoorCandidates(FLRoom oRoom, bool PreferOpenAreaEdge = false)
        {
            // If we want to prefer open areas just select those
            if (PreferOpenAreaEdge)
            {
                return oRoom.GetEdgeList().Where(m => m.IsOpenSpaceEdge == 1).ToList();
            }

            // return the list of edges that are candidates
            return oRoom.GetEdgeList().Where(m => m.DoorCandidate == 1).ToList();
        }
    }
}
