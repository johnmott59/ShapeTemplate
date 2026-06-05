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
        /// <summary>
        /// Convenience method that automatically generates candidate list and randomly selects doors.
        /// This gathers all door candidates from all assembled rooms and passes them to the main
        /// RandomlySelectDoors method. Use this for quick/automatic door placement.
        /// For more control, use the overload that takes a specific candidate list.
        /// </summary>
        public void RandomlySelectDoors()
        {
            // Build a candidate list from all assembled rooms
            List<FMEdge> candidateList = new List<FMEdge>();

            if (AssembledRoomList != null && AssembledRoomList.Length > 0)
            {
                foreach (var room in AssembledRoomList)
                {
                    var candidates = GetDoorCandidates(room);
                    candidateList.AddRange(candidates);
                }
            }
            else
            {
                // No rooms to process - this might happen if the floor layout generation failed
                // or if the input didn't define any rooms properly
                // Just return without doing anything rather than passing empty list
                return;
            }

            // Call the existing method with the generated candidate list
            RandomlySelectDoors(candidateList);
        }
    }
}
