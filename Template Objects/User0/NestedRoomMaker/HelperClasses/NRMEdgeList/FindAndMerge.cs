using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    // This is a list of edges that used in polygon processing, although they may not form a polygon

    public partial class NRMEdgeList
    {
        /*
         * Move through the potential overlaps. When we find an overlap create a union and replace the list and return. 
         * this will be called until there are no more overlaps. we're looking for the following conditions
         * 1. is any polygon contained inside another? if so discard the inner one
         * 2. do any two polygons overlap? If so create a union of them
         * after we do each test we will return, so that the list will be repeatedly massaged until its done.
         * This is easier than trying to manage in between states
         */
 

        public static bool FindAndMerge(List<NRMEdgeList> PotentialOverlaps)
        {
            
            foreach (NRMEdgeList TestOver1 in PotentialOverlaps.ToList())
            {
                List<NRMEdgeList> Temp = new List<NRMEdgeList>();

                // Find each item this overlaps with 
                foreach (NRMEdgeList TestOver2 in PotentialOverlaps.Where(m=>m != TestOver1).ToList())
                {
                    // if one of these is inside the other discard the inner one. after each change, return
                    if (TestOver1.Encloses(TestOver2)) {
                        PotentialOverlaps.Remove(TestOver2);
                        return true;
                    }

                    if (TestOver2.Encloses(TestOver1))
                    {
                        PotentialOverlaps.Remove(TestOver1);
                        return true;
                    }

                    // if these two intersect then retrieve the union. the result may be 1 or more polygons depending on the shape

                    if (TestOver1.Intersects(TestOver2))
                    {
                        List<NRMEdgeList> list = TestOver2.UnionOf(TestOver1);

                        PotentialOverlaps.Remove(TestOver1);
                        PotentialOverlaps.Remove(TestOver2);
                        foreach (var v in list)
                        {
                            PotentialOverlaps.Add(v);
                        }

                        return true;

                    }

                }
            }

            return false;

        }
    }
}
