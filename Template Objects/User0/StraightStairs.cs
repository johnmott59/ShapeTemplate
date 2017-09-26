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
        /// <summary>
        /// The verticaldistance describes how they are placed vertically
        /// </summary>
        [HelpProperty(SampleValue ="10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int VerticalDistance { get; set; } = 10;

        /// <summary>
        /// The horizontaldistance describes how they are placed horizontally
        /// </summary>
        [HelpProperty( SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int HorizontalDistance { get; set; } = 10;

        /// <summary>
        /// The lateraldistance describes how the stairs drift sideways 
        /// </summary>
        [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int LateralDistance { get; set; } = 0;

        /// <summary>
        /// staircount is the count of stairs
        /// </summary>
        [HelpProperty(SampleValue ="10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int StairCount { get; set; } = 10;

        /// <summary>
        /// width is the width of stairs
        /// </summary>
        [HelpProperty(SampleValue = "30", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int Width { get; set; } = 30;

        /// <summary>
        /// rise is the how tall each stair is 
        /// </summary>
        [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int Rise { get; set; } = 10;

        /// <summary>
        /// run is the length of the stair 
        /// </summary>
        [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int Run { get; set; } = 10;

        /// <summary>
        /// The left and right side of the stairs have a texture, and there is a texture for the steps
        /// leftsidetexture is the name of the texture file for the left side
        /// </summary>
        [HelpProperty( SampleValue = "leftside.png", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public string LeftSideTexture { get; set; } = "";

        /// <summary>
        /// The left and right side of the stairs have a texture, and there is a texture for the steps
        /// rightsidetexture is the name of the texture file for the left side
        //// 1. its an inline attrobute of its parent
        /// 2. its an attribute of node with named 'property'
        /// </summary>
        [HelpProperty( SampleValue = "leftside.png", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public string RightSideTexture { get; set; } = "";

        /// <summary>
        /// The left and right side of the stairs have a texture, and there is a texture for the steps
        /// stairtexture is the name of the texture file for the left side
        /// </summary>
        [HelpProperty(SampleValue = "stairtexture", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public string StairTexture { get; set; } = "";


        public override XElement Compile()
        {
            XElement root = new XElement("straightstairs");

            for (int i = 0; i < StairCount; i++)
            {
                root.Add(SingleStair(new Point3D(i * HorizontalDistance, i * VerticalDistance, i* LateralDistance), Width, Rise, Run));
            }

            return root;
        }
        public override XElement GetProperties(string PropertyName="")
        {
            XElement root = new XElement("template",
                new XAttribute("prop",PropertyName),
                new XAttribute("user","User0"),
                new XAttribute("name","straightstairs"),

            new XElement("property", new XAttribute(nameof(VerticalDistance).ToLower(), VerticalDistance)),
            new XElement("property", new XAttribute(nameof(HorizontalDistance).ToLower(), HorizontalDistance)),
            new XElement("property", new XAttribute(nameof(LateralDistance).ToLower(), LateralDistance)),
            new XElement("property", new XAttribute(nameof(StairCount).ToLower(), StairCount)),
            new XElement("property", new XAttribute(nameof(Width).ToLower(), Width)),
            new XElement("property", new XAttribute(nameof(Rise).ToLower(), Rise)),
            new XElement("property", new XAttribute(nameof(Run).ToLower(),Run)),
            new XElement("property", new XAttribute(nameof(LeftSideTexture).ToLower(), LeftSideTexture)),
            new XElement("property", new XAttribute(nameof(RightSideTexture).ToLower(), RightSideTexture)),
            new XElement("property", new XAttribute(nameof(StairTexture).ToLower(), StairTexture)));

            return root;
        }


        public override bool LoadProperties(XElement ele, out string message)
        {
            int iTmp;
            string sTmp;

            message = "OK";

            // find each property
            XAttribute oAtt = Utilities.GetPropertyAttribute(ele,nameof(VerticalDistance));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt,  out iTmp, out message)) return false;
                VerticalDistance = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(HorizontalDistance));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
                HorizontalDistance = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(LateralDistance));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
                LateralDistance = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(StairCount));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
                StairCount = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(Width));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
                Width = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(Rise));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
                Rise = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(Run));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
                Run = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(LeftSideTexture));
            if (oAtt != null)
            {
                LeftSideTexture = oAtt.Value;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(RightSideTexture));
            if (oAtt != null)
            {
                RightSideTexture = oAtt.Value;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(StairTexture));
            if (oAtt != null)
            {
                StairTexture = oAtt.Value;
            }

            return true;
        }

        protected XElement SingleStair(Point3D BasePoint, int width = 30, int rise = 10, int run = 10)
        {
            Panel sp = new Panel();
            sp.FrontMesh.MeshDisplayProperties.MaterialColor = "#DBDBEF";
            sp.FrontMesh.MeshDisplayProperties.MaterialName = "stairfront";

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

            sp.BackMesh.MeshDisplayProperties.MaterialColor = "#DBDBEF";
            sp.BackMesh.MeshDisplayProperties.MaterialName = "stairback";

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

            sp.ConnectorDisplayProperties.MaterialColor = "#FFFFFF";
            sp.ConnectorDisplayProperties.MaterialName = "StairConnector";
            sp.ConnectorDisplayProperties.TextureFileName = "woodfloor.png";

            sp.ConnectorSegmentVisible = new List<bool>() { true, true, true, true };

            return sp.GetProperties();
        }


    }

}
