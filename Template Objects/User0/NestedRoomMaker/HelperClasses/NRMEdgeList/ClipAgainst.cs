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
        // Clip the parameter against us. We will find the intersection of the argument 
        // and us and replace the argument with the intersection

        public List<NRMEdgeList> ClipAgainst(NRMEdgeList target)
        {
            List<NRMEdgeList> result = new List<NRMEdgeList>();

            // if the target doesn't intersect with us return
            if (!Intersects(target)) {
                result.Add(target);
                return result;
            }

            // find the intersection of these two and replace the child with that intersection
            List<List<PolygonEdge>> list1 = PolygonEdge
                .FindIntersectionPolygons(
                NRMRoomEdgeList.ToList<PolygonEdge>(), 
                target.NRMRoomEdgeList.ToList<PolygonEdge>());          

            foreach (List<PolygonEdge> el in list1)
            {
                result.Add(new NRMEdgeList(el));
            }

            return result;    
        }

    }
}
