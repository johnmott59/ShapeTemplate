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
        public BoundaryPolygon GetPolygon(int index)
        {
            int ithPoly = 0;
            for (int i=0; i < oHoleGroup.HoleList.Length; i++)
            {
                if (oHoleGroup.HoleList[i].HoleType == "poly")
                {
                    if (ithPoly == index)
                    {
                        LayoutHole lh = oHoleGroup.HoleList[i];
                        BoundaryPolygon bp0 = PolygonArray[lh.HoleTypeIndex];

                        List<Point3D> p3List = new List<Point3D>();
                        foreach (Point3D p in bp0.PointList)
                        {
                            p3List.Add(new Point3D() { X = p.X + lh.OffsetX, Y = p.Y + lh.OffsetY });
                        }

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
