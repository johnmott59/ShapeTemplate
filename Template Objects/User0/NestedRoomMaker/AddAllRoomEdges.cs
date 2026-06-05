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
        protected void AddAllRoomEdges(List<NRMEdgeList> EdgeList)
        {
            // add children
            foreach (NestedRoomMaker fli in this.Children)
            {
                fli.AddAllRoomEdges(EdgeList);
            }

            // the room edges

            foreach (var rws in this.oWorkBench.rwslist)
            {
                foreach (var room in rws.RoomList)
                {
                    NRMEdgeList el = new User0.NRMEdgeList();
                    foreach (var re in room.WallSegments)
                    {
                        el.NRMRoomEdgeList.Add(re);
                    }
                    EdgeList.Add(el);
                }
            }
        }
    }
}
