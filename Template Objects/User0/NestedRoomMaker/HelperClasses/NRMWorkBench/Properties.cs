using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class NRMRoomWorkBench
    {
        // This defines what type the 'outside' element of this group is, used for clipping 

        public NRMEdgeList Outline { get; set; }
        public List<NRMEdgeList> OpenArea { get; set; } = new List<NRMEdgeList>();
        public NRMEdgeList WallSegments { get; set; }
        public List<NRMRoom> RoomList { get; set; } = new List<NRMRoom>();

        public List<NRMOpenArea> OpenAreaList { get; set; } = new List<NRMOpenArea>();
    }
}
