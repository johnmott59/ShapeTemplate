using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    // This is a list of edges that used in polygon processing, although they may not form a polygon

    public partial class NRMEdgeList
    {
        /// <summary>
        ///  See if this polygon intersects with us.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool Intersects(NRMEdgeList target)
        {
            // If any of the edges of the target have an intersection point with any of our edges we have an intersection

            foreach(var peTarget in target.NRMRoomEdgeList)
            {
                foreach (var us in this.NRMRoomEdgeList)
                {
                    PointF? intersect = PolygonEdge.FindInsideIntersectionPoint(peTarget, us);
                    if (intersect != null) return true;
                }
            }
            return false;
        }

    }
}
