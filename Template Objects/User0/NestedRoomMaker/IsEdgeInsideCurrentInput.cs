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

        /// <summary>
        /// See if an edge is inside an edge list - if a line segment is inside a polygon
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="elist"></param>
        /// <returns></returns>
        protected bool IsEdgeInsideCurrentInput(PolygonEdge pe)
        {
            foreach (NRMEdgeList el in oWorkBench.CurrentLevelInputPolygons)
            {
                if (el.IsEdgeInside(pe)) return true;
            }
            return false;
        }
    }
}
