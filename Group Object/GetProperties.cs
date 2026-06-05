using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib;

namespace ShapeTemplateLib
{
    public partial class Group 
    {

        public override XElement GetProperties(string PropertyName = "")
        {
            XAttribute xType = null;
            XAttribute xValue = new XAttribute("value", this.TransformValue);
            if (TransformType != null) {
                switch (TransformType)
                {
                    case eTransformType.RotateX:
                        xType = new XAttribute("transform", "rx");
                        break;
                    case eTransformType.RotateY:
                        xType = new XAttribute("transform", "ry");
                        break;
                    case eTransformType.RotateZ:
                        xType = new XAttribute("transform", "rz");
                        break;
                    case eTransformType.TranslateX:
                        xType = new XAttribute("transform", "tx");
                        break;
                    case eTransformType.TranslateY:
                        xType = new XAttribute("transform", "ty");
                        break;
                    case eTransformType.TranslateZ:
                        xType = new XAttribute("transform", "tz");
                        break;
                }
            }

            XElement xGroup = new XElement("group",new XAttribute("prop", PropertyName));

            if (xType != null)
            {
                xGroup.Add(xType, xValue);
            }
            /*
             * Recurse to process group nodes
             */
             foreach (Group g in GroupChildrenList)
            {
                xGroup.Add(g.GetProperties());
            }
            /*
             * Get the properties of the child nodes
             */
             foreach (TransformableRoot mb in this.MeshBaseClassList)
            {
                XElement xb = mb.GetProperties("");
                xGroup.Add(xb);
            }
            return xGroup;
        }


    }
}
