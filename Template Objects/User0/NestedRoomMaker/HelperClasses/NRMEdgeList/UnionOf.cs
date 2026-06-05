using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    // This is a list of edges that used in polygon processing, although they may not form a polygon

    public partial class NRMEdgeList
    {
        // Get the union of this polygon and another. return null on failure
        public List<NRMEdgeList> UnionOf(NRMEdgeList target)
        {
            // if these do not overlap return error
            List<List<PolygonEdge>> result = PolygonEdge
                .FindUnionPolygons(this.NRMRoomEdgeList.ToList<PolygonEdge>(),
                target.NRMRoomEdgeList.ToList<PolygonEdge>());

            if (result.Count == 0) return null;

            List<NRMEdgeList> list = new List<NRMEdgeList>();

            foreach (var l in result)
            {
                list.Add(new NRMEdgeList(l));
            }

            return list;
        }
    }
}
