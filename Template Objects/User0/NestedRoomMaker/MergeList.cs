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
        // Take a list of boundary polygons and find all of the unions that we can find. 

        protected List<NRMEdgeList> MergeList(List<NRMEdgeList> bpList)
        {
            // handle simple case
            if (bpList.Count <= 1) return bpList;

            return bpList;
        }
    }
}
