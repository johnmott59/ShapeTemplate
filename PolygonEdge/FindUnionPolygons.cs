using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {
        public static List<List<PolygonEdge>> FindUnionPolygons(List<PolygonEdge> p1, List<PolygonEdge> p2) 
        {
            List<List<PolygonEdge>> inputList = new List<List<PolygonEdge>>();

            inputList.Add(p1);
            inputList.Add(p2);

            return FindUnionPolygons(inputList);

        }
        /// <summary>
        /// Given a list of polygons defined by edges, return a list of the polygons that are unions
        /// of the input. This will eliminate the intersections. There may be 0, 1 or more output
        /// </summary>
        /// <param name="InputPolygons"></param>
        /// <returns></returns>
        /// 


        public static List<List<PolygonEdge>> FindUnionPolygons(List<List<PolygonEdge>> InputPolygons)
        {
            // if there is only one polygon return
            if (InputPolygons.Count == 1) return InputPolygons;
            /*
             * Find all splits
             */
            RecursivelySplit(InputPolygons, 0);

            // Create a list of all edges that will be tested
            List<PolygonEdge> AllEdges = new List<PolygonEdge>();
            InputPolygons.ForEach(m => AllEdges.AddRange(m));
            /*
             * Remove any edge that is inside another polygon
             */
            foreach (List<PolygonEdge> polygon in InputPolygons)
            {
                foreach (PolygonEdge p in polygon)
                {
                    // Search the original polygons that are not this one
                    foreach (List<PolygonEdge> innerlist in InputPolygons.Where(m => m != polygon))
                    {
                        if (PolygonEdge.IsPointInsidePolygon(p.CenterPoint, innerlist))
                        {
                            AllEdges.Remove(p);
                        }
                    }
                }
            }

            /*
             * Now find the polygons that are left. We should be able to extract a set (maybe only 1) of edges 
             * that connect end to end
             */
            List<List<PolygonEdge>> result = new List<List<PolygonEdge>>();

            // create a list of all edges that have been assigned to a polygon
            List<PolygonEdge> UsedEdges = new List<PolygonEdge>();

            foreach (PolygonEdge p in AllEdges.ToList())
            {

                // If the result list already contains this edge skip it
                if (UsedEdges.Contains(p)) continue;

                // Build a polygon
                List<PolygonEdge> NewPolygon = new List<PolygonEdge>();

                // Add this as an edge that has been used
                UsedEdges.Add(p);

                // add the first edge to the polygon
                NewPolygon.Add(p);

                BuildPolygon(AllEdges, UsedEdges, NewPolygon, p, p);

          

                if (NewPolygon.Count > 0)
                {
                    result.Add(OrderAndLink(NewPolygon));
                }

            }
            /*
             * If we have one result, return
             */
            if (result.Count <= 1) return result;
            /*
             * its possible to end up with a polygon that is completely inside the outline, make sure that all edges are not
             * part of any resulting polygon. We only want freestanding polygons
             */
            foreach (List<PolygonEdge> outer in result)
            {
                foreach (PolygonEdge p in outer.ToList())
                {
                    // search all polygons that are not this one
                    foreach (List<PolygonEdge> inner in result.Where(m => m != outer))
                    {
                        if (PolygonEdge.IsPointInsidePolygon(p.CenterPoint, inner))
                        {
                            outer.Remove(p);
                        }
                    }
                }
            }
            /*
             * IF we ended up with 0 count polygons we can remove them
             */
            foreach (List<PolygonEdge> list in result.ToList())
            {
                if (list.Count == 0) result.Remove(list);
            }

            return result;
        }
    }
}
