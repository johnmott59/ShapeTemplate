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
        // Given an origin point that a set of edges 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EdgeByAngleList">a set of edges that connect to the incoming edge at the origin point</param>
        /// <param name="Incoming"> The edge that the list connects to</param>
        /// <param name="Origin"></param>
        void OrderByAngle(List<Tuple<double, NRMRoomEdge>> EdgeByAngleList, Vector Incoming, PointF Origin,bool OrderBy)
        {
            List<Tuple<double, NRMRoomEdge>> EdgeList = new List<Tuple<double, NRMRoomEdge>>();
            /*
             * Compute vectors that represent the edges coming in to the 'origin', which is the shared point
             */
            List<Vector> vlist = new List<Vector>();
            for (int i = 0; i < EdgeByAngleList.Count; i++)
            {
                Tuple<double, NRMRoomEdge> p = EdgeByAngleList[i];

                Vector vEdge;
                if (p.Item2.To == Origin)
                {
                    vEdge = new Vector() { X = p.Item2.From.X - p.Item2.To.X, Y = p.Item2.From.Y - p.Item2.To.Y };
                }
                else
                {
                    vEdge = new Vector() { X = p.Item2.To.X - p.Item2.From.X, Y = p.Item2.To.Y - p.Item2.From.Y };
                }
                /*
                 * Get the angle between the incoming vector and each edge. The 'anglebetween' call produces 
                 * value 0 to -180 clockwise from the incoming vector and 0 to 180 counterclockwise from the incoming vector.
                 * After we adjust we will have positive values as they go clockwise.
                 */
                double theta = Vector.AngleBetween(Incoming, vEdge);
                if (theta < 0) theta = -theta;      // convert 0,-180 to 0,180
                else theta = 360 - theta;           // convert 0, 180 to 360,180

                // Add the angle and the edge for this angle
                EdgeByAngleList[i] = new Tuple<double, NRMRoomEdge>(theta, p.Item2);
            }

            // theta now has a clockwise order for all angles in the range 0 - 360. we can sort it
            if (OrderBy)
            {
                EdgeList = EdgeByAngleList.OrderBy(m => m.Item1).ToList();
            } else
            {
                EdgeList = EdgeByAngleList.OrderByDescending(m => m.Item1).ToList();
            }
            

            EdgeByAngleList.Clear();
            EdgeByAngleList.AddRange(EdgeList);

            /*
             * Finished
             */
        }
    }
}
