using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public  partial class StraightStairs 
    {
     
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


            if (LocalTransform != null)
            {
                root.Add(LocalTransform.GetProperties("localtransform"));
            }

            if (oFrameOfReference != null)
            {
                root.Add(oFrameOfReference.GetProperties("frameofreference"));
            }

            return root;
        }


    }

}
