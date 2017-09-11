using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public class StraightStairs : TemplateRoot
    {
        public int VerticalDistanceBetweenSteps { get; set; } = 10;
        public int StairCount { get; set; } = 10;
        public int Width { get; set; } = 30;
        public int Rise { get; set; } = 10;
        public int Run { get; set; } = 10;
        public string LeftSideTexture { get; set; } = "";
        public string RightSideTexture { get; set; } = "";
        public string StairTexture { get; set; } = "";


        public override XElement Compile()
        {
            XElement root = new XElement("straightstairs");

            for (int i = 0; i < StairCount; i++)
            {
                root.Add(SingleStair(new Point3D(i * 10, i * VerticalDistanceBetweenSteps, 0), Width, Rise, Run));
            }

            return root;
        }
        public override XElement GetProperties()
        {
            XElement root = new XElement("straightstairs",
                new XAttribute("user","User0"),

            new XElement("property", new XAttribute(nameof(VerticalDistanceBetweenSteps).ToLower(), VerticalDistanceBetweenSteps)),
            new XElement("property", new XAttribute(nameof(StairCount).ToLower(), StairCount)),
            new XElement("property", new XAttribute(nameof(Width).ToLower(), Width)),
            new XElement("property", new XAttribute(nameof(Rise).ToLower(), Rise)),
            new XElement("property", new XAttribute(nameof(Run),Run)),
            new XElement("property", new XAttribute(nameof(LeftSideTexture).ToLower(), LeftSideTexture)),
            new XElement("property", new XAttribute(nameof(RightSideTexture).ToLower(), RightSideTexture)),
            new XElement("property", new XAttribute(nameof(StairTexture), StairTexture)));

            return root;
        }

        public override bool LoadProperties(XElement ele, out string message)
        {
            int iTmp;
            string sTmp;

            message = "OK";

            foreach (XElement prop in ele.Elements("property"))
            {
                switch (prop.Name.LocalName)
                {
                    case nameof(VerticalDistanceBetweenSteps):
                        if (!Utilities.GetIntAttribute(prop, nameof(VerticalDistanceBetweenSteps), out iTmp, out message)) return false;
                        VerticalDistanceBetweenSteps = iTmp;
                        break;
                    case nameof(StairCount):
                        if (!Utilities.GetIntAttribute(prop, nameof(StairCount), out iTmp, out message)) return false;
                        StairCount = iTmp;
                        break;
                    case nameof(Width):
                        if (!Utilities.GetIntAttribute(prop, nameof(Width), out iTmp, out message)) return false;
                        Width = iTmp;
                        break;
                    case nameof(Rise):
                        if (!Utilities.GetIntAttribute(prop, nameof(Rise), out iTmp, out message)) return false;
                        Rise = iTmp;
                        break;
                    case nameof(Run):
                        if (!Utilities.GetIntAttribute(prop, nameof(Run), out iTmp, out message)) return false;
                        Run = iTmp;
                        break;
                    case nameof(LeftSideTexture):
                        if (!Utilities.GetStringAttribute(prop, nameof(LeftSideTexture), out sTmp, out message)) return false;
                        LeftSideTexture = sTmp;
                        break;
                    case nameof(RightSideTexture):
                        if (!Utilities.GetStringAttribute(prop, nameof(RightSideTexture), out sTmp, out message)) return false;
                        RightSideTexture = sTmp;
                        break;
                    case nameof(StairTexture):
                        if (!Utilities.GetStringAttribute(prop, nameof(StairTexture), out sTmp, out message)) return false;
                        StairTexture = sTmp;
                        break;
                }
            }
            return true;
        }

        protected XElement SingleStair(Point3D BasePoint, int width = 30, int rise = 10, int run = 10)
        {
            Panel sp = new Panel();
            sp.FrontMesh.MeshDisplayProperties.Color = "#DBDBEF";
            sp.FrontMesh.MeshDisplayProperties.Name = "stairfront";

            sp.FrontMesh.Boundary = new BoundaryLineSegment()
            {
                PointList = {
                new Point3D(BasePoint.X, BasePoint.Y, BasePoint.Z),
                new Point3D(BasePoint.X, BasePoint.Y + rise, BasePoint.Z),
                new Point3D(BasePoint.X + run, BasePoint.Y + rise, BasePoint.Z),
                new Point3D(BasePoint.X + run, BasePoint.Y, BasePoint.Z),
                new Point3D(BasePoint.X, BasePoint.Y, BasePoint.Z)
            }
            };

            sp.BackMesh.MeshDisplayProperties.Color = "#DBDBEF";
            sp.BackMesh.MeshDisplayProperties.Name = "stairback";

            sp.BackMesh.Boundary = new BoundaryLineSegment()
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

            return sp.GetProperties();
        }


    }

}
