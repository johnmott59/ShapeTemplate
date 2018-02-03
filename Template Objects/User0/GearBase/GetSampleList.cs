using System;
using System.Collections.Generic;
using ShapeTemplateLib.Templates.User0;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class GearBase : IGetSample
    {
        /// <summary>
        /// Return a list of sample instances
        /// </summary>
        /// <returns></returns>
        public List<SampleInstance> GetSampleList()
        {
            GearBase gb = new GearBase();
            gb.Scale = 20;
            gb.Width = 20;
            gb.ToothCount = 10;

            ToothShape = new Point2DContainer();

            ToothShape.Point2DList = new Point2D[]
            {
                new Point2D() { X = -.5f, Y = 0 },
                new Point2D() { X = -.5f + .15f, Y = 1 },
                new Point2D() { X = -.5f + .85f, Y = 1 },
                new Point2D() { X = .5f, Y = 0 }
            };

            gb.ToothShape = ToothShape;

            gb.GearHoles.HoleList = new Hole[1];
            gb.GearHoles.HoleList[0] = new Hole()
            {
                Boundary = new BoundaryEllipse(20, 20),
                Offset = new Point3D(0, 0, 0)
            };

            List<SampleInstance> list = new List<SampleInstance>() {

            new SampleInstance()
                {
                    ShortDescription = "Gear",
                    LongDescription = "Square shaped teeth with a large central hole",
                    Sample = gb.GetProperties()
                },
            };

            return list;

        }
    }
}
