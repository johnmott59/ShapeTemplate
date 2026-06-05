using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// Input to generate a floor layout, containing a tree of open areas and outlines. 
    /// </summary>
    public partial class NestedRoomMaker
    {
        public NestedRoomMaker()
        {

        }

        public NestedRoomMaker(SingleHoleGroup Outline,LineSegment[] InteriorWalls)
        {
            this.OutlineType = eFLInput3OutsideType.Outline;
            this.HoleGroup = Outline;
            this.InteriorWallSegmentArray = InteriorWalls;
        }

        public NestedRoomMaker(SingleHoleGroup OpenArea)
        {
            this.OutlineType = eFLInput3OutsideType.OpenArea ;
            this.HoleGroup = OpenArea;
        }

        public NestedRoomMaker AddChildOutline(SingleHoleGroup Outline)
        {
            NestedRoomMaker tmp = new NestedRoomMaker(Outline);
            tmp.OutlineType = eFLInput3OutsideType.Outline;

            this.Children.Add(tmp);

            return tmp;
        }

        public NestedRoomMaker AddChildOpenArea(SingleHoleGroup OpenArea)
        {
            NestedRoomMaker tmp = new NestedRoomMaker(OpenArea);

            this.Children.Add(tmp);

            return tmp;
        }
    }
}
