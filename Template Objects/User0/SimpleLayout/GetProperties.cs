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
        public override XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("template",
                new XAttribute("prop", PropertyName),
                new XAttribute("user", "User0"),
                new XAttribute("name", "simplelayout"),

            new XElement("property", new XAttribute(nameof(HorizontalScale).ToLower(), HorizontalScale)),
            new XElement("property", new XAttribute(nameof(VerticalScale).ToLower(), VerticalScale)));

            XElement XVertexList = new XElement("list", new XAttribute("name", nameof(VertexList).ToLower()));
            root.Add(XVertexList);
            XVertexList.Add(GetVertexList().Elements());

            XElement XSegmentList = new XElement("list", new XAttribute("name", nameof(EdgeList).ToLower()));
            root.Add(XSegmentList);
            XSegmentList.Add(GetSegmentList().Elements());

            XElement XHoldGroupList = new XElement("list", new XAttribute("name", nameof(HoleGroupList).ToLower()));
            root.Add(XHoldGroupList);
            XHoldGroupList.Add(GetHoleGroupList().Elements());

            XElement XRectangleList = new XElement("list", new XAttribute("name", nameof(BoundaryRectangleList).ToLower()));
            root.Add(XRectangleList);
            XRectangleList.Add(GetRectangleList());

            XElement XEllipseList = new XElement("list", new XAttribute("name", nameof(BoundaryEllipseList).ToLower()));
            root.Add(XEllipseList);
            XEllipseList.Add(GetEllipseList());

            XElement XPolygonList = new XElement("list", new XAttribute("name", nameof(BoundaryPolygonList).ToLower()));
            root.Add(XPolygonList);
            XPolygonList.Add(GetPolygonList());

            XElement XHorizontalPanelList = new XElement("list", new XAttribute("name", nameof(HorizontalPanelList).ToLower()));
            root.Add(XHorizontalPanelList);
            XHorizontalPanelList.Add(GetHorizontalPanelList());

            return root;
        }
    }
}
