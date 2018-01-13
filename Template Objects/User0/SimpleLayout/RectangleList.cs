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
        protected XElement GetRectangleList()
        {
            XElement XRectangleList = new XElement("boundaryrectanglelist");
            foreach (BoundaryRectangle hg in BoundaryRectangleList)
            {
                XRectangleList.Add(new XElement("boundaryrectangle",
                    new XAttribute("width", hg.Width),
                    new XAttribute("height", hg.Height),
                    new XAttribute("zdepth", hg.ZDepth))
                    );
            }
            return XRectangleList;
        }

        protected bool LoadRectangleList(XElement ele, out string message)
        {
            message = "OK";

            XElement XRectangleList = Utilities.GetListElement(ele, "boundaryrectanglelist");

            // ok if there are no items
            if (XRectangleList == null) return true;

            // There should be one child, a boundaryrectanglelist, that's the container
            XRectangleList = XRectangleList.Element("boundaryrectanglelist");
            if (XRectangleList == null) return true;

            foreach (XElement XRectangle in XRectangleList.Elements("boundaryrectangle"))
            {
                float width, height, zdepth;

                if (!Utilities.GetFloatAttribute(XRectangle, "width", out width, out message)) return false;

                if (!Utilities.GetFloatAttribute(XRectangle, "height", out height, out message)) return false;

                if (!Utilities.GetFloatAttribute(XRectangle, "zdepth", out zdepth, out message)) return false;

                BoundaryRectangleList.Add(new BoundaryRectangle((int) width, (int) height, (int) zdepth));
            }

            return true;
        }
    }
}
