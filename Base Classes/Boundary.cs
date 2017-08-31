using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplate
{
    public class BoundaryRoot : IXElement
    {
        public virtual XElement GetXElement()
        {
            return new XElement("empty");
        }

        public void GetXElement(XElement Parent)
        {
            throw new NotImplementedException();
        }
    }

    public class BoundaryRectangle : BoundaryRoot
    {
        public float Width { get; set; } = 20;
        public float Height { get; set; } = 20;

        public override XElement GetXElement()
        {
            return new XElement("boundary", new XElement("rectangle", new XAttribute("width", Width), new XAttribute("height", Height)));
        }
    }

    public class BoundaryEllipse : BoundaryRoot
    {
        public float Width { get; set; } = 20;
        public float Height { get; set; } = 20;

        public override XElement GetXElement()
        {
            return new XElement("boundary", new XElement("rectangle", new XAttribute("width", Width), new XAttribute("height", Height)));
        }
    }

    public class BoundaryLineSegment : BoundaryRoot
    {
        public List<Point3D> PointList { get; set; } = new List<Point3D>();

        public override XElement GetXElement()
        {
            XElement bound = new XElement("boundary");

            XElement plist = new XElement("line");

            bound.Add(plist);

            foreach (Point3D p in PointList)
            {
                plist.Add(p.GetXElement());
            }

            return bound;
        }
    }


}