using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class NRMRoomFinder
    {
        public NRMRoomFinder(NRMEdgeList Outline, List<NRMEdgeList> OpenAreaList) : this(Outline, OpenAreaList, null, false)
        {

        }
        
        

        public NRMRoomFinder(NRMEdgeList Outline, List<NRMEdgeList> OpenAreaList, NRMEdgeList WallSectionList, bool DebugSteps = false)
        {
            this.AllEdgeList = new List<NRMRoomEdge>();
            // Create a list of all edges.
            AllEdgeList.AddRange(Outline.NRMRoomEdgeList);

            foreach (NRMEdgeList el in OpenAreaList)
            {
                foreach (NRMRoomEdge re in el.NRMRoomEdgeList)
                {
                    if (!AllEdgeList.Contains(re))
                    {
                        AllEdgeList.Add(re);
                    }
                }
            }

            /*
             * Only add wall sections that connect on both ends to another edge. Eliminating stray edges will
             * make room finding more efficient
             */
            if (WallSectionList != null)
            {
                foreach (NRMRoomEdge p in WallSectionList.NRMRoomEdgeList)
                {
                    if (!AllEdgeList.Contains(p))
                    {
                        AllEdgeList.Add(p);
                    }
                }
            }
            
            /*
             * Make a pass through the wall sections, eliminating those that do not have connections at both ends.
             * Do this step multiple times to catch any wall segments that are more than one edge long but do 
             * not connect at one end.
             */
            bool ThereAreExposedSegments = true;
            while (ThereAreExposedSegments) {
                ThereAreExposedSegments = false;
                foreach (NRMRoomEdge p in AllEdgeList.Where(m=>m.IsInteriorWallSection).ToList())
                {
                    // Get the count of connections to this edge 
                    int fromcount = AllEdgeList.Where(m => m != p && (m.From == p.From || m.To == p.From)).Count();

                    if (fromcount == 0)
                    {
                        AllEdgeList.Remove(p);
                        ThereAreExposedSegments = true;
                        break;
                    }

                    int tocount = AllEdgeList.Where(m => m != p && (m.From == p.To || m.To == p.To)).Count();

                    if (tocount == 0)
                    {
                        AllEdgeList.Remove(p);
                        ThereAreExposedSegments = true;
                        break;
                    }
                }
            }

            // Rooms for this edge
            CompletedEdgeRoomList = new List<NRMRoom>();

            // All completed rooms for all edges referenced.
            CompletedRoomList = new List<NRMRoom>();

            if (DebugSteps)
            {
                CreationSteps = new List<List<NRMRoomEdge>>();
            }
        }
    }
}
