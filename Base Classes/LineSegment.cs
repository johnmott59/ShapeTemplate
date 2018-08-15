using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
{


    public partial class LineSegment : ILoadAndSaveProperties
    {

        public LineSegment()
        {
            From = new Point2D();
            To = new Point2D();
        }

        public XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("linesegment", new XAttribute("prop", PropertyName));

            root.Add(From.GetProperties("from"));

            root.Add(To.GetProperties("to"));

            return root;
        }

        public bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;
            xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "point2d", "from", out message);
            if (xNode == null) return false;
            if (!From.LoadProperties(xNode, out message)) return false;
            xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "point2d", "to", out message);
            if (xNode == null) return false;
            if (!To.LoadProperties(xNode, out message)) return false;

            return true;
        }
    }
}
