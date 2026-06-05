using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    // This is a list of edges that used in polygon processing, although they may not form a polygon

    public partial class NRMEdgeList
    {
        /// <summary>
        /// See if this point is also a point on this edge list.
        /// </summary>
        /// <param name="peTarget"></param>
        /// <returns></returns>
        public bool DoesEdgeConnect(PolygonEdge peTarget) 
        {
            foreach (PolygonEdge pe in NRMRoomEdgeList)
            {
                if (pe.HasCommonEndPoint(peTarget)) return true;   
            }

            return false;
        }

    }
}
