using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorMaker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="room1"></param>
        /// <param name="room2"></param>
        /// <returns></returns>
        private List<int> GetCommonEdges(FMAssembledRoom room1, FMAssembledRoom room2)
        {
            List<int> CommonEdgeList = new List<int>();

            foreach (var e1 in room1.EdgeIndexList)
            {
                if (room2.EdgeIndexList.Contains(e1))
                {
                    CommonEdgeList.Add(e1);
                }
            }

            return CommonEdgeList;
        }
    }
}
