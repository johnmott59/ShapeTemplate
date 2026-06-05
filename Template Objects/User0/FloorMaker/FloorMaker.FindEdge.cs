using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates;
using ShapeTemplateLib;
using ShapeTemplateLib.BasicShapes;
using ShapeTemplateLib.Templates.User0;
using System.Xml.Linq;
using System.Drawing;

namespace ShapeTemplateLib.Templates.User0
{
    // Rename this floorwithrooms and delete the old template
    public partial class FloorMaker : TemplateBaseClass
    {

        /// <summary>
        /// Find an edge
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public FMEdge FindEdge(PointF p1, PointF p2)
        {
            // Find each vertex
            Vertex v1 = this.VertexList.Where(m => m.X == p1.X && m.Y == p1.Y).FirstOrDefault();
            if (v1 == null) return null;

            Vertex v2 = this.VertexList.Where(m => m.X == p2.X && m.Y == p2.Y).FirstOrDefault();
            if (v2 == null) return null;

            // Find the edge with these two indices, in either order
            return this.EdgeList.Where(m => (m.p1 == v1.Index && m.p2 == v2.Index) || (m.p1 == v2.Index && m.p2 == v1.Index)).FirstOrDefault();
           
        }
    }
}
