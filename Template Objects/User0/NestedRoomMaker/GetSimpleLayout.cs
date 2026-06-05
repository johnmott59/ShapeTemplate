using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;
using System.Drawing;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// Retrieve the current items as a simple layout. This doesn't make an attempt to create rooms, 
    /// just retrieve the current list of line segments.
    /// </summary>

    public partial class NestedRoomMaker
    {
        public SimpleLayout GetSimpleLayout()
        {
            SimpleLayout sl = new SimpleLayout();
            /*
             * Retrieve all edges from all levels
             */
            List<NRMEdgeList> AllEdges = new List<NRMEdgeList>();
            AddAllEdges(AllEdges);

            return _getit(AllEdges);

        }

        protected void AddAllEdges(List<NRMEdgeList> EdgeList) 
        {
            // add children
            foreach (NestedRoomMaker fli in this.Children)
            {
                fli.AddAllEdges(EdgeList);
            }

            // add our edges and wall segments

            foreach (var el in this.oWorkBench.CurrentLevelInputPolygons)
            {
                EdgeList.Add(el);
            }

            EdgeList.Add(oWorkBench.InnerWallSegments);

        }
    }
}
