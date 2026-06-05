using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    // This is a list of edges that used in polygon processing, although they may not form a polygon
#if false
    public partial class EdgeList
    {
        public List<RoomEdge> ToRoomEdgeList()
        {
            List<RoomEdge> reList = new List<RoomEdge>();

            foreach (var v in this.PolygonEdgeList)
            {
                reList.Add(new RoomEdge()
                {
                    From = new Point2D(v.From.X, v.From.Y),
                    To = new Point2D(v.To.X, v.To.Y),
                    Height = v.Height,
                    Width = v.Width,
                });
            }
            return reList;
        }
    }
#endif
}
