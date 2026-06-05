using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class HolePlacementTemplate
    {
        /// <summary>
        /// The Grid Cell Plan contains the definitions for the cells in text form with repetition data, like "width=40,4".
        /// These text versions are compiled ('decoded') into the final full layout once the overall width and height are defined
        /// </summary>

        public partial class GridElementPlan
        {        

            /*
             * Get the size and iteration count for this item in this containing cell. If we have width specification we figure out
             * how much we have horizontally. If we have height specification we figure out vertical
             */
            public Tuple<double, int> GetSizeAndIterations(OffsetAndSize ContainingCell)
            {
                // There are formats
                // 1. size,repeat
                // 2. size
                // The size can be a number or a percentag

                string format = "height=100%";
                string repeat = "1";
                string[] parts = CellFormat.Split(",".ToCharArray());

                switch (parts.Length)
                {
                    case 1:
                        format = parts[0];
                        break;
                    case 2:
                        format = parts[0];
                        repeat = parts[1];
                        break;
                    default:
                        // error
                        break;
                }

                // break size up
                string[] formatparts = format.Split("=".ToCharArray());
                string direction = "height";
                string amount = "100%";

                switch (formatparts.Length)
                {
                    case 2:
                        direction = formatparts[0];
                        amount = formatparts[1];
                        break;
                }

                bool bIsPercent = false;
                double dAmount = 100;
                // is this a percentage?
                if (amount.Contains("%"))
                {
                    bIsPercent = true;
                    amount = amount.Replace("%", "");
                }

                double.TryParse(amount, out dAmount);

                // Assume its a row unless they say width.
                bool bIsRow = true;
                if (direction == "width") bIsRow = false;

                double MaxAllocation = bIsRow ? ContainingCell.Height : ContainingCell.Width;

                double ThisCellSize = bIsPercent ? ThisCellSize = dAmount / 100 * MaxAllocation : dAmount;

                // Get the repeat count for this cell
                int iterations = 0;
                if (repeat == "*")
                {
                    // set iterations to the max possible, the max allocation / fixed size
                    iterations = (int)(MaxAllocation / ThisCellSize);
                    /*
                     * Adjust the width so that it fits evenly within the remaining space. In the 
                     * case of an '*' construct the width is interpreted as a minimum
                     */
                    ThisCellSize = MaxAllocation / (double)iterations;
                }
                else
                {
                    iterations = 1;
                    Int32.TryParse(repeat, out iterations);

                    // limit the repeat count so it will fit in the max allocation, but do not change the cell size
                    if (iterations * ThisCellSize > MaxAllocation)
                    {
                        iterations = (int)(MaxAllocation / ThisCellSize);
                    }
                }

                Tuple<double, int> SizeAndIterations = new Tuple<double, int>(ThisCellSize, iterations);

                return SizeAndIterations;

            }
        }
    }
}
