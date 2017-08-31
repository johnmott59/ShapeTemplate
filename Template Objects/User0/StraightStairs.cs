using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplate.BasicShapes;

namespace ShapeTemplate.Templates.User0
{
    public class StraightStairs : MeshTemplateRoot
    {
        public int VerticalDistanceBetweenSteps { get; set; } = 10;
        public int StairCount { get; set; } = 10;
        public int Width { get; set; } = 30;
        public int Rise { get; set; } = 10;
        public int Run { get; set; } = 10;
        public string LeftSideTexture { get; set; } = "";
        public string RightSideTexture { get; set; } = "";
        public string StairTexture { get; set; } = "";

        public override XElement GetElement()
        {
            XElement root = new XElement("root");

            for (int i = 0; i < StairCount; i++)
            {
                root.Add(SingleStair(new Point3D(i * 10, i * VerticalDistanceBetweenSteps, 0), Width, Rise, Run));
            }

            return root;
        }


        protected XElement SingleStair(Point3D BasePoint, int width = 30, int rise = 10, int run = 10)
        {
            SimplePanel sp = new SimplePanel();
            sp.FrontPanel.MeshDisplayProperties.Color = "#DBDBEF";
            sp.FrontPanel.MeshDisplayProperties.Name = "stairfront";

            sp.FrontPanel.Boundary = new BoundaryLineSegment()
            {
                PointList = {
                new Point3D(BasePoint.X, BasePoint.Y, BasePoint.Z),
                new Point3D(BasePoint.X, BasePoint.Y + rise, BasePoint.Z),
                new Point3D(BasePoint.X + run, BasePoint.Y + rise, BasePoint.Z),
                new Point3D(BasePoint.X + run, BasePoint.Y, BasePoint.Z),
                new Point3D(BasePoint.X, BasePoint.Y, BasePoint.Z)
            }
            };

            sp.BackPanel.MeshDisplayProperties.Color = "#DBDBEF";
            sp.BackPanel.MeshDisplayProperties.Name = "stairback";

            sp.BackPanel.Boundary = new BoundaryLineSegment()
            {
                PointList = {
                new Point3D(BasePoint.X, BasePoint.Y, BasePoint.Z - 30),
                new Point3D(BasePoint.X, BasePoint.Y + rise, BasePoint.Z - 30),
                new Point3D(BasePoint.X + run, BasePoint.Y + rise, BasePoint.Z - 30),
                new Point3D(BasePoint.X + run, BasePoint.Y, BasePoint.Z - 30),
                new Point3D(BasePoint.X, BasePoint.Y, BasePoint.Z - 30)
            }
            };

            sp.ConnectorDisplayProperties.Color = "#FFFFFF";
            sp.ConnectorDisplayProperties.Name = "StairConnector";
            sp.ConnectorDisplayProperties.TextureFile = "woodfloor.png";

            sp.ConnectorSegmentVisible = new List<bool>() { true, true, true, true };

            return sp.GetXElement();
        }


    }

}
