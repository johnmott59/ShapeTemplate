using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class HolePlacementTemplate
    {
        public partial class GridElementPlan : ILoadAndSaveProperties
        {
            public GridElementPlan()
            {
                this.CellFormat = "";
            }

            public GridElementPlan(string Format)
            {
                this.CellFormat = Format;
            }

        }
    }
}
