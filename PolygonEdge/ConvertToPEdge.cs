using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {
        /*
         * Convety as list of polygons stored in LineSegments into a list stored in PEedges
         */
        public static List<List<PolygonEdge>> ConvertToPEdge(List<List<LineSegment>> lsListlist)
        {
            List<List<PolygonEdge>> peListList = new List<List<PolygonEdge>>();

            // process each polygon
            foreach (List<LineSegment> lsList in lsListlist)
            {
                List<PolygonEdge> peList = new List<PolygonEdge>();

                // Process each edge, converting to a PEdge
                foreach (var ls in lsList)
                {
                    peList.Add(new PolygonEdge(ls));
                }

                peListList.Add(peList);

            }

            return peListList;

        }
    }
}
