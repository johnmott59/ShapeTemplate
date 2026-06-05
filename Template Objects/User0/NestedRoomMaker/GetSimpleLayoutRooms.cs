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
        public SimpleLayout GetSimpleLayoutRooms()
        {
            /*
             * Retrieve all edges from all levels
             */
            List<NRMEdgeList> AllEdges = new List<NRMEdgeList>();
            AddAllRoomEdges(AllEdges);

            return _getit(AllEdges);

        }


        protected SimpleLayout _getit(List<NRMEdgeList> AllEdges)
        {
            SimpleLayout sl = new SimpleLayout();
            /*
             * Locate all unique points in the roomedgegroup
             */
            sl.VertexList = new List<Vertex>();
            List<PointF> vList = new List<PointF>();
            sl.EdgeList = new List<Edge>();

            List<NRMRoomEdge> pEdgeList = new List<NRMRoomEdge>();
            int VertexIndex = 0;
            foreach (NRMEdgeList el in AllEdges)
            {
                foreach (NRMRoomEdge pEdge in el.NRMRoomEdgeList)
                {
                    // keep a unique list of points
                    if (!vList.Contains(pEdge.From))
                    {
                        vList.Add(pEdge.From);
                        sl.VertexList.Add(new Vertex() { Index = VertexIndex++, X = pEdge.From.X, Y = pEdge.From.Y });
                    }
                    if (!vList.Contains(pEdge.To))
                    {
                        vList.Add(pEdge.To);
                        sl.VertexList.Add(new Vertex() { Index = VertexIndex++, X = pEdge.To.X, Y = pEdge.To.Y });
                    }

                    // See if the endpoints in this edge object match ones in our list. If two edges
                    // have the same from and to values they are the same edge
                    int count = pEdgeList.Where(m => m.SameEndPoints(pEdge)).Count();
                    if (count == 0)
                    {
                        pEdgeList.Add(pEdge);

                        // Add this edge to the simple layout 
                        sl.EdgeList.Add(new Edge()
                        {
                            p1 = vList.IndexOf(pEdge.From),
                            p2 = vList.IndexOf(pEdge.To),
                            Height = (int)pEdge.Height,
                            Width = (int)pEdge.Width,
                        });
                    }
                }
            }

            return sl;
        }




   


    }
}
