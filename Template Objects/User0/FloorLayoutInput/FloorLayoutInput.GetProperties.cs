
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

        public override XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("template",
          new XAttribute("prop", PropertyName),
          new XAttribute("user", "User0"),
          new XAttribute("name", "FloorLayoutInput".ToLower()));

            root.Add(Outline.GetProperties("outline"));

            root.Add(OpenArea.GetProperties("openarea"));

            XElement XWallSegmentArray = new XElement("list", new XAttribute("name", nameof(WallSegmentArray).ToLower()));
            root.Add(XWallSegmentArray);
            XWallSegmentArray.Add(GetWallSegmentArray().Elements());

            return root;
        }

        //--------------------------------
        protected XElement GetWallSegmentArray()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("wallsegmentarray");

            foreach (LineSegment r in WallSegmentArray)
            {
                XList.Add(r.GetProperties());
            }

            return XList;
        }


    }
}
