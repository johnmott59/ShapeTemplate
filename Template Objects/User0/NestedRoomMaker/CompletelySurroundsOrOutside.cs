using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;
using ShapeTemplateLib;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// Input to generate a floor layout, containing a tree of open areas and outlines. 
    /// </summary>

    public partial class NestedRoomMaker
    {
#if false
        // see if a child polygon completely surrounds the test polygon
        protected bool CompletelySurroundsOrOutside(List<EdgeList> child, List<EdgeList> parent)
        {
            // See if each edge in the child is outside the parent. If both end points are outside the parent we call it outside even thought
            // it could be intersecting

            foreach (EdgeList chEdge in child)
            {
                foreach (PolygonEdge pe in chEdge.PolygonEdgeList)
                {
                    foreach (EdgeList el in parent)
                    {
                        foreach (PolygonEdge paEdge in el.PolygonEdgeList)
                        {
                            bool bFrom = PolygonEdge.IsPointInsidePolygon(pe.From, el);
                            bool bTo = PolygonEdge.IsPointInsidePolygon(pe.To, el);

                            // if either or both endpoints are inside the parent then it doesn't completely surround it
                            if (!bFrom || !bTo) return false;
                        }
                    }
                }
            }
            // for now say falsse
            return true;
        }
#endif

    }
}
