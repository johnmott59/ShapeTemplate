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
        protected List<NRMEdgeList> Clip(NRMEdgeList child, NRMEdgeList parent, NRMEdgeList ModifiedParentList)
        {
            // find the intersection of these two and replace the child with that intersection

            // Create a copy of the parent list. This copy will reveal where there were splits in the parent
            // if we clipped against it

            List<PolygonEdge> pelist = new List<PolygonEdge>(parent.NRMRoomEdgeList.ToList<PolygonEdge>());

            List<List<PolygonEdge>> list1 = PolygonEdge
                .FindIntersectionPolygons(child.NRMRoomEdgeList.ToList<PolygonEdge>(),
                pelist);
            
            // We've lost the open areas

            List<NRMEdgeList> list2 = new List<NRMEdgeList>();

            foreach (var v in list1)
            {
                list2.Add(new NRMEdgeList(v));
            }

            ModifiedParentList.NRMRoomEdgeList = new NRMEdgeList(pelist).NRMRoomEdgeList;

            // copy over properties associated with the source of the modified parent
            foreach (var v in ModifiedParentList.NRMRoomEdgeList)
            {
                v.ConnectedOpenAreaFlag = parent.NRMRoomEdgeList[0].ConnectedOpenAreaFlag;
                v.ExteriorWindowCandidate = parent.NRMRoomEdgeList[0].ExteriorWindowCandidate;
                v.IsExteriorEdge = parent.NRMRoomEdgeList[0].IsExteriorEdge;
                v.IsOpenSpaceEdge = parent.NRMRoomEdgeList[0].IsOpenSpaceEdge;
                v.IsInteriorWallSection = parent.NRMRoomEdgeList[0].IsInteriorWallSection;
            }
            
           
            return list2;
        }
    }
}
