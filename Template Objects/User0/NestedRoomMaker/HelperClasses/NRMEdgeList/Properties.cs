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
        public List<NRMRoomEdge> NRMRoomEdgeList { get; set; } = new List<NRMRoomEdge>();

    }
}
