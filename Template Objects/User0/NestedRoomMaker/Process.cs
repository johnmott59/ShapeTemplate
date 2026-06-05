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


        // sketching out processing
        public FLInputNodeStatus Process()
        {

            // we will work with copies of the data in the nodes so that this can be run over and over.
            // step 1 is to create a list of polygons that represent the overlapped shapes, but merged as much as possible

            FLInputNodeStatus sts = Stage1();
            if (sts.eStatus != eFLInputNodeMessage.OK) return sts;

            // next step is to take the wall sections that were defined and intersect them in each outline area with their
            // respective outlines and enclosed open areas in order to create rooms.

            AddWalls();

            // if we are an open area then walls don't affect us and we will call each child,
            // which we know to be an outline, and let them carve their space up with walls

            sts = CreateRoomsAndOpenAreas();

            return sts;

        }
    }
}
