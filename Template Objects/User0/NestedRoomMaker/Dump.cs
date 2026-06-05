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
        public void Dump(int nest)
        {
            // show our edges and wall segments first, then our children

            ShowSpaces(nest);
            Console.Write(eFLInput3OutsideType.Outline.ToString());

            foreach (NRMEdgeList el in oWorkBench.CurrentLevelInputPolygons)
            {
                ShowSpaces(nest);
                Console.WriteLine(" -- Edge List --");
                foreach (PolygonEdge pe in el.NRMRoomEdgeList)
                {
                    ShowSpaces(nest);
                    Console.WriteLine("{4} {0},{1} -> {2},{3}", pe.From.X, pe.From.Y, pe.To.X, pe.To.Y,pe.ID);
                }
            }

            ShowSpaces(nest);
            Console.WriteLine(" -- Interior Wall Segment --");
            foreach (PolygonEdge pe in oWorkBench.InnerWallSegments.NRMRoomEdgeList)
            {
                ShowSpaces(nest);
                Console.WriteLine("{0},{1} -> {2},{3}", pe.From.X, pe.From.Y, pe.To.X, pe.To.Y);
            }

            // now do children

            foreach (NestedRoomMaker fli in this.Children)
            {
                fli.Dump(nest + 1);
            }
        }

        public void ShowSpaces(int nest)
        {
            for (int i=0; i < nest; i++)
            {
                Console.Write(" "); 
            }
        }
    }
}
