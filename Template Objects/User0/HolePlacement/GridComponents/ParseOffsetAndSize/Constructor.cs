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
        private partial class ParseOffsetAndSize
        {        
            public ParseOffsetAndSize(double Width, double Height)
            {
                GridCellWidth = Width;
                GridCellHeight = Height;
            }
        }
    }
}
