using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SimpleLayout 
    {
        /// <summary>
        /// Given a set of labaelled consecutive edges, split one of the edges in the list. in order to 
        /// maintain the list as consecutive we will add the new edge to the layout as the last edge but
        /// insert it into the consecutive lists in the spot where it belongs. This is all part of managing
        /// a set of edges that will be used for horizontal panels
        /// </summary>
        /// <param name="ConsecutiveEdgeList"></param>
        /// <param name="index"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public bool SplitEdgeInConsecutiveList(List<Edge> ConsecutiveEdgeList,int index,int width)
        {
            if (index >= ConsecutiveEdgeList.Count) return false;

            Edge ed = ConsecutiveEdgeList[index];
            if (EdgeLength(ed) < width) return false;

            int nextindex = VertexList.Max(m => m.Index) + 1;

            Tuple<Vertex, Vertex> newpoints = GetHoleCoordinates(ed, width, .5F);

            // add the new points
            newpoints.Item1.Index = nextindex++;
            VertexList.Add(newpoints.Item1);

            newpoints.Item2.Index = nextindex;
            VertexList.Add(newpoints.Item2);

            Edge newEdge = new Edge() { Height = ed.Height, p1 = newpoints.Item2.Index, p2 = ed.p2, ID=ed.ID };

            // add to the list of edges for this layout
            EdgeList.Add(newEdge);

            // insert into the ordered list at the correct point
            ConsecutiveEdgeList.Insert(index+1, newEdge);
        

            // modify existing edge and insert new edge
            ed.p2 = newpoints.Item1.Index;

            return true;        

        }

      

    }
}
