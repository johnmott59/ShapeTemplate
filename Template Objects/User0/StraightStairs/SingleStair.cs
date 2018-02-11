using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// The straight stairs template is a set of stairs. 
    /// </summary>
    [HelpItem(eItemFlavor.Template,"straightstairs")]
    public  partial class StraightStairs : TemplateRoot
    {

        protected XElement SingleStair(Point3D BasePoint, int width = 30, int rise = 10, int run = 10)
        {
            Panel sp = new Panel();
            sp.FrontMesh.MeshDisplayProperties.MaterialColor = "#DBDBEF";
            sp.FrontMesh.MeshDisplayProperties.MaterialName = "stairfront";

            sp.FrontMesh.Boundary = new BoundaryPolygon()
            {
                PointList = new Point3D[] {
                new Point3D(BasePoint.X, BasePoint.Y, BasePoint.Z),
                new Point3D(BasePoint.X, BasePoint.Y + rise, BasePoint.Z),
                new Point3D(BasePoint.X + run, BasePoint.Y + rise, BasePoint.Z),
                new Point3D(BasePoint.X + run, BasePoint.Y, BasePoint.Z),
                new Point3D(BasePoint.X, BasePoint.Y, BasePoint.Z)
            }
            };

            sp.BackMesh.MeshDisplayProperties.MaterialColor = "#DBDBEF";
            sp.BackMesh.MeshDisplayProperties.MaterialName = "stairback";

            sp.BackMesh.Boundary = new BoundaryPolygon()
            {
                PointList = new Point3D[] {
                new Point3D(BasePoint.X, BasePoint.Y, BasePoint.Z - 30),
                new Point3D(BasePoint.X, BasePoint.Y + rise, BasePoint.Z - 30),
                new Point3D(BasePoint.X + run, BasePoint.Y + rise, BasePoint.Z - 30),
                new Point3D(BasePoint.X + run, BasePoint.Y, BasePoint.Z - 30),
                new Point3D(BasePoint.X, BasePoint.Y, BasePoint.Z - 30)
            }
            };

            sp.ConnectorDisplayProperties.MaterialColor = "#FFFFFF";
            sp.ConnectorDisplayProperties.MaterialName = "StairConnector";
            sp.ConnectorDisplayProperties.TextureFileName = "woodfloor.png";

            sp.ConnectorSegmentVisible = new List<bool>() { true, true, true, true };

            return sp.GetProperties();
        }


    }

}
