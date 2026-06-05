using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShapeTemplateLib.Templates.User0.NestedRoomMaker;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class NRMRoomWorkBench
    {

        /// <summary>
        /// 
        /// </summary>
        public void FindRoomsAndMatchWithOpenAreas(eFLInput3OutsideType OutlineType)
        {

            NRMExtractRooms xr = new NRMExtractRooms(this.Outline, this.OpenArea, this.WallSegments);


            // these are the rooms and the open areas
            var x = xr.RoomList;
            var y = xr.OpenAreaList;

            this.RoomList.Clear();
            this.OpenAreaList.Clear();

            // if the outer is an open area swap the outline and open area

            if (OutlineType == eFLInput3OutsideType.OpenArea)
            {
                List<NRMRoom> xrRoomList = new List<NRMRoom>();

                foreach (var v in xr.OpenAreaEdgeList) //  xr.RoomEdgeList)
                {
                    xrRoomList.Add(new NRMRoom()
                    {
                        WallSegments = new List<NRMRoomEdge>(v.NRMRoomEdgeList)
                    });
                }

                List<NRMOpenArea> xrOpenAreaList = new List<NRMOpenArea>();
                foreach (var v in xr.RoomEdgeList) //  xr.OpenAreaEdgeList)
                {
                    xrOpenAreaList.Add(new NRMOpenArea(v, 1));
                }

                this.RoomList.AddRange(xrRoomList);
                this.OpenAreaList.AddRange(xrOpenAreaList);

            } else
            {

                this.RoomList.AddRange(xr.RoomList);
                this.OpenAreaList.AddRange(xr.OpenAreaList);
            }

    


            // TBD The edges are all shared here, and we haven't created room structs. 
            // 1. create room structs
            // 2. create unique copies of edges, order them and wind them counter clockwise
            // 3. Eliminate any room that contains points that are inside another room. Shared points are fine,
            // we're looking for points that are not on the list of edges and that are inside


            // Make sure that the rooms are end to end and wound counter clockwise
            string roomlist = "";
            foreach (NRMRoom r in RoomList)
            {
                r.Cleanup();
                StringBuilder sb = new StringBuilder();

               
            }
            string rl = roomlist;

            // for each room, find the open areas that this shares edges with

            // this is failing when we create rooms by crossing wall sections. its not correctly picking up
            // that one of the rooms encloses an open area

            foreach (var r in RoomList)
            {
                foreach (var o in OpenAreaList)
                {
                    float f = r.Area;
                  
                    // if this open area is completely inside this room then its connected
                    if (r.Encloses(o))
                    {
                        r.ConnectedOpenAreas.Add(o);
                        o.ConnectedRooms.Add(r);

                        // mark all edges of this room as connecting to the open area
                        foreach (var w in r.WallSegments)
                        {
                            w.ConnectedOpenAreas.Add(o);
                        }
                    }
                    else
                    {
                        // if there are common edges with an open area attach each to the other, the open area
                        // to the outline and vice versa
                        MarkConnectedRooms(r, o);
                    }
                }
            }
        }

        /// <summary>
        /// Rooms and open areas can have common edges. Each contains a list to the edges of the other
        /// </summary>
        /// <param name="oRoom"></param>
        /// <param name="oOpenArea"></param>
        protected void MarkConnectedRooms(NRMRoom oRoom, NRMOpenArea oOpenArea)
        {
            foreach (var r1 in oRoom.WallSegments)
            {
                if (WallSegmentContainsSegment(oOpenArea.WallSegments,r1)     //oOpenArea.WallSegments.Contains(r1) 
                    && !oOpenArea.ConnectedRooms.Contains(oRoom)
                    && !oRoom.ConnectedOpenAreas.Contains(oOpenArea)
                    )
                {
                    r1.ConnectedOpenAreas.Add(oOpenArea);
                    
                    // mark this room as connecting to this open area, and vice versa
                    oRoom.ConnectedOpenAreas.Add(oOpenArea);
                    oOpenArea.ConnectedRooms.Add(oRoom);
                }
            }
        }

        // see if a segment is in a list. They are separate objects but their From and To are identical if they are the same
        private bool WallSegmentContainsSegment(List<NRMRoomEdge> reList,NRMRoomEdge reTest)
        {
            foreach (var re in reList)
            {
                if (re.From.Equals(reTest.From) && re.To.Equals(reTest.To))
                {
                    return true;
                }

                if (re.From.Equals(reTest.To) && re.To.Equals(reTest.From))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
