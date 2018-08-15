using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    /*
     * This class is used to contain one array of holes and the boundary structures that go with them
     * for the purposes of the FloorLayoutInput
     * we're using the same structurs as the simplelayout but not in the same way. The simplelayout
     * can have many hole groups, because shapes can be shared between hole groups. This container
     * will only have one, because we expect that shapes will not be shared.
     */
    public partial class SingleHoleGroup
    {

        public XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("singleholegroup", new XAttribute("prop", PropertyName));

            root.Add(oHoleGroup.GetProperties("oholegroup"));

            XElement XRectangleArray = new XElement("list", new XAttribute("name", nameof(RectangleArray).ToLower()));
            root.Add(XRectangleArray);
            XRectangleArray.Add(GetRectangleArray().Elements());

            XElement XEllipseArray = new XElement("list", new XAttribute("name", nameof(EllipseArray).ToLower()));
            root.Add(XEllipseArray);
            XEllipseArray.Add(GetEllipseArray().Elements());

            XElement XPolygonArray = new XElement("list", new XAttribute("name", nameof(PolygonArray).ToLower()));
            root.Add(XPolygonArray);
            XPolygonArray.Add(GetPolygonArray().Elements());

            return root;
        }

        //--------------------------------
        protected XElement GetRectangleArray()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("rectanglearray");

            foreach (BoundaryRectangle r in RectangleArray)
            {
                XList.Add(r.GetProperties());
            }

            return XList;
        }


        //--------------------------------
        protected XElement GetEllipseArray()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("ellipsearray");

            foreach (BoundaryEllipse r in EllipseArray)
            {
                XList.Add(r.GetProperties());
            }

            return XList;
        }


        //--------------------------------
        protected XElement GetPolygonArray()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("polygonarray");

            foreach (BoundaryPolygon r in PolygonArray)
            {
                XList.Add(r.GetProperties());
            }

            return XList;
        }

    }
}
