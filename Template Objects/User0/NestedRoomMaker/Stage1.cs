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

        // The first stage is to convert the shapes defining the layout into a form that we can work with and 
        // then to create the blocking by eliminating overlaps
        // This is a separate stage so that we can see the results or perhaps utilize that output

        public FLInputNodeStatus Stage1()
        {
            // we will work with copies of the data in the nodes so that this can be run over and over.
            // step 1 is to create a list of polygons that represent the overlapped shapes, but merged as much as possible

            // pass in a flag that lets us know that this is the outermost outline. That will let us know when we find
            // outline edges that are nested. Those interior outlines will be door candidates since they will have an open
            // area surrounding them. the outermost edges will be exterior window candidates and exterior door candidates

            FLInputNodeStatus sts = GetContainingPolygons(true);
            if (sts.eStatus != eFLInputNodeMessage.OK) return sts;

            // each level has a set of polygons and a set of direct children. Each child can be a list of polygons. In that
            // case we want to remove any child that either completely surrounds any parent or doesn't intersect with any parent

            RemoveIntersectsAndOverlaps();

           // foreach (var ch in this.Children)
           // {
           //     int x = ch.oWorkBench.CurrentLevelInputPolygons.Count;
           // }

            return new FLInputNodeStatus() { eStatus = eFLInputNodeMessage.OK, AdditionalInformation = "" };
        }

    }
}
