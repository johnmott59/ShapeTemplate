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
        /// This will find all intersections of all polygons in a list. It will use recursion to proess the
        /// list.
        /// if there are 0 or 1 in the list, its done
        /// if there are 2 in the list it will 
        ///     split [0] and [1]
        /// if there are 3 in the list it will
        ///     split [0] and [1]
        ///     split [2] and [1]
        ///     split [2] and [0]
        /// if there are 4 in the list it will
        ///     split [0] and [1]
        ///     split [2] and [1]
        ///     split [2] and [0]
        ///     split [3] and [2]
        ///     split [3] and [1]
        ///     split [3] and [0]
        ///     
        /// in this way each combination of items will be processed against the other
        ///     
        /// </summary>
        /// <param name="InputList"></param>
        /// <param name="StartIndex"></param>
        private static void RecursivelySplit(List<List<PolygonEdge>> InputList, int StartIndex)
        {
            // Terminating condition. 
            // If we're at the end of the list we're done
            if (StartIndex == InputList.Count) return;

            // Split the items further down the list

            RecursivelySplit(InputList, StartIndex + 1);

            // Process this index with all the ones beneath it

            for (int i = StartIndex - 1; i >= 0; i--)
            {
                SplitEdgesAtIntersection(InputList[StartIndex], InputList[i]);
            }

        }
    }
}
