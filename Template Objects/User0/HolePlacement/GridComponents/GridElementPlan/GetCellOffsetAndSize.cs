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
            public OffsetAndSize GetCellOffsetAndSize(double Offset, double ThisCellSize, OffsetAndSize ContainerCell)
            {

                if (CellFormat.Contains("width"))
                {
                    // Column
                    return new OffsetAndSize()
                    {
                        Offset = new Point2D()
                        {
                            X = (float)Offset,
                            Y = ContainerCell.Offset.Y
                        },
                        Height = (float)ContainerCell.Height,
                        Width = (float)ThisCellSize
                    };
                }
                else
                {
                    // Row
                    return new OffsetAndSize()
                    {
                        Offset = new Point2D()
                        {
                            X = (float)ContainerCell.Offset.X,
                            Y = (float)Offset
                        },
                        Height = (float)ThisCellSize,
                        Width = (float)ContainerCell.Width
                    };
                }
            }
        }
    }
}
