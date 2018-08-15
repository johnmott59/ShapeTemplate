using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorLayout
    {

        /// <summary>
        /// An entry point to allow the random selection of doors from the candidates. External code 
        /// can also do this if it wants, this is just for a quick result
        /// </summary>
        public void RandomlySelectDoors()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            /*
             * Do this in two stages. First select rooms that are not back rooms. We want open area doors
             * for all rooms. For the non-back rooms pick a door that is on the open area
             */
            foreach (var room in RoomList.Where(m => m.BackRoom == 0))
            {
                var list = this.GetDoorCandidates(room, true);
                // this should not actually occur, the creation of the FWDR should have marked door candidates
                if (list.Count == 0)
                {
                    continue;
                }
                int ndx = r.Next(0, list.Count - 1);
                SplitEdge(list[ndx], 10, 1, 2);
            }
            /*
             * Now process the back rooms
             */
            foreach (var room in RoomList.Where(m => m.BackRoom == 1))
            {
                var list = this.GetDoorCandidates(room);

                // this should not actually occur, the creation of the FWDR should have marked door candidates
                if (list.Count == 0)
                {
                    continue;
                }

                int ndx = r.Next(0, list.Count - 1);
                SplitEdge(list[ndx], 10, 1, 2);

            }
        }
    }
}
