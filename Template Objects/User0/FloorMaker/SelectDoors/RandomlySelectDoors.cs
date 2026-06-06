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
            bool hasDestinations = DestinationRoomList != null && DestinationRoomList.Count() > 0;
            bool hasMultipleRooms = AssembledRoomList != null && AssembledRoomList.Count() > 1;

            int destCount = DestinationRoomList?.Length ?? 0;
            int roomCount = AssembledRoomList?.Length ?? 0;

            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("RandomlySelectDoors(CandidateList) decision logic:");
            System.Diagnostics.Debug.WriteLine($"  DestinationRoomList count: {destCount}");
            System.Diagnostics.Debug.WriteLine($"  AssembledRoomList count: {roomCount}");
            System.Diagnostics.Debug.WriteLine($"  hasDestinations: {hasDestinations}");
            System.Diagnostics.Debug.WriteLine($"  hasMultipleRooms: {hasMultipleRooms}");
            System.Diagnostics.Debug.WriteLine($"  CandidateList count: {CandidateList.Count}");

            if (!hasDestinations && hasMultipleRooms)
            {
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("PATH 1: No destinations, multiple rooms");
                System.Diagnostics.Debug.WriteLine("  -> Calling FindConnectedRooms() starting from room 0");

                List<FMAssembledRoom> VisitedList = new List<FMAssembledRoom>();
                List<FMAssembledRoom> ConnectedToOpenArea = new List<FMAssembledRoom>();

                FindConnectedRooms(AssembledRoomList[0], VisitedList, ConnectedToOpenArea, CandidateList);

                System.Diagnostics.Debug.WriteLine($"  FindConnectedRooms complete. Visited {VisitedList.Count} rooms.");
                return;
            }

            // for each open area find the rooms that connect to it. then recurse and find all rooms that connect to that room
            // and select an edge

            if (DestinationRoomList != null && DestinationRoomList.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine($"PATH 2: Has {DestinationRoomList.Length} destination rooms");
                System.Diagnostics.Debug.WriteLine("  -> Calling BuildPathwayToDestinationRoom() for each destination");

                foreach (var oa in DestinationRoomList)
                {
                    System.Diagnostics.Debug.WriteLine($"  Processing destination room...");
                    BuildPathwayToDestinationRoom(oa, CandidateList);
                }
                return;
            }

            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("WARNING: No path taken! No doors will be assigned.");
            System.Diagnostics.Debug.WriteLine("  Possible reasons:");
            System.Diagnostics.Debug.WriteLine("  - Only 1 room and no destinations (single room doesn't need doors)");
            System.Diagnostics.Debug.WriteLine("  - DestinationRoomList is null or empty");
            System.Diagnostics.Debug.WriteLine("  - AssembledRoomList has 0 or 1 rooms");
        }



    }
}
