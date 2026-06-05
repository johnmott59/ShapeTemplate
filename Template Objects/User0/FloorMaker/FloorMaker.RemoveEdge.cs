using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates;
using ShapeTemplateLib;
using ShapeTemplateLib.BasicShapes;
using ShapeTemplateLib.Templates.User0;
using System.Xml.Linq;
using System.Drawing;

namespace ShapeTemplateLib.Templates.User0
{
    // Rename this floorwithrooms and delete the old template
    public partial class FloorMaker : TemplateBaseClass
    {


        public void RemoveEdge(PointF p1, PointF p2)
        {
            FMEdge fm = FindEdge(p1, p2);

            if (fm == null) return;

            RemoveEdge(fm);
        }


        /// <summary>
        /// this will delete an edge 
        /// </summary>
        /// <param name="oEdge"></param>

        /// <returns></returns>
        public void RemoveEdge(FMEdge oEdge)
        {
            // we shouldn't see the same edge twice

            // Get the index of this edge and remove it from any of the rooms
                        
            foreach (var room in AssembledRoomList.ToList())
            {
                List<int> NewEdgeList = new List<int>();
                foreach (var ed in room.EdgeIndexList)
                {
                    if (ed == oEdge.Index) continue;    // skip this one, we're deleting it
                    NewEdgeList.Add(ed);
                }
                // replace the index list with the desired edge removed
                room.EdgeIndexList = NewEdgeList.ToArray();

                // If we've removed all of the edges from this room, remove the room
                if (room.EdgeIndexList.Count() == 0)
                {
                    List<FMAssembledRoom> rtmp = AssembledRoomList.ToList();
                    rtmp.Remove(room);
                    AssembledRoomList = rtmp.ToArray();
                }
                
            }

            foreach (var dest in DestinationRoomList)
            {
                List<int> NewEdgeList = new List<int>();
                foreach (var ed in dest.EdgeIndexList)
                {
                    if (ed == oEdge.Index) continue;    // skip this one, we're deleting it
                    NewEdgeList.Add(ed);
                }
                // replace the index list with the desired edge removed
                dest.EdgeIndexList = NewEdgeList.ToArray();
            }

            // now remove this from the edge list
            List<FMEdge> tmp = new List<FMEdge>();

            foreach (var e in this.EdgeList)
            {
                if (oEdge.Index == e.Index) continue;
                tmp.Add(e);
            }
            this.EdgeList = tmp.ToArray();
        
        }
    }

  
}
