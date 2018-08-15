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


        public bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;
            xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "holegroup", "oholegroup", out message);
            if (xNode == null) return false;
            if (!oHoleGroup.LoadProperties(xNode, out message)) return false;
            if (!LoadRectangleArray(xTemplateNode, out message)) return false;
            if (!LoadEllipseArray(xTemplateNode, out message)) return false;
            if (!LoadPolygonArray(xTemplateNode, out message)) return false;

            return true;
        }
        public bool LoadRectangleArray(XElement Xele, out string message)
        {
            message = "OK";

            XElement XList = Utilities.GetListElement(Xele, "rectanglearray");

            // ok if there are no items
            if (XList == null) return true;

            List<BoundaryRectangle> list = new List<BoundaryRectangle>();
            foreach (XElement x in XList.Elements("boundaryrectangle"))
            {
                BoundaryRectangle o = new BoundaryRectangle();
                if (!o.LoadProperties(x, out message)) return false;
                list.Add(o);
            }

            RectangleArray = list.ToArray();

            return true;
        }

        public bool LoadEllipseArray(XElement Xele, out string message)
        {
            message = "OK";

            XElement XList = Utilities.GetListElement(Xele, "ellipsearray");

            // ok if there are no items
            if (XList == null) return true;

            List<BoundaryEllipse> list = new List<BoundaryEllipse>();
            foreach (XElement x in XList.Elements("boundaryellipse"))
            {
                BoundaryEllipse o = new BoundaryEllipse();
                if (!o.LoadProperties(x, out message)) return false;
                list.Add(o);
            }

            EllipseArray = list.ToArray();

            return true;
        }

        public bool LoadPolygonArray(XElement Xele, out string message)
        {
            message = "OK";

            XElement XList = Utilities.GetListElement(Xele, "polygonarray");

            // ok if there are no items
            if (XList == null) return true;

            List<BoundaryPolygon> list = new List<BoundaryPolygon>();
            foreach (XElement x in XList.Elements("boundarypolygon"))
            {
                BoundaryPolygon o = new BoundaryPolygon();
                if (!o.LoadProperties(x, out message)) return false;
                list.Add(o);
            }

            PolygonArray = list.ToArray();

            return true;
        }
    }
}
