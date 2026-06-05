using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {

        /// <summary>
        /// Recursive routine to find polygon edges that are all connected
        /// </summary>
        /// <param name="AllEdges"></param>
        /// <param name="UsedEdges"></param>
        /// <param name="ResultingPolygon"></param>
        /// <param name="Startedge"></param>
        /// <param name="CurrentEdge"></param>
        private static void BuildPolygon(List<PolygonEdge> AllEdges,
            List<PolygonEdge> UsedEdges,
            List<PolygonEdge> ResultingPolygon,
            PolygonEdge Startedge,
            PolygonEdge CurrentEdge)
        {
            // Get all edges that connect to this edge 
            // this polygon
            List<PolygonEdge> list = AllEdges.Where(m =>
                       (!UsedEdges.Contains(m))
                        && (m.From == CurrentEdge.From
                        || m.From == CurrentEdge.To
                        || m.To == CurrentEdge.From
                        || m.To == CurrentEdge.To)
                    ).ToList();

            foreach (PolygonEdge candidate in list.Where(m=> !UsedEdges.Contains(m)))
            {
                // Add this to the polygon and to the list of used edges
                // once something has been added to the used list we don't try to add it again

                ResultingPolygon.Add(candidate);
                UsedEdges.Add(candidate);
                // Now recurse
                BuildPolygon(AllEdges, UsedEdges, ResultingPolygon, Startedge, candidate);
            }
        }

    }
}
