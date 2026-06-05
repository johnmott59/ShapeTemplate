using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {
        /// <summary>
        /// see if these two edges have the same end point
        /// </summary>
        /// <param name="Test"></param>
        /// <returns></returns>
        public bool SameEndPoints(PolygonEdge Test)
        {
            return (From.Equals(Test.From) && To.Equals(Test.To) || (From.Equals(Test.To) && To.Equals(Test.From)));
        }
    }
}
