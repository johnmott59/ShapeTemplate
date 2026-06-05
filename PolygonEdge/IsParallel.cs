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
        // See if another edge is parallel to use

        public bool IsParallel(PolygonEdge peCompare)
        {
            PointF CompareFrom = peCompare.From;
            PointF CompareTo = peCompare.To;

            PointF UsFrom = From;
            PointF UsTo = To;

            /*
             * Are they parallel? This is the dot product of the two edges as vectors
             */

            float par = (float)((CompareTo.X - CompareFrom.X) * (UsTo.Y - UsFrom.Y) -
                           (CompareTo.Y - CompareFrom.Y) * (UsTo.X - UsFrom.X));

            if (par == 0)
            {
                return true;                               /* parallel lines */
            }

            return false;
        }
    }
}
