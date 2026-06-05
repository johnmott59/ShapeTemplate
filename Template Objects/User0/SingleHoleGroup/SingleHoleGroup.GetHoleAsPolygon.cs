using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SingleHoleGroup
    {
        /// <summary>
        /// Entry point to retrieve a hole as a polygon, useful for other steps that want a rendered version of the hole
        /// </summary>
        /// <param name="lh"></param>
        /// <returns></returns>
        public BoundaryPolygon GetHoleAsPolygon(LayoutHole lh)
        {
            List<Point3D> p3List = new List<Point3D>();
            BoundaryPolygon bp = new BoundaryPolygon();

            switch (lh.HoleType)
            {
                case "rect":

                    BoundaryRectangle br0 = RectangleArray[lh.HoleTypeIndex];

                    p3List.Add(new Point3D() { X = lh.OffsetX, Y = lh.OffsetY });
                    p3List.Add(new Point3D() { X = lh.OffsetX + br0.Width, Y = lh.OffsetY });
                    p3List.Add(new Point3D() { X = lh.OffsetX + br0.Width, Y = lh.OffsetY + br0.Height });
                    p3List.Add(new Point3D() { X = lh.OffsetX, Y = lh.OffsetY + br0.Height });

                    break;

                 case "poly":

                    BoundaryPolygon bp0 = PolygonArray[lh.HoleTypeIndex];
                    foreach (Point3D p in bp0.PointList)
                    {
                        p3List.Add(new Point3D() { X = p.X + lh.OffsetX, Y = p.Y + lh.OffsetY });
                    }

                    break;
            }

            bp.PointList = p3List.ToArray();

            return bp;
        }
    }
}
