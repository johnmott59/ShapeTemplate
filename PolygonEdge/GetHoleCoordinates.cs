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
        /// 
        /// </summary>
        /// <param name="HoleSize">size of the hole</param>
        /// <param name="numerator">The fractional starting point of the hole, 1/2, 1/4 etc</param>
        /// <param name="denominator">The fractional starting point of the hole, 1/2, 1/4 etc</param>
        /// <returns></returns>
        public Tuple<PointF, PointF> GetHoleCoordinates(int HoleSize, int numerator, int denominator)
        {
            // If the hole size is >= the length of the edge return the edge

            if (HoleSize >= EdgeLength) return new Tuple<PointF, PointF>(From, To);

            // If the hole size is 0 return the point at this fraction

            if (HoleSize <= 0)
            {
                PointF p = GetProportionalPoint(numerator, denominator);
                return new Tuple<PointF, PointF>(p, p);
            }

            var fraction = (float)numerator / (float)denominator;

            // what percentage of the length of this edge is this hole?

            var percentlength = (float)HoleSize / EdgeLength;

            // now we know the percentage. Get the point that is before and after this percentage length

            PointF nf = GetProportionalPoint(fraction - percentlength);
            PointF nt = GetProportionalPoint(fraction + percentlength);

            return new Tuple<PointF, PointF>(nf, nt);
        }
    }
}
