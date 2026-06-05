using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorLayoutInput 
    {
        /*
         * Add a polygon to the set of shapes that we manage
         */
         public void AddPolygonToOpenArea(List<LineSegment> lsList)
        {
            // Link the points in the polygon
            // add the outer polygon
            BoundaryPolygon bp = new BoundaryPolygon() { BoundaryType = "poly" };

            bp.PointList = GetPointListInOrder(lsList).ToArray();

            SingleHoleGroup shg = OpenArea;

            // Add this boundary polygon. First retrieve any existing polygons as a list, add the new one, then retrieve as an array

            List<BoundaryPolygon> bplist = shg.PolygonArray.ToList();
            // Add the polygon to the list of polygons
            int NewIndex = bplist.Count;
            bplist.Add(bp);
            shg.PolygonArray = bplist.ToArray();

            /*
             * Now add the hole description to the hole array
             */
            List<LayoutHole> hl = OpenArea.oHoleGroup.HoleList.ToList();

            hl.Add(new LayoutHole()
            {
                HoleType = "poly",
                HoleTypeIndex = NewIndex,
                OffsetX = 0,
                OffsetY = 0
            });

            OpenArea.oHoleGroup.HoleList = hl.ToArray();
        }

        List<Point3D> GetPointListInOrder(List<LineSegment> outer)
        {
            List<Point3D> p3dList = new List<Point3D>();
            if (outer.Count == 0) return p3dList;

            p3dList.Add(new Point3D() { X = outer[0].To.X, Y = outer[0].To.Y });

            Point3D ConnectingPoint = p3dList[0];
            outer.RemoveAt(0);

            // Process each of the segments
            while (outer.Count > 0)
            {
                // If the connecting segment is the 'from' point
                LineSegment lsFrom = outer.Where(m => m.From.X == ConnectingPoint.X && m.From.Y == ConnectingPoint.Y).FirstOrDefault();
                if (lsFrom != null)
                {
                    ConnectingPoint = new Point3D() { X = lsFrom.To.X, Y = lsFrom.To.Y };
                    p3dList.Add(ConnectingPoint);
                    outer.Remove(lsFrom);
                    continue;
                }

                LineSegment lsTo = outer.Where(m => m.To.X == ConnectingPoint.X && m.To.Y == ConnectingPoint.Y).FirstOrDefault();
                if (lsTo != null)
                {
                    ConnectingPoint = new Point3D() { X = lsTo.From.X, Y = lsTo.From.Y };
                    p3dList.Add(ConnectingPoint);
                    outer.Remove(lsTo);
                    continue;
                }

                // If we're here its an open polygon, which is an error
                throw new Exception("Incomplete outer polygon");

            }

            return p3dList;

        }
    }
}
