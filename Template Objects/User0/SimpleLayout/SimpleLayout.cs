using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;


namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// The simplelayout is a layout of walls, holes and floor/ceiling panels. Edges and vertices form the wall panels. Panels can have
    /// holes, which are defined via a HoleGroup. A holegroup consists of some number of BoundaryRectangle, BoundaryPolygon or BoundaryEllipse
    /// outlines. The wall panels are created vertically and can intersect. Horizontal panels, used for floors and roofs, are defined in a HorizontalPanel
    /// object, and these can also have holes, defined in a HoleGroup. The scale of the rendered mesh can be controlled by the HorizontalScale and
    /// VerticalScale properties so that the resulting mesh can be generated in a size appropriate for the platform using the mesh.
    /// </summary>
    [HelpItem(eItemFlavor.Template, "simplelayout")]
    public partial class SimpleLayout : TemplateRoot
    {

        public override XElement Compile()
        {
            // Compilation for this template is the same as generating the properties

            return GetProperties();
        }
        public override XElement GetProperties(string PropertyName="")
        {
            XElement root = new XElement("template",
                new XAttribute("prop", PropertyName),
                new XAttribute("user", "User0"),
                new XAttribute("name", "simplelayout"),

            new XElement("property", new XAttribute(nameof(HorizontalScale).ToLower(), HorizontalScale)),
            new XElement("property", new XAttribute(nameof(VerticalScale).ToLower(), VerticalScale)));

            XElement XVertexList = new XElement("list", new XAttribute("name",nameof(VertexList).ToLower()));
            root.Add(XVertexList);
            XVertexList.Add(GetVertexList().Elements());

            XElement XSegmentList = new XElement("list", new XAttribute("name",nameof(EdgeList).ToLower()));
            root.Add(XSegmentList);
            XSegmentList.Add(GetSegmentList().Elements());

            XElement XHoldGroupList = new XElement("list", new XAttribute("name",nameof(HoleGroupList).ToLower()));
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



        public override bool LoadProperties(XElement ele, out string message)
        {
            float iTmp;
            message = "OK";

            // find each property
            XAttribute oAtt = Utilities.GetPropertyAttribute(ele,nameof(HorizontalScale));
            if (oAtt != null)
            {
                if (!Utilities.GetFloatFromAttribute(oAtt,  out iTmp, out message)) return false;
                HorizontalScale = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(VerticalScale));
            if (oAtt != null)
            {
                if (!Utilities.GetFloatFromAttribute(oAtt, out iTmp, out message)) return false;
                VerticalScale = iTmp;
            }

            if (!LoadRectangleList(ele, out message)) return false;

            if (!LoadEllipseList(ele, out message)) return false;

            if (!LoadPolygonList(ele, out message)) return false;

            if (!LoadVertexList(ele, out message)) return false;

            if (!LoadSegmentList(ele, out message)) return false;

            if (!LoadHoleGroupList(ele, out message)) return false;

            if (!LoadHorizontalPanelList(ele, out message)) return false;

            return true;
        }

    }

}
