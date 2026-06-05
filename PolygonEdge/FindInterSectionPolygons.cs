using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib;
using System.Diagnostics;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {
        public static List<List<PolygonEdge>> FindIntersectionPolygons(List<PolygonEdge> InputPolygon, List<PolygonEdge> PEOutline)
        {
            List<List<PolygonEdge>> InputPolygonList = new List<List<PolygonEdge>>();

            InputPolygonList.Add(InputPolygon);

            return FindIntersectionPolygons(InputPolygonList, PEOutline);

        }

        public static List<List<PolygonEdge>> FindIntersectionPolygons(List<List<PolygonEdge>> InputPolygons, List<PolygonEdge> PEOutline)
        {
            /*
             * Split the inner polygons (which we know do not overlap) against the outline
             */
            foreach (List<PolygonEdge> pelist in InputPolygons)
            {
                PolygonEdge.SplitEdgesAtIntersection(pelist, PEOutline);
            }
            /*
             * Order the polygons so that they are from->to->from->to
             */
            for (int i = 0; i < InputPolygons.Count; i++)
            {
                InputPolygons[i] = PolygonEdge.OrderAndLink(InputPolygons[i]);
            }

            StringBuilder sb = new StringBuilder();
            foreach (var e in InputPolygons[0])
            {
                sb.AppendLine($"{e.From.X},{e.From.Y} -> {e.To.X}, {e.To.Y}");
            }
            //Debug.WriteLine($"Inside rect edges = {sb.ToString()}");
            string x = sb.ToString();

            PEOutline = PolygonEdge.OrderAndLink(PEOutline);

            sb = new StringBuilder();
            foreach (var e in PEOutline)
            {
                sb.AppendLine($"{e.From.X},{e.From.Y} -> {e.To.X}, {e.To.Y}");
            }
           // Debug.WriteLine($"outline rect edges = {sb.ToString()}");
            x = sb.ToString();

            // Create a list of all edges, including the edges from the split outside.
            // if any of the edges of the input are exactly the same as edges in the outline don't add them. 
            // remember that we are clipping against the outline, so if there are interior edges that match the outline
            // we want to discard them.

            List<PolygonEdge> AllEdges = new List<PolygonEdge>();
            AllEdges.AddRange(PEOutline);

            foreach (var ll in InputPolygons)
            {
                foreach (var l in ll)
                {
                    if (AllEdges.Where(
                            m => m.From.Equals(l.From) && m.To.Equals(l.To)
                        || (m.From.Equals(l.To) && m.To.Equals(l.From)))
                        .Count() == 0) {

                        AllEdges.Add(l);
                    }
                    
                }
            }
            sb = new StringBuilder();
            foreach (var e in AllEdges)
            {
                sb.AppendLine($"{e.From.X},{e.From.Y} -> {e.To.X}, {e.To.Y}");
            }
            //Debug.WriteLine($"AllEdges rect edges = {sb.ToString()}");
            x = sb.ToString();

            /*
             * We're looking for the intersection of the inner polygons with the outer polygon
             * We will only compare the edges from the inner
             */
            foreach (List<PolygonEdge> polygon in InputPolygons)
            {
                foreach (PolygonEdge p in polygon)
                {
                    if (!PolygonEdge.IsPointInsidePolygon(p.CenterPoint, PEOutline))
                    {
                        AllEdges.Remove(p);
                    }
                }
            }
            sb = new StringBuilder();
            foreach (var e in AllEdges)
            {
                sb.AppendLine($"{e.From.X},{e.From.Y} -> {e.To.X}, {e.To.Y}");
            }
           // Debug.WriteLine($"Stripped AllEdges rect edges = {sb.ToString()}");
            x = sb.ToString();

            // Now compare the outline to the inner polygons. We want to discard this segment only if its outside
            // of all the inner polygons. If its inside any of them it will become part of the intersection
            // The reason we can check against the center point is because all lines have been intersected.

            foreach (var pe in PEOutline)
            {
                bool InsideAny = false;
                foreach (var inner in InputPolygons)
                {
                    // See if these segments overlap.
                    foreach (var ed in inner)
                    {
                        if (pe.SameEndPoints(ed))
                        {
                            InsideAny = true;
                            break;
                        }
                    }

                    if (PolygonEdge.IsPointInsidePolygon(pe.CenterPoint, inner))
                    {
                        InsideAny = true;
                        break;
                    }
                }

                if (!InsideAny)
                {
                    // BREAKING Are we removing legitimate outline points here?
                   // Debug.WriteLine($"Removing Edge {pe.ToString()}, its not inside any inner polygon");
                    AllEdges.Remove(pe);
                }
            }
            sb = new StringBuilder();
            foreach (var e in AllEdges)
            {
                sb.AppendLine($"{e.From.X},{e.From.Y} -> {e.To.X}, {e.To.Y}");
            }
           // Debug.WriteLine($"after compare inner to houter AllEdges = {sb.ToString()}");
            x = sb.ToString();
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

        /// <summary>
        /// Find the intersection of a set of polygons and an outline
        /// </summary>
        /// <param name="innerList"></param>
        /// <param name="PEOutline"></param>
        /// <returns></returns>
        public static List<List<PolygonEdge>> FindIntersectionPolygons(List<List<LineSegment>> innerList, List<PolygonEdge> PEOutline)
        {
            /*
             * Convert the list of line segments to a list of pedges
             */
            List<List<PolygonEdge>> InputPolygons = PolygonEdge.ConvertToPEdge(innerList);

            return FindIntersectionPolygons(InputPolygons, PEOutline);
#if false
            /*
             * Split the inner polygons (which we know do not overlap) against the outline
             */
            foreach (List<PolygonEdge> pelist in InputPolygons)
            {
                PolygonEdge.SplitEdgesAtIntersection(pelist, PEOutline);
            }
            /*
             * Order the polygons so that they are from->to->from->to
             */
            for (int i=0; i < InputPolygons.Count; i++)
            {
                InputPolygons[i] = PolygonEdge.OrderAndLink(InputPolygons[i]);
            }

            PEOutline = PolygonEdge.OrderAndLink(PEOutline);

            // Create a list of all edges, including the edges from the split outside

            List<PolygonEdge> AllEdges = new List<PolygonEdge>();
            InputPolygons.ForEach(m => AllEdges.AddRange(m));
            AllEdges.AddRange(PEOutline);
            /*
             * We're looking for the intersection of the inner polygons with the outer polygon
             * We will only compare the edges from the inner
             */
            foreach (List<PolygonEdge> polygon in InputPolygons)
            {
                foreach (PolygonEdge p in polygon)
                {
                    if (!PolygonEdge.IsPointInsidePolygon(p.CenterPoint, PEOutline))
                    {
                        AllEdges.Remove(p);
                    }
                }
            }
            // Now compare the outline to the inner polygons. We want to discard this segment only if its outside
            // of all the inner polygons. If its inside any of them it will become part of the intersection
            foreach (var pe in PEOutline)
            {
                bool InsideAny = false;
                foreach (var inner in InputPolygons)
                {
                    if (PolygonEdge.IsPointInsidePolygon(pe.CenterPoint, inner))
                    {
                        InsideAny = true;
                        break;   
                    }
                }

                if (!InsideAny)
                {
                   // BREAKING Are we removing legitimate outline points here?
                    AllEdges.Remove(pe);
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
#endif
        }
    }
}
