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
        // This is a leaf element in the grid

        public partial class GridElement
        {
            // Offset and size of this element
            public OffsetAndSize CellOffsetAndSize { get; set; } = new OffsetAndSize();
 
            // offsets and sizes of boxes within this cell, taking into account the cell
            public List<OffsetAndSize> BoxOffsetAndSizeList { get; set; } = new List<OffsetAndSize>();
        }
    }
}
