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
        public PointF GetProportionalPoint(int numerator, int denominator)
        {
            var fraction = (float)numerator / (float)denominator;

            return GetProportionalPoint(fraction);
        }

        public PointF GetProportionalPoint(float fraction)
        {
            return new PointF()
            {
                X = From.X + fraction * (To.X - From.X),
                Y = From.Y + fraction * (To.Y - From.Y)
            };
        }
    }
}
