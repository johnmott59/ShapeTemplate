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

            // Create default HoleGroup definitions for any HoleGroupIDs used by edges
            var usedHoleGroupIds = EdgeList.Where(e => !string.IsNullOrEmpty(e.HoleGroupID))
                                           .Select(e => e.HoleGroupID)
                                           .Distinct()
                                           .ToList();

            foreach (var holeGroupId in usedHoleGroupIds)
            {
                // Add a default rectangular boundary for the door opening (3 feet wide x 7 feet tall)
                var doorRect = new BoundaryRectangle()
                {
                    Width = 36,   // 3 feet
                    Height = 84,  // 7 feet
                    ZDepth = 0
                };
                oSimpleLayout.BoundaryRectangleList.Add(doorRect);
                int rectIndex = oSimpleLayout.BoundaryRectangleList.Count - 1;

                // Create a hole that references the boundary rectangle
                // The hole is centered horizontally on the wall and starts at floor level
                var doorHole = new LayoutHole()
                {
                    HoleType = "rect",
                    HoleTypeIndex = rectIndex,
                    OffsetX = -18,  // Center the 36" door (half of 36)
                    OffsetY = 0     // At floor level
                };

                var holeGroup = new HoleGroup()
                {
                    HoleGroupID = holeGroupId,
                    HoleList = new LayoutHole[] { doorHole }
                };

                oSimpleLayout.HoleGroupList.Add(holeGroup);

                System.Diagnostics.Debug.WriteLine($"Created HoleGroup '{holeGroupId}' with 36\"x84\" door opening");
            }

            return oSimpleLayout;

        }

        public XElement Compile(float HorizontalScale, float VerticalScale)
        {

            return GetSimpleLayout(HorizontalScale, VerticalScale).Compile();

        }
    }
}
