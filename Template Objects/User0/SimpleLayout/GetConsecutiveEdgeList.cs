using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SimpleLayout 
    {

        /// <summary>
        /// Find the list of external edges in order. This assumes that there are no spaces, so this action should
        /// take place before any doors are added. We can use this ordered list to generate floors or ceilings
        /// </summary>
        /// <param name="OrderedEdgeList"></param>
        /// <returns></returns>
        public int GetConsecutiveEdgeList(List<Edge> OrderedEdgeList)
        {          
            List<Edge> InputList = EdgeList.Where(m => m.ID == "Window").ToList();

            Edge First = InputList[0];
            OrderedEdgeList.Add(First);

            Edge from = InputList[0];
            int FindPoint = from.p2;

            while (true)
            {
                // Get the next edge
                Edge next = InputList
                    .Where(m => m != from && (m.p1 == FindPoint || m.p2 == FindPoint))
                    .FirstOrDefault();

                if (next == null || next == First)
                {
                    break;
                }

                OrderedEdgeList.Add(next);

                from = next;
                FindPoint = next.p1 == FindPoint ? next.p2 : next.p1;
            }


            return OrderedEdgeList.Count;

        }
    }
}
