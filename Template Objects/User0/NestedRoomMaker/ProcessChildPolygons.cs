using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// Input to generate a floor layout, containing a tree of open areas and outlines. 
    /// </summary>

    public partial class NestedRoomMaker
    {
        // Step 1 is to get the containing polygons as an edgelist
        protected FLInputNodeStatus GetContainingPolygons(bool IsOutermost)
        {
            // get our children
            foreach (NestedRoomMaker fli in Children)
            {
                FLInputNodeStatus sts = fli.GetContainingPolygons(false);
                if (sts.eStatus != eFLInputNodeMessage.OK) return sts;
            }

            // get our containing polygon and save it to the workbench. The result can be a single polygon or a list,
            // but will not contain any overlapping polygons, it will find the union.

            Tuple<FLInputNodeStatus,List<NRMEdgeList>> s = CombineComponents(HoleGroup);
            if (s.Item1.eStatus != eFLInputNodeMessage.OK) return s.Item1;

            oWorkBench.CurrentLevelInputPolygons = s.Item2;
            /*
             * Mark these edges
             */
            string ID = this.OutlineType == eFLInput3OutsideType.Outline ? "Outline" : "OpenArea";
            bool IsOutline = this.OutlineType == eFLInput3OutsideType.Outline ? true : false;

            // determine if this is an interior door candidate
            bool InteriorDoorCandidate = false;

            // if this is an open area then every edge is a door candidate
            if (this.OutlineType == eFLInput3OutsideType.OpenArea) InteriorDoorCandidate = true;

            // if this is an outline type and we're not the outermost edge then each edge is a door candidate, because
            // the edges touch the enclosing open area
            if (this.OutlineType == eFLInput3OutsideType.Outline && !IsOutermost) InteriorDoorCandidate = true;

            bool ExteriorWindowCandidate = false;
            // The outermost outline edges are candidates for windows
            if (IsOutermost) ExteriorWindowCandidate = true;

            foreach (NRMEdgeList el in oWorkBench.CurrentLevelInputPolygons)
            {
                foreach (NRMRoomEdge re in el.NRMRoomEdgeList)
                {
                    re.ID = this.IDForEdges;
                    re.InteriorDoorCandidate = InteriorDoorCandidate;
                    re.ExteriorWindowCandidate = ExteriorWindowCandidate;

                    // These values are used when building rooms
                    re.IsExteriorEdge = this.OutlineType == eFLInput3OutsideType.Outline ? 1 : 0;
                    re.IsOpenSpaceEdge = this.OutlineType == eFLInput3OutsideType.OpenArea ? 1 : 0;
                }
            }


            // Convert any walls at this level into an edge list to work with
            oWorkBench.InnerWallSegments = new NRMEdgeList();
            oWorkBench.InnerWallSegments.NRMRoomEdgeList = new List<NRMRoomEdge>();

            foreach (var v in this.InteriorWallSegmentArray)
            {
                oWorkBench.InnerWallSegments.NRMRoomEdgeList.Add(new NRMRoomEdge(v,"InteriorWall"));
            }

            

            return s.Item1;
        }

#if true
        /*
         * Given another node remove any polygons which enclose our polygons
         */
        protected void ClipIntersectingPolygons(NestedRoomMaker MyChild)
        {
            for (int i=0; i < this.oWorkBench.CurrentLevelInputPolygons.Count; i++) {

                NRMEdgeList us = this.oWorkBench.CurrentLevelInputPolygons[i];

                // compare to each child polygon.
                foreach (NRMEdgeList MyChildEdgeList in MyChild.oWorkBench.CurrentLevelInputPolygons.ToList())
                {
                    // if this child intersects us remove it
                    if (MyChildEdgeList.Intersects(us))
                    {
                        // If we successfully clip the children we'll end up modifying the parent
                        NRMEdgeList ModifiedUs = new NRMEdgeList();

                        // we want to clip this child against us.
                        var NewMyChildEdgeList = Clip(MyChildEdgeList, us, ModifiedUs);

                        // If our count of edges changed replace it with the modified edg
                        if (ModifiedUs.NRMRoomEdgeList.Count != us.NRMRoomEdgeList.Count)
                        {
                            us = this.oWorkBench.CurrentLevelInputPolygons[i] = ModifiedUs;
                        }

                        // the new polygons created as a result of the clip have been stripped of the flag that 
                        // indicates whether they are open area edges or outlines; replace them

                        if (this.OutlineType == eFLInput3OutsideType.Outline)
                        {
                            ModifiedUs.NRMRoomEdgeList.ForEach(m => m.IsExteriorEdge = 1);
                            NewMyChildEdgeList.ForEach(m=>m.NRMRoomEdgeList.ForEach(n=>n.IsOpenSpaceEdge = 1));
                        } else
                        {
                            ModifiedUs.NRMRoomEdgeList.ForEach(m => m.IsOpenSpaceEdge = 1);
                            NewMyChildEdgeList.ForEach(m => m.NRMRoomEdgeList.ForEach(n => n.IsExteriorEdge = 1));  
                        }

                        // replace the mychild polygon with the new child list
                        MyChild.oWorkBench.CurrentLevelInputPolygons.Remove(MyChildEdgeList);
                        MyChild.oWorkBench.CurrentLevelInputPolygons.AddRange(NewMyChildEdgeList);
                       
                    }
                }
            }
        }
#endif
        /*
         * Given another node remove any polygons which enclose our polygons
         */
        protected void DiscardIntersectingPolygons(NestedRoomMaker MyChild)
        {
            foreach (NRMEdgeList us in this.oWorkBench.CurrentLevelInputPolygons)
            {
                foreach (NRMEdgeList MyChildEdgeList in MyChild.oWorkBench.CurrentLevelInputPolygons.ToList())
                {
                    // if this child intersects us remove it
                    if (MyChildEdgeList.Intersects(us))
                    {
                        MyChild.RemovePolygon(MyChildEdgeList);
                    }
                }
            }
        }

        /*
         * Given another node remove any polygons which enclose our polygons
         */
        protected void DiscardEnclosingPolygons(NestedRoomMaker MyChild)
        {
            foreach (NRMEdgeList us in this.oWorkBench.CurrentLevelInputPolygons)
            {
                // Does this polygon enclose our polygons? if so discard it
                foreach (NRMEdgeList MyChildEdgeList in MyChild.oWorkBench.CurrentLevelInputPolygons.ToList())
                {
                    // if  this child encloses us remove it
                    if (MyChildEdgeList.Encloses(us))
                    {
                        MyChild.RemovePolygon(MyChildEdgeList);
                    }
                }
            }
        }

        protected void RemovePolygon(NRMEdgeList polygon)
        {
            this.oWorkBench.CurrentLevelInputPolygons.Remove(polygon);
        }

        // discard any polygons from this node which are not contained within at least one of ours
        // and which do not intersect any of ours
        protected void DiscardOutsideNonIntersectingPolygons(NestedRoomMaker MyChild)
        {
            foreach (NRMEdgeList MyChildEdgeList in MyChild.oWorkBench.CurrentLevelInputPolygons.ToList())
            {
                bool Keeper = false;

                // do any of our polygons enclose the polygon in the target?

                foreach (NRMEdgeList us in this.oWorkBench.CurrentLevelInputPolygons)
                {
                    // if we enclose this target or we intersect it its a good polygon
                    // if there is an intersection we don't process that at this stage, this step 
                    // is a validation step

                    if (us.Encloses(MyChildEdgeList) || MyChildEdgeList.Intersects(us))
                    {
                        Keeper = true;
                        break;                       
                    }
                }

                // none of our polygons completely enclose this polygon in the target -- its outside of all of them
                if (!Keeper)
                {
                    // If this polygon intersects us we'll keep it
                    MyChild.RemovePolygon(MyChildEdgeList);                  
                }
            }

        }

        // discard any polygons from this node which contained within at least one of ours
        protected void DiscardInsidePolygons(NestedRoomMaker target)
        {
            foreach (NRMEdgeList us in oWorkBench.CurrentLevelInputPolygons)
            {
                // is this polygon enclosed by one of ours? if so discard it
                foreach (NRMEdgeList them in target.oWorkBench.CurrentLevelInputPolygons.ToList())
                {
                    // if we enclose this child remove it
                    if (us.Encloses(them))
                    {
                        target.RemovePolygon(them);
                    }
                }
            }
        }


        // Step 2 is to remove any orphans and clip children against their parents
        // If a child polygon completely surrounds any of its parents or doesn't intersect with any of its parents then delete it
        // we do this depth first so that the lower levels are removed before upper levels. This will improve efficiency
        
        protected void RemoveIntersectsAndOverlaps()
        {
            // Recurse

            foreach (NestedRoomMaker fli in Children)
            {
                fli.RemoveIntersectsAndOverlaps();
            }
            /*
             * Remove any polygons which enclose the parent or are completely outside the parent.
             * If the child intersects the parent clip it
             */
            foreach (NestedRoomMaker fliChild in Children)
            {               
                // discard any children that completely enclose us
                DiscardEnclosingPolygons(fliChild);

                // discard any children  that are outside of all our polygons
                DiscardOutsideNonIntersectingPolygons(fliChild);

                // clip intersecting polygons against the parent
                ClipIntersectingPolygons(fliChild);
              
            }

            // now make sure that no cousins overlap or enclose each other. We want only separated cousins 
            // with no overlap

            foreach (NestedRoomMaker fliChild in Children)
            {
                foreach (NestedRoomMaker fliCousin in Children.Where(m=>m != fliChild))
                {
                    // discard any cousins that intersect with any of us
                    fliChild.DiscardIntersectingPolygons(fliCousin);

                    // discard any cousins that we contain
                    fliChild.DiscardInsidePolygons(fliCousin);
                }
            }
     
        }


    }
}
