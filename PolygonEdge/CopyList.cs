using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {
        // Make a fresh copy of the list

        public static List<PolygonEdge> CopyList(List<PolygonEdge> peList)
        {
            List<PolygonEdge> list = new List<PolygonEdge>();
            foreach (var v in peList)
            {
                list.Add(new PolygonEdge()
                {
                    From = new System.Drawing.PointF() { X = v.From.X, Y = v.From.Y },
                    To = new System.Drawing.PointF() { X = v.To.X, Y = v.To.Y }
                });
            }

            return list;
        }
    }
}
