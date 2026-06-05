using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib;
using ShapeTemplateLib.BasicShapes;
using ShapeTemplateLib.Templates.User0;
using static ShapeTemplateLib.BasicShapes.Panel;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// Template for generating a stairwell that spans multiple floors
    /// </summary>
    public partial class StairWellTemplate1 : TemplateBaseClass
    {
        /// <summary>
        /// Number of floors the stairwell connects
        /// </summary>
        [HelpProperty(SampleValue = "3", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int FloorCount { get; set; } = 3;

        /// <summary>
        /// Height of each floor
        /// </summary>
        [HelpProperty(SampleValue = "100", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public float FloorHeight { get; set; } = 100;

        /// <summary>
        /// Number of steps per stair run
        /// </summary>
        [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int StairCount { get; set; } = 10;

        /// <summary>
        /// Width of the stairs
        /// </summary>
        [HelpProperty(SampleValue = "30", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int StairWidth { get; set; } = 30;

        /// <summary>
        /// Length of the stair run
        /// </summary>
        [HelpProperty(SampleValue = "100", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int StairLength { get; set; } = 100;

        /// <summary>
        /// Length of the stairwell enclosure
        /// </summary>
        [HelpProperty(SampleValue = "200", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public float StairWellLength { get; set; } = 200;

        /// <summary>
        /// Width of the stairwell enclosure
        /// </summary>
        [HelpProperty(SampleValue = "100", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public float StairWellWidth { get; set; } = 100;

        /// <summary>
        /// Offset position of the stairs within the stairwell
        /// </summary>
        public Point2D StairOffset { get; set; } = new Point2D() { X = 0, Y = 0 };

        public override XElement Compile()
        {
            // Create a set of stairs
            StraightStairs ss = new StraightStairs();

            ss.StairCount = StairCount;
            ss.Width = StairWidth;
            // Set the rise and the run to match the length and height
            ss.Rise = (int)(FloorHeight / StairCount);
            ss.Run = (int)StairLength / StairCount;
            ss.VerticalDistance = ss.Rise;
            ss.HorizontalDistance = ss.Run;
            ss.LocalTransform = Matrix4x4.Translation(StairOffset.X , 0, StairOffset.Y );

            Group[] FloorList = new Group[FloorCount];

            // Handle first floor

            FloorList[0] = new Group();
            FloorList[0].TemplateList.Add(ss);

            // Handle middle floors

            for (int i = 1; i < FloorCount; i++)
            {
                FloorList[i] = FloorList[0].GetChildGroupWithTemplate(Group.eTransformType.TranslateY, FloorHeight * i, ss);
            }

            // Handle top floor
            Group TopFloor = FloorList[0].GetChildGroup(Group.eTransformType.TranslateY, FloorHeight * FloorCount);

            // Create a floor with space for the stairways
            FlatMesh fm = new FlatMesh();
            fm.Boundary = new BoundaryRectangle(StairWellLength, StairWellWidth);

            // rotate the flat mesh to flatten it out and translate it to move it into place
            fm.LocalTransform = Matrix4x4.RotateX(90);

            fm.HoleList = new Hole[]
            {
                new Hole() { Boundary=new BoundaryRectangle(StairLength,StairWidth), Offset=new Point3D(StairOffset.X,StairOffset.Y,0) }  // space for stairs
            };
            // Place the floor on 2nd, 3rd and 4th

            for (int i = 1; i < FloorCount; i++)
            {
                FloorList[i].MeshBaseClassList.Add(fm);
            }

            // Add a mesh to the top floor
            TopFloor.MeshBaseClassList.Add(fm);

            // Compile this based on the current frame of reference of the template

            return FloorList[0].CompileLevel(this.oFrameOfReference);
        }
    }
}
