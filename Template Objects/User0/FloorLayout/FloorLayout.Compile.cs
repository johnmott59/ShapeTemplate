using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorLayout
    {
    
        public override XElement Compile()
        {
                return Compile(1, 1);
        }

        public XElement Compile(float HorizontalScale, float VerticalScale)
        {
            List<Point2D> List = new List<Point2D>();
  
            // Move the points into a simple layout struct
            SimpleLayout oSimplyLayout = new SimpleLayout();
            oSimplyLayout.HorizontalScale = HorizontalScale;
            oSimplyLayout.VerticalScale = VerticalScale;

            // copy the vertex list
            oSimplyLayout.VertexList = new List<Vertex>(this.VertexList);

            foreach (var edge in EdgeList)
            {
                oSimplyLayout.EdgeList.Add(
                    new Edge() {
                        p1 = edge.p1,
                        p2 = edge.p2,
                        Width = (int)edge.Width,
                        Height = (int)edge.Height,
                        ID = edge.ID,
                        HoleGroupID = edge.HoleGroupID
                    });
            }

            /*
             * Compile this simple layout and return its value
             */
            return oSimplyLayout.Compile();

        }
    }
}
