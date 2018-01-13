using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SimpleLayout 
    {

        //-----------------------------------
        protected XElement GetEllipseList()
        {
            XElement XEllipseList = new XElement("boundaryellipselist");
            foreach (BoundaryEllipse hg in BoundaryEllipseList)
            {
                XEllipseList.Add(new XElement("boundaryellipse",
                    new XAttribute("width", hg.Width),
                    new XAttribute("height", hg.Height),
                    new XAttribute("zdepth", hg.ZDepth))
                    );
            }
            return XEllipseList;
        }

        protected bool LoadEllipseList(XElement ele, out string message)
        {
            message = "OK";

            XElement XEllipseList = Utilities.GetListElement(ele, "boundaryellipselist");

            // ok if no ellipses.
            if (XEllipseList == null) return true;

            // There should be one child, a boundaryrectanglelist, that's the container
            XEllipseList = XEllipseList.Element("boundaryellipselist");
            if (XEllipseList == null) return true;

            foreach (XElement XEllipse in XEllipseList.Elements("boundaryellipse"))
            {
                float width, height, zdepth;

                if (!Utilities.GetFloatAttribute(XEllipse, "width", out width, out message)) return false;

                if (!Utilities.GetFloatAttribute(XEllipse, "height", out height, out message)) return false;

                if (!Utilities.GetFloatAttribute(XEllipse, "zdepth", out zdepth, out message)) return false;

                BoundaryEllipseList.Add(new BoundaryEllipse((int) width, (int) height, (int) zdepth));
            }

            return true;
        }
    }
}
