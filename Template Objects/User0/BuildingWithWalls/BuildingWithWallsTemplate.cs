using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ShapeTemplateLib;
using ShapeTemplateLib.BasicShapes;
using ShapeTemplateLib.Templates.User0;
using static ShapeTemplateLib.BasicShapes.Panel;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// Complete multi-story building template with stairwell, exterior walls, and optional interior walls
    /// </summary>
    [HelpItem(eItemFlavor.Template, "buildingwithwalls")]
    public partial class BuildingWithWallsTemplate : TemplateBaseClass
    {
        public enum eStairWellDirection
        {
            FrontToBack,
            LeftToRight,
            BackToFront,
            RightToLeft
        };

        /// <summary>
        /// Length of the stair run
        /// </summary>
        [HelpProperty(SampleValue = "150", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int StairLength { get; set; } = 150;

        /// <summary>
        /// Width of the stairs
        /// </summary>
        [HelpProperty(SampleValue = "85", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int StairWidth { get; set; } = 85;

        /// <summary>
        /// Length of the stairwell enclosure
        /// </summary>
        public int StairWellLength { get; set; }

        /// <summary>
        /// Width of the stairwell enclosure
        /// </summary>
        public int StairWellWidth { get; set; }

        /// <summary>
        /// Height of each floor
        /// </summary>
        [HelpProperty(SampleValue = "100", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int FloorHeight { get; set; } = 100;

        /// <summary>
        /// Number of floors in the building
        /// </summary>
        [HelpProperty(SampleValue = "3", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int FloorCount { get; set; } = 3;

        /// <summary>
        /// Width of the building
        /// </summary>
        [HelpProperty(SampleValue = "400", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int BuildingWidth { get; set; } = 400;

        /// <summary>
        /// Length of the building
        /// </summary>
        [HelpProperty(SampleValue = "500", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int BuildingLength { get; set; } = 500;

        /// <summary>
        /// Thickness of the building walls
        /// </summary>
        [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int BuildingWallWidth { get; set; } = 10;

        /// <summary>
        /// Width of doors
        /// </summary>
        [HelpProperty(SampleValue = "40", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int DoorWidth { get; set; } = 40;

        /// <summary>
        /// Height of doors (default: FloorHeight - 15)
        /// </summary>
        public int DoorHeight { get; set; }

        /// <summary>
        /// Position of the stairwell in the building
        /// </summary>
        public Point2D StairwellOffset = new Point2D() { X = 300, Y = 300 };

        /// <summary>
        /// Width of windows
        /// </summary>
        [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int WindowWidth { get; set; } = 10;

        /// <summary>
        /// Height of windows
        /// </summary>
        [HelpProperty(SampleValue = "40", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int WindowHeight { get; set; } = 40;

        /// <summary>
        /// Pattern for window placement (0 = no window, 1 = window)
        /// </summary>
        public List<int> WindowPattern { get; set; } = new List<int>() { 0, 1, 1, 0, 1, 0, 0 };

        /// <summary>
        /// Direction the stairwell runs
        /// </summary>
        [HelpProperty(SampleValue = "FrontToBack", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public eStairWellDirection StairWellDirection { get; set; } = eStairWellDirection.FrontToBack;

        public BuildingWithWallsTemplate()
        {
            StairWellLength = StairLength;
            StairWellWidth = StairWidth;

            DoorHeight = FloorHeight - 15;
        }

        public override XElement Compile()
        {
            // add scaling and rotation to the root element so that blocks of objects can be placed next to each other
            // I don't think we can translate because positioning an object itself is a translation

            StairWellTemplate1 stairtempl = new StairWellTemplate1();
            stairtempl.LocalTransform = this.LocalTransform;
            stairtempl.oFrameOfReference = this.oFrameOfReference;

            stairtempl.StairOffset = new Point2D() { X = 0, Y = 0 };
            stairtempl.FloorHeight = FloorHeight;
            stairtempl.FloorCount = FloorCount;
            stairtempl.StairWidth = StairWidth;
            stairtempl.StairWellLength = StairWellLength;
            stairtempl.StairWellWidth = StairWellWidth;
            stairtempl.StairLength = StairLength;

            Group rootgroup = new Group();

            int FinalStairWellLengthInBuilding = StairWellLength;
            int FinalStairWellWidthInBuilding = StairWellWidth;

            // Add a cap for the stairs. It will need to be aligned correctly
            SingleRoomBuilding srb = new SingleRoomBuilding();
            srb.LocalTransform = this.LocalTransform;
            srb.oFrameOfReference = this.oFrameOfReference;

            srb.Height = FloorHeight;
            srb.Door.Boundary = new BoundaryRectangle(StairWellWidth - 10, srb.Height - 20);
            srb.FrontWindow.Boundary.BoundaryType = "";

            int srbAngle = 0;
            /*
             * Do translations first so we can think logically about how this is to move. Once you do a rotation then
             * further translations will be in that frame of reference along those axis. This is also what you want
             * but if you're moving and rotating its easier to think about it by doing the translations first
             */
            switch (StairWellDirection)
            {
                case eStairWellDirection.FrontToBack:
                    FinalStairWellLengthInBuilding = StairWellWidth;
                    FinalStairWellWidthInBuilding = StairWellLength;

                    srb.Width = StairWellWidth;
                    srb.Length = StairWellLength;
                    srbAngle = 0;

                    rootgroup
                        .Translate(StairwellOffset.X, 0, StairwellOffset.Y)
                        .Add(stairtempl);

                    rootgroup
                        .Translate(StairwellOffset.X + srb.Length, FloorCount * FloorHeight, StairwellOffset.Y)
                        .RotateY(srbAngle - 90)
                        .Add(srb);

                    break;
                case eStairWellDirection.RightToLeft:
                    // Flip the length and width
                    FinalStairWellLengthInBuilding = StairWellLength;
                    FinalStairWellWidthInBuilding = StairWellWidth;

                    srb.Width = FinalStairWellLengthInBuilding;
                    srb.Length = FinalStairWellWidthInBuilding;
                    srbAngle = 90;

                    rootgroup
                        .Translate(StairwellOffset.X, 0, StairWellLength + StairwellOffset.Y)
                        .RotateY(90)
                        .Add(stairtempl);

                    rootgroup
                        .Translate(StairwellOffset.X, FloorCount * FloorHeight, StairwellOffset.Y)
                        .RotateY(srbAngle - 90)
                        .Add(srb);

                    break;
                case eStairWellDirection.BackToFront:
                    FinalStairWellLengthInBuilding = StairWellWidth;
                    FinalStairWellWidthInBuilding = StairWellLength;

                    srb.Width = FinalStairWellWidthInBuilding;
                    srb.Length = FinalStairWellLengthInBuilding;
                    srbAngle = 180;

                    rootgroup.Translate(StairWellLength + StairwellOffset.X, 0, StairWellWidth + StairwellOffset.Y)
                        .RotateY(180)
                        .Add(stairtempl);

                    rootgroup.Translate(StairwellOffset.X, FloorCount * FloorHeight, StairwellOffset.Y)
                        .RotateY(srbAngle - 90)
                        .Add(srb);

                    break;
                case eStairWellDirection.LeftToRight:
                    // Flip the length and width
                    FinalStairWellLengthInBuilding = StairWellLength;
                    FinalStairWellWidthInBuilding = StairWellWidth;

                    srb.Width = FinalStairWellLengthInBuilding;
                    srb.Length = FinalStairWellWidthInBuilding;
                    srbAngle = 270;

                    rootgroup
                        .Translate(StairwellOffset.X + StairWellWidth, 0, StairwellOffset.Y)
                        .RotateY(270)
                        .Add(stairtempl);

                    rootgroup.Translate(StairwellOffset.X, FloorCount * FloorHeight, StairwellOffset.Y)
                        .RotateY(srbAngle - 90)
                        .Add(srb);

                    break;
            }

            BuildingShellTemplate btemp = new BuildingShellTemplate() { StairWellLength = FinalStairWellLengthInBuilding, StairwellWidth = FinalStairWellWidthInBuilding };
            btemp.LocalTransform = this.LocalTransform;
            btemp.oFrameOfReference = this.oFrameOfReference;

            btemp.StairWellOffset = StairwellOffset;
            btemp.FloorHeight = FloorHeight;
            btemp.FloorCount = FloorCount;
            btemp.BuildingWidth = BuildingWidth;
            btemp.BuildingLength = BuildingLength;

            btemp.StairWellOffset = StairwellOffset;
            btemp.WindowWidth = WindowWidth;
            btemp.WindowHeight = WindowHeight;
            btemp.WindowPattern = WindowPattern;
            btemp.RoofCapHeight = 30;
            btemp.BuildingWallWidth = BuildingWallWidth;

            rootgroup.TemplateList.Add(btemp);

#if false
            // Note: Interior wall generation commented out because it requires APILib.InvertGraphXml
            // which creates a circular dependency. This functionality should be moved to a separate
            // template or handled at the application level (e.g., in ConsoleTester)

            // Add walls for a single floor using helper methods
            SimpleLayout Walls = HallwayTest2(
                new List<int>() { 80, 200 }, BuildingLength - BuildingWallWidth,
                new List<int>() { 150 }, BuildingWidth - BuildingWallWidth, 50);

            SimpleLayout MultiRoomLayout = AddWalls(Walls);

            // Place walls on each floor
            for (int i = 0; i < FloorCount; i++)
            {
                SimpleLayout wall = GetSingleWallFrontToBack(BuildingLength, 4, FloorHeight - 5, true);
                wall.LocalTransform = this.LocalTransform;
                wall.oFrameOfReference = this.oFrameOfReference;

                rootgroup.Translate(0, i * FloorHeight + 3, 200).TemplateList.Add(wall);
            }
#endif

            return rootgroup.CompileLevel(this.oFrameOfReference);
        }

        /// <summary>
        /// Add walls and doors to a multi-room layout
        /// </summary>
        public SimpleLayout AddWalls(SimpleLayout MultiRoomLayout)
        {
            MultiRoomLayout.LocalTransform = this.LocalTransform;
            MultiRoomLayout.oFrameOfReference = this.oFrameOfReference;

            // add a door shape
            MultiRoomLayout.BoundaryRectangleList.Add(new BoundaryRectangle() { Height = DoorHeight, Width = DoorWidth });

            // get the list of all rooms, sets of connected edges
            List<List<Edge>> roomList = MultiRoomLayout.GetConnectedEdges();

            Random r = new Random(System.DateTime.Now.Millisecond);

            // Add a single door at random to each room in the middle of a random panel
            DoorCounter = 0;
            foreach (List<Edge> room in roomList)
            {
                AddDoor(MultiRoomLayout, room, r);
            }

            return MultiRoomLayout;
        }

        // Object variable used to increment a value used for creating doors
        protected int DoorCounter { get; set; }

        /// <summary>
        /// Add a door to a room
        /// </summary>
        private void AddDoor(SimpleLayout MultiRoomLayout, List<Edge> room, Random r)
        {
            // change the height and width
            foreach (Edge e in room)
            {
                e.Height = 95;
                e.Width = 2;
            }

            // Pick an edge at random and add a door
            int index = r.Next(0, room.Count);
            Edge EdgeWithDoor = room[index];
            string iid = $"ID{DoorCounter++}";

            EdgeWithDoor.HoleGroupID = iid;

            float length = MultiRoomLayout.EdgeLength(EdgeWithDoor);

            MultiRoomLayout.HoleGroupList.Add(new HoleGroup()
            {
                HoleGroupID = iid,
                HoleList = new LayoutHole[]
                {
                    new LayoutHole() {
                        HoleType ="rect",
                        HoleTypeIndex=0,
                        OffsetX=length/2 - DoorWidth / 2,
                        OffsetY =0
                    }
                }
            });
        }

        #region Helper Methods (Inlined from WallHelper)

        /// <summary>
        /// Generate a single wall from front to back
        /// </summary>
        private SimpleLayout GetSingleWallFrontToBack(int length, int width, int height, bool IncludeDoor = false)
        {
            if (IncludeDoor)
            {
                return new SimpleLayout()
                {
                    VertexList = new List<Vertex>() {
                        new Vertex() { Index = 0, X = 0, Y = 0 },
                        new Vertex() { Index = 1, X = length, Y = 0},
                    },
                    EdgeList = new List<Edge>()
                    {
                        new Edge() { Height= height, p1=0, p2=1, Width=width, HoleGroupID="door" }
                    },
                    HoleGroupList = new List<HoleGroup>()
                    {
                        new HoleGroup()
                        {
                            HoleGroupID="door",
                            HoleList = new LayoutHole[]
                            {
                              new LayoutHole() { HoleType ="rect", HoleTypeIndex =0, OffsetX =length/4, OffsetY =0 }
                            }
                        }
                    },
                    BoundaryRectangleList = new List<BoundaryRectangle>()
                    {
                        new BoundaryRectangle()
                        {
                             Height=height - 5, Width=80, BoundaryType="rectangle"
                        }
                    }
                };
            }
            else
            {
                return new SimpleLayout()
                {
                    VertexList = new List<Vertex>() {
                    new Vertex() { Index = 0, X = 0, Y = 0 },
                    new Vertex() { Index = 1, X = length, Y = 0 },
                },
                    EdgeList = new List<Edge>()
                {
                    new Edge() { Height= height, p1=0, p2=1, Width=width }
                }
                };
            }
        }

        /// <summary>
        /// Create hallway layout from explicit offsets
        /// NOTE: This method requires APILib and is currently disabled to avoid circular dependencies
        /// </summary>
        private SimpleLayout HallwayTest2(
            List<int> LookingFromSide2SideOffset,
            int InteriorBuildingWidth,
            List<int> LookingFromFront2BackOffset,
            int InteriorBuildingLength,
            int HallwayWidth)
        {
            if (HallwayWidth <= 0) return null;

            SimpleLayout ol = new SimpleLayout();

            // Add front to back hallways
            for (int i = 0; i < LookingFromFront2BackOffset.Count; i++)
            {
                AddFrontToBackEdge(ol, LookingFromFront2BackOffset[i], InteriorBuildingLength, HallwayWidth);
            }

            // Add side to side hallways
            for (int i = 0; i < LookingFromSide2SideOffset.Count; i++)
            {
                AddSideToSideEdge(ol, LookingFromSide2SideOffset[i], InteriorBuildingWidth, HallwayWidth);
            }

            // Note: This requires APILib.InvertGraphXml which creates circular dependency
            // TODO: Move this functionality to application layer or create separate utility
            // API oapi = new API();
            // APIStatus sts = oapi.APIEntry(APILib.eAPICommand.InvertGraphXml, ol.Compile());
            // XElement ele = XElement.Load(sts.OutputFile);
            // string message = "";
            // ol = new SimpleLayout();
            // ol.LoadProperties(ele, out message);

            return ol;
        }

        /// <summary>
        /// Add a side-to-side edge to the layout
        /// </summary>
        private void AddSideToSideEdge(SimpleLayout ol, int Displacement, int InteriorBuildingWidth, int HallwayWidth)
        {
            int p1 = ol.VertexList.Count;
            int p2 = p1 + 1;

            Vertex v1 = new Vertex() { Index = p1, X = 0, Y = Displacement };
            Vertex v2 = new Vertex() { Index = p2, X = InteriorBuildingWidth, Y = Displacement };

            ol.VertexList.Add(v1);
            ol.VertexList.Add(v2);

            ol.EdgeList.Add(new Edge() { p1 = p1, p2 = p2, Width = HallwayWidth, Height = 100 });
        }

        /// <summary>
        /// Add a front-to-back edge to the layout
        /// </summary>
        private void AddFrontToBackEdge(SimpleLayout ol, int Displacement, int InteriorBuildingLength, int HallwayWidth)
        {
            int p1 = ol.VertexList.Count;
            int p2 = p1 + 1;

            Vertex v1 = new Vertex() { Index = p1, X = Displacement, Y = 0 };
            Vertex v2 = new Vertex() { Index = p2, X = Displacement, Y = InteriorBuildingLength };

            ol.VertexList.Add(v1);
            ol.VertexList.Add(v2);

            ol.EdgeList.Add(new Edge() { p1 = p1, p2 = p2, Width = HallwayWidth, Height = 100 });
        }

        #endregion
    }
}
