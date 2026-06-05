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
        // This is a graph test, need to code carefully
        protected bool EdgeConnectsTo(List<PolygonEdge> peChainHeader, PolygonEdge peTest)
        {
            // 1. make a quick pass to see if there are common end points with the edges that, that will answer the question
            foreach (PolygonEdge pe in peChainHeader)
            {
                if (pe.HasCommonEndPoint(peTest)) return true;
            }


            return false;

        }
    }
}
