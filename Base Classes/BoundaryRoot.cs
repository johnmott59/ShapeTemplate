using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public class BoundaryRoot : ILoadAndSaveProperties
    {
        public virtual XElement GetProperties()
        {
            return new XElement("empty");
        }

        public virtual bool LoadProperties(XElement ele,out string message)
        {
            message = "OK";
            return true;
        }

        public static bool LoadXElement(XElement ele,out BoundaryRoot bound, out string message)
        {
            if (ele == null)
            {
                message = $"Missing boundary element";
                bound = null;
                return false;
            }

            message = "OK";

            // Based on the child, build and return an object
            XElement child = (XElement) ele.FirstNode;

            switch (child.Name.LocalName)
            {
                case "rectangle":
                    bound = new BoundaryRectangle();
                    return bound.LoadProperties(child, out message);
                case "ellipse":
                    bound = new BoundaryEllipse();
                    return bound.LoadProperties(child, out message);
                case "line":
                    bound = new BoundaryLineSegment();
                    return bound.LoadProperties(child, out message);
                default:
                    message = $"Expected boundary value rectangle, ellipse or line, got {child.Name.LocalName}";
                    bound = null;
                    return false;
            }

        }

    }


}