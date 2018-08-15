
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;
using System.Web.Script.Serialization;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorLayoutInput
    {

        public override bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;
            xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "singleholegroup", "outline", out message);
            if (xNode == null) return false;
            if (!Outline.LoadProperties(xNode, out message)) return false;
            xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "singleholegroup", "openarea", out message);
            if (xNode == null) return false;
            if (!OpenArea.LoadProperties(xNode, out message)) return false;
            if (!LoadWallSegmentArray(xTemplateNode, out message)) return false;

            return true;
        }
        public bool LoadWallSegmentArray(XElement Xele, out string message)
        {
            message = "OK";

            XElement XList = Utilities.GetListElement(Xele, "wallsegmentarray");

            // ok if there are no items
            if (XList == null) return true;

            List<LineSegment> list = new List<LineSegment>();
            foreach (XElement x in XList.Elements("linesegment"))
            {
                LineSegment o = new LineSegment();
                if (!o.LoadProperties(x, out message)) return false;
                list.Add(o);
            }

            WallSegmentArray = list.ToArray();

            return true;
        }


    }
}
