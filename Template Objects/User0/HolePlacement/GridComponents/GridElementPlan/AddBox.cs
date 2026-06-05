using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class HolePlacementTemplate
    {
        public partial class GridElementPlan
        {
            // Add a box (a place for a hole) to this element

            public void AddBox(string BoxDefinition) => BoxPlanList.Add(BoxDefinition);
        }
    }
}
