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
        protected List<NRMRoom> ExtractRooms()
        {
            // given the wall list segment, the outline polygon and each child open area polygon find the rooms
            // The outlining polygon has been broken into segments that intersect with the walls
            // the outline of the open areas that are children have also been marked. 
            // this tehn becomes a graph problem of finding connected edges that are either an outline segment,
            // a wall segment or an open area segment. The polygons that are the open areas are not candidates
            // for being a room

            List<NRMRoom> roomList = new List<NRMRoom>();

            foreach (NRMEdgeList el in oWorkBench.CurrentLevelInputPolygons)
            {
                NRMRoom r = new NRMRoom();
                roomList.Add(r);
#if false
                for (int i = 0; i < el.PolygonEdgeList.Count; i++)
                {
                    if (i == 0)
                    {
                        r.StartingEdge = el.PolygonEdgeList[0];
                    } else
                    {
                        r.WallSegments.Add(el.PolygonEdgeList[i]);
                    }
                }
#endif
            }

            return roomList;

        }
    }
}
