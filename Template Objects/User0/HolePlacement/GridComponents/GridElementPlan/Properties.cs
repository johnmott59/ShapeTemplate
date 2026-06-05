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
            public string CellFormat { get; set; }

            public List<GridElementPlan> ChildElementPlanList { get; set; } = new List<GridElementPlan>();

            public List<string> BoxPlanList = new List<string>();
        }
    }
}
