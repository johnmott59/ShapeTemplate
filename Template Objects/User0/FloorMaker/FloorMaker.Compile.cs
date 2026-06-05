using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorMaker
    {
    
        public override XElement Compile()
        {
                return Compile(1, 1);
        }

        // This entry point allows a caller to get the simple layout version of the floor
        public SimpleLayout GetSimpleLayout(float HorizontalScale = 1, float VerticalScale = 1)
        {
            // Move the points into a simple layout struct
            SimpleLayout oSimpleLayout = new SimpleLayout();
            oSimpleLayout.HorizontalScale = HorizontalScale;
            oSimpleLayout.VerticalScale = VerticalScale;
            
            // pass down the location information
            oSimpleLayout.oFrameOfReference = this.oFrameOfReference;
            oSimpleLayout.LocalTransform = this.LocalTransform;

            // copy the vertex list
            oSimpleLayout.VertexList = new List<Vertex>(this.VertexList);

            foreach (var edge in EdgeList)
            {
                oSimpleLayout.EdgeList.Add(
                    new Edge()
                    {
                        p1 = edge.p1,
                        p2 = edge.p2,
                        Width = (int)edge.Width,
                        Height = (int)edge.Height,
                        ID = edge.ID,
                        HoleGroupID = edge.HoleGroupID
                    });
            }

            return oSimpleLayout;

        }

        public XElement Compile(float HorizontalScale, float VerticalScale)
        {

            return GetSimpleLayout(HorizontalScale, VerticalScale).Compile();

        }
    }
}
