using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class NRMRoomWorkBench
    {
        // add an open area. What's being passed to us is the set of open areas that are children of the current outline.
        // we will only save the open areas that are contained within the outline

        public void AddOpenArea(List<NRMEdgeList> listlist)
        {
            OpenArea = new List<NRMEdgeList>();

            foreach (NRMEdgeList el in listlist)
            {
                if (Outline.Encloses(el))
                {
                    OpenArea.Add(el);
                }
            }
        }
    }
}
