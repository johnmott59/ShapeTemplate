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
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("RandomlySelectDoors() - Building candidate list...");

            // Build a candidate list from all assembled rooms
            List<FMEdge> candidateList = new List<FMEdge>();

            if (AssembledRoomList != null && AssembledRoomList.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine($"  Checking {AssembledRoomList.Length} rooms for door candidates...");

                for (int i = 0; i < AssembledRoomList.Length; i++)
                {
                    var room = AssembledRoomList[i];
                    var candidates = GetDoorCandidates(room);
                    System.Diagnostics.Debug.WriteLine($"  Room {i}: {candidates.Count} door candidates found");

                    foreach (var edge in candidates)
                    {
                        System.Diagnostics.Debug.WriteLine($"    - Edge {edge.Index}: InteriorDoorCandidate={edge.InteriorDoorCandidate}, ExteriorWindowCandidate={edge.ExteriorWindowCandidate}");
                    }

                    candidateList.AddRange(candidates);
                }

                System.Diagnostics.Debug.WriteLine($"  Total candidates collected: {candidateList.Count}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("  WARNING: No rooms to process!");
                return;
            }

            // Call the existing method with the generated candidate list
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine($"Calling RandomlySelectDoors(candidateList) with {candidateList.Count} candidates...");
            RandomlySelectDoors(candidateList);
        }
    }
}
