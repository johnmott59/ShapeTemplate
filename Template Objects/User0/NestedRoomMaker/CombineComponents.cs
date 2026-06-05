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
        // the single hole group contains a set of polygons at this level
        // This may result in a set of polygons or a single polygon
        // This processing will find the union of all of these so that there are no overlapping polygons
        // or polygons which are completely inside another one

        protected Tuple<FLInputNodeStatus, List<NRMEdgeList>> CombineComponents(SingleHoleGroup HoleGroup)
        {
#if true

            if (HoleGroup.EllipseArray.Count() == 0 
                && HoleGroup.PolygonArray.Count() == 0
                && HoleGroup.RectangleArray.Count() == 0)
            {
                return new Tuple<FLInputNodeStatus, List<NRMEdgeList>>(
                    new FLInputNodeStatus()
                    {
                        eStatus = eFLInputNodeMessage.NoShapesDefined,
                        AdditionalInformation = "Empty Holegroup"
                    },
                        new List<NRMEdgeList>()
                    );
            }
            // during development return the first rectangle as the combined component
            // Collect a list of these polygons

            List<NRMEdgeList> edgelist = new List<NRMEdgeList>();

            foreach (var lh in HoleGroup.oHoleGroup.HoleList)
            {
                edgelist.Add(new NRMEdgeList(HoleGroup.GetHoleAsPolygon(lh)));
            }

            /*
             * Combine these polygons, forming a union where there is overlap. the result should be 1 or more polygons with no overlap
             */
            bool result = true;
            do
            {
                result = NRMEdgeList.FindAndMerge(edgelist);

            } while (result);

            FLInputNodeStatus sts = new FLInputNodeStatus() { eStatus = eFLInputNodeMessage.OK };


            return new Tuple<FLInputNodeStatus, List<NRMEdgeList>>(sts, edgelist);
         
#endif
        }
    }
}
