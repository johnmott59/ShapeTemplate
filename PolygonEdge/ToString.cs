using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {
        public override string ToString()
        {
            return $"{From.X},{From.Y} -> {To.X},{To.Y}";

        }
    }
}
