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
        // Quick method to add a same sided polygon at an offet
        public void AddPolygon(Point2D Offset, int Sides, float SideLength)
        {
            if (Sides < 3) return;

            double RadiansPerSide = 2 * Math.PI / (double)Sides;
            List<Point2D> PointList = new List<Point2D>();

            double rad = 0;
            for (int i = 0; i < Sides; i++, rad += RadiansPerSide)
            {
                float x = SideLength * (float) Math.Cos(rad);
                if (Math.Abs(x) < .000001) x = 0;

                float y = SideLength * (float) Math.Sin(rad);
                if (Math.Abs(y) < .000001) y = 0;

                PointList.Add(new ShapeTemplateLib.Point2D() { X = x, Y = y });
            }

            AddPolygon(Offset,PointList);
            
        }
        /*
         * Add a polygon to the set of shapes that we manage
         */
        public void AddPolygon(Point2D offset,List<Point2D> PointList)
        {
            BoundaryPolygon bp = new BoundaryPolygon() { BoundaryType = "poly" };

            List<Point3D> p3list = new List<Point3D>();

            foreach (Point2D p in PointList)
            {
                p3list.Add(new Point3D() { X = p.X, Y = p.Y, Z = 0 });
            }

            bp.PointList = p3list.ToArray();

            _Addbp(offset,bp);
        }
        /*
         * Add a polygon to the set of shapes that we manage
         */
        public void AddPolygon(List<Point2D> PointList)
        {
            BoundaryPolygon bp = new BoundaryPolygon() { BoundaryType = "poly" };

            List<Point3D> p3list = new List<Point3D>();

            foreach (Point2D p in PointList)
            {
                p3list.Add(new Point3D() { X = p.X, Y = p.Y, Z = 0 });
            }

            bp.PointList = p3list.ToArray();

            _Addbp(bp);
        }

        protected void _Addbp( BoundaryPolygon bp)
        {
            _Addbp(new Point2D(0, 0), bp);
        }

        protected void _Addbp(Point2D Offset,BoundaryPolygon bp)
        {

            // Add this boundary polygon. First retrieve any existing polygons as a list, add the new one, then retrieve as an array

            List<BoundaryPolygon> bplist = PolygonArray.ToList();
            // Add the polygon to the list of polygons
            int NewIndex = bplist.Count;
            bplist.Add(bp);
            PolygonArray = bplist.ToArray();

            /*
             * Now add the hole description to the hole array
             */
            List<LayoutHole> hl = oHoleGroup.HoleList.ToList();

            hl.Add(new LayoutHole()
            {
                HoleType = "poly",
                HoleTypeIndex = NewIndex,
                OffsetX = Offset.X,
                OffsetY = Offset.Y
            });

            oHoleGroup.HoleList = hl.ToArray();
        }

        public void AddPolygon(List<LineSegment> lsList)
        {
            // Link the points in the polygon
            // add the outer polygon
            BoundaryPolygon bp = new BoundaryPolygon() { BoundaryType = "poly" };

            bp.PointList = GetPointListInOrder(lsList).ToArray();

            _Addbp(bp);

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
