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
        // create boxes that represent the leaves of the grid tree. 

        // Recurse into the grid, adding to the grid list when we find a 
        // row and column that have no row and column children
#if true
        public partial class GridElement
        {
            public OffsetAndSize GetBoxOffsetAndSize(string FormatString, List<string> StatusMessage)
            {
                ParseOffsetAndSize oHelper = new ParseOffsetAndSize(CellOffsetAndSize.Width, CellOffsetAndSize.Height);

                if (!oHelper.DecodeString(FormatString))
                {
                    StatusMessage.AddRange(StatusMessage);
                    return null;
                }

                bool bVisible = true;

                // get box offset, width and height. If the cell doesn't completely contain the item its invisible
                DirectionalOffsetAndSize oWidth = oHelper.GetWidth();
                if (oWidth.Offset + oWidth.Size >= CellOffsetAndSize.Width) bVisible = false;

                DirectionalOffsetAndSize oHeight = oHelper.GetHeight();
                if (oHeight.Offset + oHeight.Size >= CellOffsetAndSize.Height) bVisible = false;

                return new OffsetAndSize()
                {
                    IsContainedInCell = bVisible,

                    Width = (float)oWidth.Size,
                    Height = (float)oHeight.Size,

                    Offset = new Point2D()
                    {
                        X = (float) oWidth.Offset + CellOffsetAndSize.Offset.X,
                        Y = (float) oHeight.Offset + CellOffsetAndSize.Offset.Y
                    }
                };

            }
        }

#endif


    }
}
