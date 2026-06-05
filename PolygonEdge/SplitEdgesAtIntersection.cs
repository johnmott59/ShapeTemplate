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
        public static void SplitEdgesAtIntersection(List<PolygonEdge> PEOpenArea, List<PolygonEdge> PEOutline)
        {
            // catch one or both lists null or empty
            if (PEOpenArea == null || PEOpenArea.Count == 0 || PEOutline == null || PEOutline.Count == 0) return;

            // Set a tolerence of the thinnest edge. 

            bool ThereAreSplits = true;

            while (ThereAreSplits)
            {
                ThereAreSplits = false;

                for (int i = 0; i < PEOpenArea.Count && ThereAreSplits == false; i++)
                {
                    PolygonEdge S1 = PEOpenArea[i];
                    for (int j = 0; j < PEOutline.Count && ThereAreSplits == false; j++)
                    {
                        PolygonEdge S2 = PEOutline[j];

                        PointF? p = PolygonEdge.FindInsideIntersectionPoint(S1, S2);

                        string _s1 = $"{S1.From.X},{S1.From.Y} -> {S1.To.X},{S1.To.Y}";
                        string _s2 = $"{S2.From.X},{S2.From.Y} -> {S2.To.X},{S2.To.Y}";

                        if (p != null)
                        {
                            // TBD
                            // If the two line segments form a 'T' then we don't need to add a line segment that
                            // would be 0 length


                            // TBD make sure its not too close to an existing point

                            // Create two new segments with the intersection as the 'from' point. 
                            // We want each new section to have the appropriate id so we know where they came from

                            // if the shared point was the endpoint of one of the segments then don't modify that segment

                            if (!p.Value.Equals(S1.From) && !p.Value.Equals(S1.To))
                            {
                                PolygonEdge pe1 = new PolygonEdge(p.Value, S1.To);
                                pe1.Width = S1.Width;
                                pe1.Height = S1.Height;
                                pe1.ID = S1.ID;
                                PEOpenArea.Insert(i + 1, pe1);
                                S1.To = p.Value;
                                ThereAreSplits = true;
                            }

                            if (!p.Value.Equals(S2.From) && !p.Value.Equals(S2.To))
                            {
                                PolygonEdge pe2 = new PolygonEdge(p.Value, S2.To);
                                pe2.Width = S1.Width;
                                pe2.Height = S1.Height;
                                pe2.ID = S2.ID;
                                PEOutline.Insert(j + 1, pe2);
                                S2.To = p.Value;
                                ThereAreSplits = true;
                            }                         
                        }

                    }
                }

            }
        }
    }
}
