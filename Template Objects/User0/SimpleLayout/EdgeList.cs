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
        //---------------------------------
        protected XElement GetSegmentList()
        {
            XElement XSegmentList = new XElement("edgelist");
            foreach (Edge s in EdgeList)
            {
                XSegmentList.Add(new XElement("edge",
                        new XAttribute("id", s.ID),
                        new XAttribute("holegroupid", s.HoleGroupID),
                        new XAttribute("p1", s.p1),
                        new XAttribute("p2", s.p2),
                        new XAttribute("width", s.Width.ToString()),
                        new XAttribute("height", s.Height.ToString())
                        )
                    );
            }
            return XSegmentList;
        }

        protected bool LoadSegmentList(XElement ele, out string message)
        {
            message = "OK";

            XElement XSegmentList = Utilities.GetListElement(ele, "edgelist");

            // an empty layout would have no segments.
            if (XSegmentList == null) return true;

            foreach (XElement XSegment in XSegmentList.Elements("edge"))
            {
                string id, holegroupid;
                int p1, p2;
                int width, height;


                if (!Utilities.GetIntAttribute(XSegment, "p1", out p1, out message)) return false;
                if (!Utilities.GetIntAttribute(XSegment, "p2", out p2, out message)) return false;
                if (!Utilities.GetIntAttribute(XSegment, "width", out width, out message)) return false;
                if (!Utilities.GetIntAttribute(XSegment, "height", out height, out message)) return false;

                if (!Utilities.GetStringAttribute(XSegment, "id", out id, out message)) return false;
                if (!Utilities.GetStringAttribute(XSegment, "holegroupid", out holegroupid, out message)) return false;

                EdgeList.Add(new Edge()
                {
                    p1 = p1,
                    p2 = p2,
                    Height = height,
                    Width = width,
                    ID = id,
                    HoleGroupID = holegroupid
                });
            }

            return true;
        }
    }
}
