using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {
        // Get the end points of a specifally sized hole in this edge at a location indicated by the fraction
        /// <summary>

        public static bool IsPointInsidePolygon(PointF v, List<PolygonEdge> PolygonEdgeList)
        {
            List<PointF> PointList = new List<PointF>();
            foreach (PolygonEdge pe in PolygonEdgeList)
            {
                PointList.Add(pe.To);
            }

            int j = PointList.Count - 1;

            bool c = false;

            // I can't remember where I found this gem of code but its very nice.
            for (int i = 0; i < PointList.Count; j = i++)
            {
                c ^= PointList[i].Y > v.Y ^ PointList[j].Y > v.Y
                    && v.X < (PointList[j].X - PointList[i].X)
                    * (v.Y - PointList[i].Y) / (PointList[j].Y - PointList[i].Y) + PointList[i].X;
            }
            return c;
        }
    }
}
