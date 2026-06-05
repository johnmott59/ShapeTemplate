using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class NRMRoomWorkBench
    {
        /// <summary>
        /// Add the wall segments. Save only the ones that intersect with the outline
        /// </summary>
        /// <param name="list"></param>
        public void AddWallSegments(NRMEdgeList list)
        {
            WallSegments = new NRMEdgeList();

            foreach (NRMRoomEdge re in list.NRMRoomEdgeList)
            {
                // The center point of this edge should be inside this outline
                if (Outline.IsEdgeInside(re))
                {
                    WallSegments.NRMRoomEdgeList.Add(re);
                }
            }
        }
    }
}
