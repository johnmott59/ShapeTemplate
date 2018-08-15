using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorLayout
    {

        /// <summary>
        /// Get the next available index for a vertex.
        /// </summary>
        /// <returns></returns>
        protected int NextVertexIndex()
        {
            if (VertexList == null) return 0;

            Vertex v = VertexList.OrderByDescending(m => m.Index).FirstOrDefault();

            if (v == null) return 0;

            return v.Index + 1;
        }

        /// <summary>
        /// Get the next available index for an edge
        /// </summary>
        /// <returns></returns>
        protected int NextEdgeIndex()
        {
            if (EdgeList == null) return 0;

            FLEdge e = EdgeList.OrderByDescending(m => m.Index).FirstOrDefault();

            if (e == null) return 0;

            return e.Index + 1;
        }
    }
}
