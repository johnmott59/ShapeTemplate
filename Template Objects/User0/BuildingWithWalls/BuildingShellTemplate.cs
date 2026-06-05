using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ShapeTemplateLib;
using ShapeTemplateLib.BasicShapes;
using ShapeTemplateLib.Templates.User0;
using static ShapeTemplateLib.BasicShapes.Panel;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// Template for generating the exterior shell of a building with windows
    /// </summary>
    public partial class BuildingShellTemplate : TemplateBaseClass
    {
        /// <summary>
        /// Height of roof cap above the top floor
        /// </summary>
        [HelpProperty(SampleValue = "0", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int RoofCapHeight { get; set; } = 0;

        /// <summary>
        /// Number of floors in the building
        /// </summary>
        [HelpProperty(SampleValue = "3", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int FloorCount { get; set; } = 3;

        /// <summary>
        /// Height of each floor
        /// </summary>
        [HelpProperty(SampleValue = "100", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int FloorHeight { get; set; } = 100;

        /// <summary>
        /// Number of window columns (not currently used)
        /// </summary>
        public int WindowCount { get; set; } = 4;

        /// <summary>
        /// Length of the building
        /// </summary>
        [HelpProperty(SampleValue = "500", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int BuildingLength { get; set; } = 500;

        /// <summary>
        /// Width of the building
        /// </summary>
        [HelpProperty(SampleValue = "500", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int BuildingWidth { get; set; } = 500;

        /// <summary>
        /// Width of the stairwell opening
        /// </summary>
        [HelpProperty(SampleValue = "300", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int StairwellWidth { get; set; } = 300;

        /// <summary>
        /// Length of the stairwell opening
        /// </summary>
        [HelpProperty(SampleValue = "300", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int StairWellLength { get; set; } = 300;

        /// <summary>
        /// Position of the stairwell opening in the building
        /// </summary>
        public Point2D StairWellOffset { get; set; } = new Point2D() { X = 0, Y = 0 };

        /// <summary>
        /// Pattern for window placement (0 = no window, 1 = window)
        /// </summary>
        public List<int> WindowPattern = new List<int>() { 1, 1, 0, 0, 0, 0, 1 };

        /// <summary>
        /// Width of each window
        /// </summary>
        [HelpProperty(SampleValue = "40", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int WindowWidth { get; set; } = 40;

        /// <summary>
        /// Height of each window
        /// </summary>
        [HelpProperty(SampleValue = "40", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int WindowHeight { get; set; } = 40;

        /// <summary>
        /// Thickness of the building walls
        /// </summary>
        [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int BuildingWallWidth { get; set; } = 10;

        public override XElement Compile()
        {
            SimpleLayout oLayout = new SimpleLayout() { VerticalScale = 1, HorizontalScale = 1 };

            // Copy frame of reference and transform of template

            oLayout.oFrameOfReference = this.oFrameOfReference;
            oLayout.LocalTransform = this.LocalTransform;

            string[] DescriptorList = new string[]
            {
                    "eid0.l.f",
                    "eid0.r.f",
                    "eid1.r.f",
                    "eid2.r.f",
            };

            float thickness = 1;

            for (int i = 1; i <= FloorCount; i++)
            {
                oLayout.HorizontalPanelList.Add(new HorizontalPanel()
                {
                    Height = FloorHeight * i,
                    Thickness = thickness,
                    DescriptorList = DescriptorList,
                    HoleGroupID = "hg2"
                });
            }

            oLayout.BoundaryRectangleList = new List<ShapeTemplateLib.BoundaryRectangle>()
            {
                new ShapeTemplateLib.BoundaryRectangle(30,30),
                new ShapeTemplateLib.BoundaryRectangle(StairwellWidth,StairWellLength)
            };
#if true
            HoleGroup hgBuildingLength = new HoleGroup()
            {
                HoleGroupID = "hgBuildingLength"
            };

            // These are the holes for the sides of the building

            List<LayoutHole> LayoutHoleList = new List<LayoutHole>();

            HolePlacementTemplate gc2 = new HolePlacementTemplate();
            gc2.AddCell("height=100,*").AddCell("width=100,*").AddBox("width=30;height=30");

            XElement xx = gc2.GetProperties();

            gc2 = new HolePlacementTemplate();
            string message = "";
            gc2.LoadProperties(xx, out message);

            List<HolePlacementTemplate.GridElement> LeafList = gc2.GetLeaves(BuildingLength,FloorHeight);

            for (int floor = 0; floor < FloorCount; floor++)
            {
                foreach (HolePlacementTemplate.GridElement leaf in LeafList)
                {
                    foreach (OffsetAndSize os in leaf.BoxOffsetAndSizeList.Where(m => m.IsContainedInCell))
                    {
                        LayoutHoleList.Add(new LayoutHole()
                        {
                            OffsetX = (float)os.Offset.X,
                            OffsetY = (float)os.Offset.Y + floor * FloorHeight,
                            HoleType = "rect",
                            HoleTypeIndex = 0
                        });
                    }
                }
            }
            // These are the hold groups for the rear of the building
            hgBuildingLength.HoleList = LayoutHoleList.ToArray();


            //-------------------------------------------
            oLayout.HoleGroupList.Add(hgBuildingLength);
            HoleGroup hgBuildingWidth = new HoleGroup()
            {
                HoleGroupID = "hgBuildingWidth"
            };


            LayoutHoleList = new List<LayoutHole>();

            gc2 = new HolePlacementTemplate();
            gc2.AddCell("height=100,*").AddCell("width=100,*").AddBox("width=30;height=30");
            LeafList = gc2.GetLeaves(BuildingWidth, FloorHeight);

            for (int floor = 0; floor < FloorCount; floor++)
            {
                foreach (HolePlacementTemplate.GridElement leaf in LeafList)
                {
                    foreach (OffsetAndSize os in leaf.BoxOffsetAndSizeList.Where(m => m.IsContainedInCell))
                    {
                        LayoutHoleList.Add(new LayoutHole()
                        {
                            OffsetX = (float)os.Offset.X,
                            OffsetY = (float)os.Offset.Y + floor * FloorHeight,
                            HoleType = "rect",
                            HoleTypeIndex = 0
                        });
                    }
                }
            }

            hgBuildingWidth.HoleList = LayoutHoleList.ToArray();
            oLayout.HoleGroupList.Add(hgBuildingWidth);


#endif
            // Hole for stairwell
            hgBuildingLength = new HoleGroup() { HoleGroupID = "hg2" };
            hgBuildingLength.HoleList = new LayoutHole[1];
            hgBuildingLength.HoleList[0] = new LayoutHole()
            {
                OffsetX = StairWellOffset.X,        // original value 0
                OffsetY = StairWellOffset.Y,        // original value 5
                HoleType = "rect",
                HoleTypeIndex = 1
            };
            oLayout.HoleGroupList.Add(hgBuildingLength);

            oLayout.VertexList.Add(new Vertex() { Index = 1, X = 0, Y = 0 });
            oLayout.VertexList.Add(new Vertex() { Index = 2, X = BuildingLength, Y = 0 });

            oLayout.EdgeList.Add(new Edge()
            {
                ID = "id0",
                p1 = 1,
                p2 = 2,
                Width = BuildingWallWidth,
                Height = FloorCount * FloorHeight + RoofCapHeight,
                HoleGroupID = "hgBuildingLength"
            });

            oLayout.VertexList.Add(new Vertex() { Index = 3, X = BuildingLength, Y = BuildingWidth });
            oLayout.EdgeList.Add(new Edge()
            {
                ID = "id1",
                p1 = 2,
                p2 = 3,
                Width = BuildingWallWidth,
                Height = FloorCount * FloorHeight + RoofCapHeight,
                HoleGroupID = "hgBuildingWidth"
            });

            oLayout.VertexList.Add(new Vertex() { Index = 4, X = 0, Y = BuildingWidth });
            oLayout.EdgeList.Add(new Edge()
            {
                ID = "id2",
                p1 = 3,
                p2 = 4,
                Width = BuildingWallWidth,
                Height = FloorCount * FloorHeight + RoofCapHeight,
                HoleGroupID = "hgBuildingLength"
            });

            return oLayout.Compile();
        }
    }
}
