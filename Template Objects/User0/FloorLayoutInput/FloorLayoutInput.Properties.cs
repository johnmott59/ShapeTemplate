using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorLayoutInput : CompilableRoot
    {
        public SingleHoleGroup Outline { get; set; }
        public SingleHoleGroup OpenArea { get; set; }

        public LineSegment[] WallSegmentArray { get; set; }
    }
}
