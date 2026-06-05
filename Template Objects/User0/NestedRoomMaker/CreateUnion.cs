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
        // Find the union of a set of boundary polygons
        protected BoundaryPolygon CreateUnion(BoundaryPolygon bp1, BoundaryPolygon bp2)
        {
            return new BoundaryPolygon();
        }
    }
}
