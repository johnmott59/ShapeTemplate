using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {
        /// <summary>
        /// Given two line segments known to be parallel see if they overlap, and if so where they overlap.
        /// </summary>
        /// <param name="pe1"></param>
        /// <param name="pe2"></param>
        /// <returns></returns>
        public static List<PointF> ParallelSegmentsOverlap(PolygonEdge pe1, PolygonEdge pe2)
        {
            List<PointF> OverlapPoints = new List<PointF>();

            // Scenario 1. The fully overlap - they are the same. no intersection

            if (pe1.From.Equals(pe2.From) && pe1.To.Equals(pe2.To)) return OverlapPoints;
            if (pe1.From.Equals(pe2.To) && pe1.To.Equals(pe2.From)) return OverlapPoints;

            // check to see if we have endpoint matches, that will guide tests for inside matches

            if (pe1.From.Equals(pe2.From))
            {
                OverlapPoints.Add(pe1.From);
                // pe1.from     pe1.to
                // pe2.from               pe2.to
                if (IsInsideSegment(pe1.To, pe2)) {
                    OverlapPoints.Add(pe1.To);
                    OverlapPoints.Add(pe2.To);
                    return OverlapPoints;
                }

                // pe1.from               pe1.to
                // pe2.from     pe2.to
                if (IsInsideSegment(pe2.To, pe1))
                {
                    OverlapPoints.Add(pe2.To);
                    OverlapPoints.Add(pe1.To);
                    return OverlapPoints;
                }
            }

            if (pe1.To.Equals(pe2.To))
            {
                OverlapPoints.Add(pe1.To);
                // pe1.To     pe1.From
                // pe2.To               pe2.From
                if (IsInsideSegment(pe1.From, pe2))
                {
                    OverlapPoints.Add(pe1.From);
                    OverlapPoints.Add(pe2.From);
                    return OverlapPoints;
                }
                // pe2.To     pe2.From
                // pe1.To               pe1.From
                if (IsInsideSegment(pe2.From, pe1))
                {
                    OverlapPoints.Add(pe2.From);
                    OverlapPoints.Add(pe1.From);
                    return OverlapPoints;
                }
            }

            // The remaining possible condition is that one segment lies completely inside another. In that
            // case return the 'from' point. We don't need to worry about losing a point, operations which 
            // create intersections are run multiple times until all conditions are found

            if (IsInsideSegment(pe1.From, pe2) && IsInsideSegment(pe1.To, pe2))
            {
                OverlapPoints.Add(pe2.From);
                OverlapPoints.Add(pe1.From);
                OverlapPoints.Add(pe1.To);
                OverlapPoints.Add(pe2.To);
                return OverlapPoints;
            }
            if (IsInsideSegment(pe2.From, pe1) && IsInsideSegment(pe2.To, pe1))
            {
                OverlapPoints.Add(pe1.From);
                OverlapPoints.Add(pe2.From);
                OverlapPoints.Add(pe2.To);
                OverlapPoints.Add(pe1.To);
                return OverlapPoints;
            }

            // These two segments do not overlap

            return OverlapPoints;

        }

        public static bool IsInsideSegment(PointF test, PolygonEdge pe)
        {

            double dis1 = distance(test, pe.From);
            double dis2 = distance(test, pe.To);

            // if either distance is 0 it means that the intersection is an end point, not what we're looking for

            if (dis1 + dis2 - pe.EdgeLength < .1) return true;

            return false;
        }

        private static double distance(PointF test, PointF Target)
        {
            return Math.Sqrt((test.X - Target.X) * (test.X - Target.X) + (test.Y - Target.Y) * (test.Y - Target.Y));
        }


        public static PointF? FindInsideIntersectionPoint(PolygonEdge peGreen, PolygonEdge peBlue)
        {
            PointF GreenFrom = peGreen.From;
            PointF GreenTo = peGreen.To;

            PointF BlueFrom = peBlue.From;
            PointF BlueTo = peBlue.To;

            /*
             * Do they contain a common point? If so they don't intersect, even if they are collienar
             */
            if (GreenFrom.Equals(BlueFrom) ||
               GreenFrom.Equals(BlueTo) ||
               GreenTo.Equals(BlueFrom) ||
               GreenTo.Equals(BlueTo))
            {
                return null;
            }
            /*
             * Are they parallel?
             */

            float par = (float)((GreenTo.X - GreenFrom.X) * (BlueTo.Y - BlueFrom.Y) -
                           (GreenTo.Y - GreenFrom.Y) * (BlueTo.X - BlueFrom.X));
            /*
             * Special case. if these segments are parallel see if they overlap. We already checked for a common point
             */
            if (par == 0)
            {
                // If these two segments overlap they will have two points in common. we already checked the case of end points matching
                // The caller of this is likely looping through segments over and over, so we can return one of the inside points.

                List<PointF> overLapList = ParallelSegmentsOverlap(peGreen, peBlue);
                if (overLapList.Count == 4) return overLapList[1];

                return null;                               /* parallel lines */
            }

       
            /*
             * Find the proportional distance from one point to another
             */
            float tp = ((BlueFrom.X - GreenFrom.X) * (BlueTo.Y - BlueFrom.Y) - (BlueFrom.Y - GreenFrom.Y) * (BlueTo.X - BlueFrom.X)) / par;
            float tq = ((GreenTo.Y - GreenFrom.Y) * (BlueFrom.X - GreenFrom.X) - (GreenTo.X - GreenFrom.X) * (BlueFrom.Y - GreenFrom.Y)) / par;
            /*
             * If the distance isn't between 0 and one the segments don't intersect
             */
            if (tp < 0 || tp > 1 || tq < 0 || tq > 1)
            {
                return null;
            }

            PointF oIntersect = new PointF(GreenFrom.X + tp * (GreenTo.X - GreenFrom.X), GreenFrom.Y + tp * (GreenTo.Y - GreenFrom.Y));

            // If this point is within epsilon of an existing endpoint clamp it to that point

            if (distance(oIntersect, GreenFrom) < .1) return GreenFrom;
            if (distance(oIntersect, GreenTo) < .1) return GreenTo;
            if (distance(oIntersect, BlueFrom) < .1) return BlueFrom;
            if (distance(oIntersect, BlueTo) < .1) return BlueTo;

            return oIntersect;

        }

        
    }
}
