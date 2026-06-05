using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorMaker
    {
     
        public void RandomlySelectDoors(List<FMEdge> CandidateList)
        {
            /*
             * If there are no destination areas but there are a set of rooms then pick one of the rooms and make it 
             * a destination area for purposes of making the area navigable. This may not create 
             * a pattern that is pleasing but it will work. The purpose of destination areas is to make 
             * sense of connecting rooms through shared spaces
             */
             if (DestinationRoomList.Count() == 0 && AssembledRoomList.Count() > 1)
            {
                List<FMAssembledRoom> VisitedList = new List<FMAssembledRoom>();
                List<FMAssembledRoom> ConnectedToOpenArea = new List<FMAssembledRoom>();

                FindConnectedRooms(AssembledRoomList[0], VisitedList, ConnectedToOpenArea, CandidateList);
             
                return;
            }

            // for each open area find the rooms that connect to it. then recurse and find all rooms that connect to that room
            // and select an edge

            foreach (var oa in DestinationRoomList)
            {
                BuildPathwayToDestinationRoom(oa, CandidateList);
            }
        }



    }
}
