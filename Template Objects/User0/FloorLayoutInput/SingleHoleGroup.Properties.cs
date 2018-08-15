using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    /*
     * This class is used to contain one array of holes and the boundary structures that go with them
     * for the purposes of the FloorLayoutInput
     * we're using the same structurs as the simplelayout but not in the same way. The simplelayout
     * can have many hole groups, because shapes can be shared between hole groups. This container
     * will only have one, because we expect that shapes will not be shared.
     */
    public partial class SingleHoleGroup : ILoadAndSaveProperties
    {
        public HoleGroup oHoleGroup { get; set; }
        public BoundaryRectangle[] RectangleArray { get; set; }
        public BoundaryEllipse[] EllipseArray { get; set; }
        public BoundaryPolygon[] PolygonArray { get; set; }

   



    }
}
