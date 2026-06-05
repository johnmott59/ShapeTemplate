using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// Input to generate a floor layout, containing a tree of open areas and outlines. 
    /// </summary>

    public partial class NRMEdgeList
    {
        // This will split our edges against the edges in a target, creating new line segments in each

        public void SplitAgainst(NRMEdgeList target)
        {
            NRMRoomEdge.SplitEdgesAtIntersection(NRMRoomEdgeList, target.NRMRoomEdgeList);
        }

    }
}
