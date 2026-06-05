using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {
        public static List<PolygonEdge> FromBoundaryPolygon(BoundaryPolygon bp)
        {
            List<PolygonEdge> PolygonEdgeList = new List<PolygonEdge>();

            if (bp.PointList.Length == 0) return PolygonEdgeList;

            Point3D from = new Point3D() { X = bp.PointList[0].X, Y = bp.PointList[0].Y };

            foreach (var v in bp.PointList.Skip(1))
            {
                Point3D to = v;
                PolygonEdgeList.Add(new PolygonEdge()
                {
                    From = new System.Drawing.PointF() { X = from.X, Y = from.Y },
                    To = new System.Drawing.PointF() { X = to.X, Y = to.Y }
                });

                from = to;
            }

            // add the link to the origin

            PolygonEdgeList.Add(new PolygonEdge()
            {
                From = new System.Drawing.PointF() { X = from.X, Y = from.Y },
                To = new System.Drawing.PointF() { X = bp.PointList[0].X, Y = bp.PointList[0].Y }
            });

            return PolygonEdgeList;
        }

    }
}
