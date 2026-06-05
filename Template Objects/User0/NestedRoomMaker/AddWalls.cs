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
        /// <summary>
        /// Add the walls. For each level where walls are defined this will add intersections of walls and open areas.
        /// </summary>
        /// <returns></returns>
        FLInputNodeStatus AddWalls()
        {
            /*
             * Do depth first
             */
            foreach (NestedRoomMaker fli in Children)
            {
                fli.AddWalls();
            }

            // only process walls if this is an outline. 
         //   if (this.OutlineType != eFLInput3OutsideType.Outline) return new FLInputNodeStatus() { eStatus = eFLInputNodeMessage.OK };


            // we know that we have a set of outlines, and that each outline can contain a set of open areas.
            // we also know that the open areas are containined within the outlines. what we want to do is to
            // 1. split each wall segment with the intersections of walls
            // 2. split each open area that we contain with wall segments
            // 3. Trim the wall segments that extend outside the outline

            // split the walls with themselves
            oWorkBench.InnerWallSegments.SplitAgainst(oWorkBench.InnerWallSegments);
         

            // Split the walls against the outlines
            foreach (NRMEdgeList el in oWorkBench.CurrentLevelInputPolygons)
            {
                // Split each outline polygon against the walls
                el.SplitAgainst(oWorkBench.InnerWallSegments);
            }

            // split each child (which is an open area) against the walls
            foreach (NestedRoomMaker fli in Children)
            {
                foreach (NRMEdgeList el in fli.oWorkBench.CurrentLevelInputPolygons)
                {
                    // Split each outline polygon against the walls
                    el.SplitAgainst(oWorkBench.InnerWallSegments);
                }
            }
            /*
             * Now remove any of the wall segments that are completely outside the outline or are inside 
             * any of the inside areas 
             */
            foreach (NRMRoomEdge re in oWorkBench.InnerWallSegments.NRMRoomEdgeList.ToList())
            {
                // if this edge isn't inside one of the outlines remove it

                if (!IsEdgeInsideCurrentInput(re))
                {
                    oWorkBench.InnerWallSegments.NRMRoomEdgeList.Remove(re);
                    continue;
                }

                // Now compare to the list of children, all of whom are open areas
                foreach (NestedRoomMaker fli in Children)
                {
                    if (fli.IsEdgeInsideCurrentInput(re))
                    {
                        oWorkBench.InnerWallSegments.NRMRoomEdgeList.Remove(re);
                        break;
                    }
                }
            }

            return new FLInputNodeStatus() { eStatus = eFLInputNodeMessage.OK, AdditionalInformation = "" };

        }
    }
}
