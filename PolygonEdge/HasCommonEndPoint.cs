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
        /// See if these two edges share a point
        /// </summary>
        /// <param name="Test"></param>
        /// <returns></returns>
        public bool HasCommonEndPoint(PolygonEdge Test)
        {
            return (From.Equals(Test.From) || To.Equals(Test.To) || (From.Equals(Test.To) || To.Equals(Test.From)));
        }
    }
}
