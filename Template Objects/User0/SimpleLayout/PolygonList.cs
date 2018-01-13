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
        protected XElement GetPolygonList()
        {
            XElement XPolygonList = new XElement("boundarypolygonlist");
            foreach (BoundaryPolygon bp in BoundaryPolygonList)
            {
                XElement xPolygon = new XElement("boundarypolygon");
                XPolygonList.Add(xPolygon);

                foreach (Point3D p in bp.PointList)
                {
                    xPolygon.Add(new XElement("point",
                        new XAttribute("x", p.X),
                        new XAttribute("y", p.Y),
                        new XAttribute("z", p.Z)));
                }
            }
            return XPolygonList;
        }

        protected bool LoadPolygonList(XElement ele, out string message)
        {
            message = "OK";

            XElement XPolygonList = Utilities.GetListElement(ele, "boundarypolygonlist");

            // ok if there are no items
            if (XPolygonList == null) return true;

            // There should be one child, a boundarypolygonlist, that's the container
            XPolygonList = XPolygonList.Element("boundarypolygonlist");
            if (XPolygonList == null) return true;

            foreach(XElement xPolygon in XPolygonList.Elements("boundarypolygon"))
            {
                BoundaryPolygon bp = new BoundaryPolygon();
                BoundaryPolygonList.Add(bp);

                // now collect points

                List<Point3D> pList = new List<Point3D>();

                foreach (XElement XPoint3D in xPolygon.Elements("point"))
                {
                    float x, y, z;

                    if (!Utilities.GetFloatAttribute(XPoint3D, "x", out x, out message)) return false;

                    if (!Utilities.GetFloatAttribute(XPoint3D, "y", out y, out message)) return false;

                    if (!Utilities.GetFloatAttribute(XPoint3D, "z", out z, out message)) return false;

                    pList.Add(new Point3D() { X = x, Y = y, Z = z });

                }

                bp.PointList = pList.ToArray();
            }

       

            return true;
        }
    }
}
