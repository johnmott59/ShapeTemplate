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
        public BoundaryPolygon GetRectangle(int index)
        {
            int ithPoly = 0;
            for (int i=0; i < oHoleGroup.HoleList.Length; i++)
            {
                if (oHoleGroup.HoleList[i].HoleType == "rect")
                {
                    if (ithPoly == index)
                    {
                        LayoutHole lh = oHoleGroup.HoleList[i];
                        BoundaryRectangle bp0 = RectangleArray[lh.HoleTypeIndex];

                        List<Point3D> p3List = new List<Point3D>();
                        p3List.Add(new Point3D() { X = lh.OffsetX, Y = lh.OffsetY });
                        p3List.Add(new Point3D() { X = lh.OffsetX + bp0.Width, Y = lh.OffsetY });
                        p3List.Add(new Point3D() { X = lh.OffsetX + bp0.Width, Y = lh.OffsetY + bp0.Height });
                        p3List.Add(new Point3D() { X = lh.OffsetX, Y = lh.OffsetY + bp0.Height });

                        BoundaryPolygon bp = new BoundaryPolygon();

                        bp.PointList = p3List.ToArray();

                        return bp;
                    }
                    ithPoly++;
                }
            }

            return null;
          
        }
    }
}
