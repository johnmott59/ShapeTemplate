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
        public NRMEdgeList()
        {

        }

        public NRMEdgeList(List<NRMRoomEdge> relist)
        {
            this.NRMRoomEdgeList = relist;
        }


        public NRMEdgeList(List<PolygonEdge> pelist)
        {
            NRMRoomEdgeList = new List<NRMRoomEdge>();
            foreach (PolygonEdge pe in pelist)
            {
                NRMRoomEdgeList.Add(new NRMRoomEdge(pe));
            }
        }

        // Add a boundary polygon as an edge list

        public NRMEdgeList(BoundaryPolygon bp)
        {
            List<PolygonEdge> pelist = PolygonEdge.FromBoundaryPolygon(bp);

            NRMRoomEdgeList = new List<NRMRoomEdge>();
            foreach (PolygonEdge pe in pelist)
            {
                NRMRoomEdgeList.Add(new NRMRoomEdge(pe));
            }
        }

    }
}
