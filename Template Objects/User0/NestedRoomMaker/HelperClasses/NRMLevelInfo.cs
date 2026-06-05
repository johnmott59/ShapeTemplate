using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class NRMLevelInfo
    {
        public List<NRMRoom> RoomList { get; set; } = new List<NRMRoom>();
        public List<NRMOpenArea> OpenAreaList { get; set; } = new List<NRMOpenArea>();
    }
}
