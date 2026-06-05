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
            public GridElementPlan AddCell(string format)
            {
                GridElementPlan gc = new GridElementPlan(format);
                ChildElementPlanList.Add(gc);
                return gc;
            }
        }
    }
}
