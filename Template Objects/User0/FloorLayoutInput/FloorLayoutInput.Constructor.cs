using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorLayoutInput : CompilableRoot
    {
        public FloorLayoutInput()
        {
            Outline = new SingleHoleGroup();
            OpenArea = new SingleHoleGroup();
            WallSegmentArray = new LineSegment[0];
        }

    }
}
