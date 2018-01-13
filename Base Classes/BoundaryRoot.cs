using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    /// <summary>
    /// 
    /// The BoundaryRoot is the base class of BoundaryRectangle, BoundaryEllipse and BoundaryLineSegment
    /// The Panel and Hole classes use Boundaries to define their outlines, but will instantiate the type of 
    /// boundary that makes sense, an ellipise, rectangle or line segment.
    /// 
    /// Points for boundaries are defined in the XY plane with left (-x), right (+x), up (+y) and down (-y) with +z facing
    /// outward towards the viewer.
    /// 
    /// Points for boundaries should be wound counter-clockwise. This happens automatically for rectangle
    /// and ellipse shapes. 
    /// An example triangle that is wound correctly would be (0,0) -> (50,0) -> (25,25)
    /// 
    /// </summary>
    [HelpItem(eItemFlavor.Data,"boundary")]
    public class BoundaryRoot : ILoadAndSaveProperties
    {
        public virtual XElement GetProperties(string PropertyName="")
        {
            return new XElement("boundary",new XAttribute("prop",PropertyName));
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
                case "linesegment":
                    bound = new BoundaryPolygon();
                    return bound.LoadProperties(child, out message);
                default:
                    message = $"Expected boundary value rectangle, ellipse or line, got {child.Name.LocalName}";
                    bound = null;
                    return false;
            }

        }

    }


}