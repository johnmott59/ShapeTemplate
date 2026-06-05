using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class NRMRoomFinder
    {

        /// <summary>
        /// Retrieve the list of edges that connect to the 'other side' of the current edge by angle.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="StartPoint"></param>
        /// <returns></returns>
        public List<Tuple<double, NRMRoomEdge>> GetEdgesByAngle(NRMRoomEdge pCurrentEdge, eStartPosition StartPoint)
        {
            List<Tuple<double, NRMRoomEdge>> EdgeByAngleList = new List<Tuple<double, NRMRoomEdge>>();
            List<NRMRoomEdge> list = null;

            if (StartPoint == eStartPosition.FromPoint)
            {
                // Current edge starts at 'from', find all edges that connect to 'to'

                list = AllEdgeList.Where(m => m.From == pCurrentEdge.To || m.To == pCurrentEdge.To).Except(CurrentEdgeList).ToList();

                list.ForEach(m => EdgeByAngleList.Add(Tuple.Create(0.0, m)));

                Vector v = new Vector(pCurrentEdge.From.X - pCurrentEdge.To.X, pCurrentEdge.From.Y - pCurrentEdge.To.Y);

                OrderByAngle(EdgeByAngleList, v, pCurrentEdge.To,true);
            }
            else
            {
                list = AllEdgeList.Where(m => m.From == pCurrentEdge.From || m.To == pCurrentEdge.From).Except(CurrentEdgeList).ToList();

                list.ForEach(m => EdgeByAngleList.Add(Tuple.Create(0.0, m)));

                Vector v = new Vector(pCurrentEdge.To.X - pCurrentEdge.From.X, pCurrentEdge.To.Y - pCurrentEdge.From.Y);

                OrderByAngle(EdgeByAngleList, v, pCurrentEdge.From,true);
            }

            /*
             * If there are more than two edges discard the middle ones. This will mean that when an edge makes a connection
             * with other multiple edges it will only follow the leftmost and the rightmost paths
             */
            while (EdgeByAngleList.Count > 1)
            {
                EdgeByAngleList.RemoveAt(1);
            }

            return EdgeByAngleList;
        }
    }
}
