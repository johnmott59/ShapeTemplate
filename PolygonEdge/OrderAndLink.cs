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
        /// <summary>
        /// Reorder a list of edges so that they form a chaing of from->to->from->to. 
        /// the processing of regenerating polygons might have produced out of order chains
        /// </summary>
        /// <param name="EdgeList"></param>
        /// <returns></returns>
        static List<PolygonEdge> OrderAndLink(List<PolygonEdge> EdgeList)
        {
            List<PolygonEdge> result = new List<PolygonEdge>();

            PolygonEdge Current = EdgeList[0];
            result.Add(Current);

            for (int i = 1; i < EdgeList.Count; i++)
            {
                // Find the edge that connect to the current 'to' point
                PolygonEdge Next = EdgeList.Where(m => m != Current && (m.From == Current.To)).FirstOrDefault();

                // If we found this link and continue
                if (Next != null)
                {
                    result.Add(Next);
                    Current = Next;
                    continue;
                }
                // There must be an edge whose 'to' point points to our 'to'
                Next = EdgeList.Where(m => m != Current && (m.To == Current.To)).FirstOrDefault();

                if (Next == null) throw new Exception("Unable to link outline");
                // Swap the 'from' and the 'to' links so that its from->to->from-to
                PointF tmp = Next.From;
                Next.From = Next.To;
                Next.To = tmp;

                result.Add(Next);

                Current = Next;

            }

            return result;
        }
    }
}
