using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {
        /*
         * convert a list of polygons stored in pedges back to linesegments
         */
        public static List<List<LineSegment>> ConvertFromPEdge(List<List<PolygonEdge>> peListList)
        {
            List<List<LineSegment>> lsListList = new List<List<LineSegment>>();

            // process each polygon
            foreach (List<PolygonEdge> peList in peListList)
            {
                List<LineSegment> lsList = new List<LineSegment>();
                lsListList.Add(lsList);

                // Process each Pedge, converting to a LineSegment
                foreach (var pe in peList)
                {
                    lsList.Add(new LineSegment()
                    {
                        From = new Point2D() { X = pe.From.X, Y = pe.From.Y },
                        To = new Point2D() { X = pe.To.X, Y = pe.To.Y },
                        Thickness = pe.Width
                    });
                }
            }
            return lsListList;
        }
    }
}
