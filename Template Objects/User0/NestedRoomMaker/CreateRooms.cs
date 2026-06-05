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
       

        // if we are an open area then recurse to our outline children. If we are an outline then
        // find the intersections of walls, our open area children and our outline

        FLInputNodeStatus CreateRoomsAndOpenAreas()
        {
            /*
             * Process children first, creating rooms at the lowest level
             */
            foreach (var fli in this.Children)
            {
                FLInputNodeStatus sts = fli.CreateRoomsAndOpenAreas();
                if (sts.eStatus != eFLInputNodeMessage.OK) return sts;
            }   
 
            // If we are an outline then create rooms whose edges are outlines and walls. we can use an open area as a wall
            // but the room cannot be completely composed of open areas.

            if (true || this.OutlineType == eFLInput3OutsideType.Outline)
            {
                /*
                 * The first step towards room creation is to collect the items that can possibly form rooms.
                 * A single outline
                 * The set of wall segments that connect to that outline
                 * The set of open areas that are inside this outline 
                 */
                oWorkBench.rwslist = new List<NRMRoomWorkBench>();

                foreach (NRMEdgeList el in oWorkBench.CurrentLevelInputPolygons)
                {
                    // create a room work structure based on a single polygon from the current level
                    NRMRoomWorkBench rws = new NRMRoomWorkBench(el);

                    // Add the wall segments contained within this outline
                    rws.AddWallSegments(oWorkBench.InnerWallSegments);

                    // add the polygons of each child. because of clipping we know that all of the child polygons are inside the outline.
                    // This step will make sure that only the child polygons that are inside this particular outline polygon will be added

                    // mark each open area in this level with a flag value that will identify it to rooms and edges that connect to it
                    // this will limit the # of open areas to 32 (or 64 if we use a long)
                    int flag = 1;

                    foreach (var fli in Children)
                    {
                        // this will become redundant at some point
                        rws.AddOpenArea(fli.oWorkBench.CurrentLevelInputPolygons);
       
                        foreach (NRMEdgeList oal in fli.oWorkBench.CurrentLevelInputPolygons)
                        {
                            rws.OpenAreaList.Add(new NRMOpenArea(oal,flag));
                            flag <<= 1;  // advance the ID. 
                        }
                    }

                    // Locate the rooms in this work structure. Match them with the open areas so that we know what touches what.
                    // This knowledge will enable us to create a navigable floor

                    rws.FindRoomsAndMatchWithOpenAreas(this.OutlineType);

                    oWorkBench.rwslist.Add(rws);
                }
         
            } else
            {
                // in the case of an open areas we want to locate the rooms inside the open area. The enclosing area is
                // the open area to connect to.

                oWorkBench.rwslist = new List<NRMRoomWorkBench>();
#if false
                foreach (NRMEdgeList el in oWorkBench.CurrentLevelInputPolygons)
                {
                    // create a room work structure based on a single polygon from the current level
                    NRMRoomWorkBench rws = new NRMRoomWorkBench(el);

                    // There are no wall segments in open areas
  
                    // add the polygons of each child. because of clipping we know that all of the child polygons are inside the outline.
                    // This step will make sure that only the child polygons that are inside this particular outline polygon will be added


                    foreach (var fli in Children)
                    {
                        // this will become redundant at some point
                        rws.AddOpenArea(fli.oWorkBench.CurrentLevelInputPolygons);

                        foreach (NRMEdgeList oal in fli.oWorkBench.CurrentLevelInputPolygons)
                        {
                            rws.OpenAreaList.Add(new NRMOpenArea(oal, 0));
                           
                        }
                    }

                    // Locate the rooms in this work structure. Match them with the open areas so that we know what touches what.
                    // This knowledge will enable us to create a navigable floor

                    rws.FindRoomsAndMatchWithOpenAreas();

                    oWorkBench.rwslist.Add(rws);
                }
#endif

            }

            return new FLInputNodeStatus() { eStatus = eFLInputNodeMessage.OK };
        }


    }
}
