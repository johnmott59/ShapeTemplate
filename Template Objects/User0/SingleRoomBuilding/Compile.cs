using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SingleRoomBuilding
    {
        public override XElement Compile()
        {
            
            // Compilation for this template is the same as generating the properties
            /*
             * Create a simple layout object based on our parameters
             */
            SimpleLayout oSimpleLayout = new SimpleLayout()
            {
                HorizontalScale = HorizontalScale,
                VerticalScale = VerticalScale
            };
            // Create 4 vertices
            oSimpleLayout.VertexList = new List<Vertex>()
            {
                new Vertex() { Index=1, X=0, Y=0 },
                new Vertex() { Index=2, X=Width, Y=0 },
                new Vertex() { Index=3, X=Width, Y=Length },
                new Vertex() { Index=4, X=0, Y=Length }
            };

            oSimpleLayout.EdgeList = new List<Edge>()
            {
                new Edge() { Height= (int) Height, p1=1,p2=2, Width=(int)Thickness, ID="e1", HoleGroupID= "FrontID" },
                new Edge() { Height= (int) Height, p1=2,p2=3, Width=(int)Thickness, ID="e2", HoleGroupID="RightID" },
                new Edge() { Height= (int) Height, p1=3,p2=4, Width=(int)Thickness, ID="e3",  HoleGroupID="RearID" },
                new Edge() { Height= (int) Height, p1=4,p2=1, Width=(int)Thickness, ID="e4", HoleGroupID="LeftID" },
            };
            /*
             * Add each of the holes that are defined into the rectangle list, saving their indices
             */
            if (Door.Boundary.BoundaryType == "rectangle" || FrontWindow.Boundary.BoundaryType == "rectangle")
            {
                List<LayoutHole> FrontHoleList = new List<LayoutHole>();
                if (Door.Boundary.BoundaryType == "rectangle")
                {
                    oSimpleLayout.BoundaryRectangleList.Add((BoundaryRectangle) Door.Boundary);
                    FrontHoleList.Add(new LayoutHole()
                    {
                        HoleType = "rect",
                        HoleTypeIndex = oSimpleLayout.BoundaryRectangleList.Count - 1,
                        OffsetX = Door.Offset.X,
                        OffsetY = Door.Offset.Y
                    });
                }

                if (FrontWindow.Boundary.BoundaryType == "rectangle")
                {
                    oSimpleLayout.BoundaryRectangleList.Add((BoundaryRectangle) FrontWindow.Boundary);
                    FrontHoleList.Add(new LayoutHole()
                    {
                        HoleType = "rect",
                        HoleTypeIndex = oSimpleLayout.BoundaryRectangleList.Count - 1,
                        OffsetX = FrontWindow.Offset.X,
                        OffsetY = FrontWindow.Offset.Y
                    });
                }

                oSimpleLayout.HoleGroupList.Add(new HoleGroup()
                {
                    HoleGroupID = "FrontID",
                    HoleList = FrontHoleList.ToArray()
                });
            }

            // Add the remainder of the holes

            AddHole(LeftWindow, "LeftID", oSimpleLayout);
            AddHole(RightWindow, "RightID", oSimpleLayout);
            AddHole(RearWindow, "RearID", oSimpleLayout);

            // Add a roof panel

            string[] DescriptorList = new string[]
            {
                "ee1.l.f",
                "ee1.r.f",
                "ee2.r.f",
                "ee3.r.f",
            };

            // Define a horizontal panel for the roof
            oSimpleLayout.HorizontalPanelList.Add(new HorizontalPanel()
            {
                Height = Height + RoofOffset,
                Thickness = Thickness,
                DescriptorList = DescriptorList,
            });
            
            return oSimpleLayout.Compile();

        }

        protected void AddHole(Hole oHole, string HoleGroupID, SimpleLayout oSimpleLayout)
        {
            if (oHole.Boundary.BoundaryType != "rectangle") return;
            
            oSimpleLayout.BoundaryRectangleList.Add((BoundaryRectangle) oHole.Boundary);

            List<LayoutHole> HoleList = new List<LayoutHole>();
            HoleList.Add(new LayoutHole()
            {
                HoleType = "rect",
                HoleTypeIndex = oSimpleLayout.BoundaryRectangleList.Count - 1,
                OffsetX = oHole.Offset.X,
                OffsetY = oHole.Offset.Y
            });
                
            oSimpleLayout.HoleGroupList.Add(new HoleGroup()
            {
                HoleGroupID = HoleGroupID,
                HoleList = HoleList.ToArray()
            });
            
        }
    }
}
