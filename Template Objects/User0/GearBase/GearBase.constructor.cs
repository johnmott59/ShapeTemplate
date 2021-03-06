
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;
using System.Web.Script.Serialization;

namespace ShapeTemplateLib.Templates.User0 
{
    /// <summary>
    /// The Gearbase is a gear shape
    /// </summary>
    [HelpItem(eItemFlavor.Template, "gearbase")]
    public partial class GearBase 
    {

		public GearBase() {

            Scale = 20;

            ToothCount = 20;

            Width = 1;
            
            ToothShape = new Point2DContainer();

            ToothShape.Point2DList = new Point2D[]
            {
                new Point2D() { X = -.5f, Y = 0 },
                new Point2D() { X = -.5f + .15f, Y = 1 },
                new Point2D() { X = -.5f + .85f, Y = 1 },
                new Point2D() { X = .5f, Y = 0 }
            };

            GearHoles = new HoleContainer();
            GearHoles.HoleList = new ShapeTemplateLib.Hole[0];
#if false
            GearHoles.HoleList[0].Boundary = new BoundaryEllipse(3, 3, 0);
            GearHoles.HoleList[0].Offset = new Point3D(5, 5, 0);
#endif

        }
	

    }
}
