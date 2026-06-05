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
        public List<LineSegment> GetLineSegmentList()
        {
            Vertex vFrom;
            Vertex vTo;

            List<LineSegment> list = new List<LineSegment>();
            foreach(Edge e in this.EdgeList)
            {
                vFrom = this.VertexList.Where(m => m.Index == e.p1).FirstOrDefault();
                vTo = this.VertexList.Where(m => m.Index == e.p2).FirstOrDefault();

                list.Add(new LineSegment()
                {
                    From = new Point2D() { X = vFrom.X, Y = vFrom.Y },
                    To = new Point2D() { X = vTo.X, Y = vTo.Y },
                    Thickness = e.Width
                });
            }

            return list;
        }

    }
}
