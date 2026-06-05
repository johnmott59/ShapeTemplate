using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// These routines will oversee grid construction and the return of element values. They are part of the
    /// holeplacementtemplate class but are all related, so they are in this directory
    /// </summary>
    public partial class HolePlacementTemplate
    {
            private void Recurse(List<GridElementPlan> CellList, List<GridElement> LeafList, OffsetAndSize ContainerCell)
            {
                foreach (GridElementPlan gc in CellList)
                {
                    // Get the size of this cell and the count of iterations
                    Tuple<double, int> SizeAndIterations = gc.GetSizeAndIterations(ContainerCell);

                    double ThisCellSize = SizeAndIterations.Item1;
                    int iterations = SizeAndIterations.Item2;

                    double CurrentOffset = 0;

                    for (int i = 0; i < iterations; i++)
                    {
                        OffsetAndSize CellOffsetAndSize = gc.GetCellOffsetAndSize(CurrentOffset, ThisCellSize, ContainerCell); 
  
                        // If this is a leaf node add it to the list. It may or may not have boxes
                        if (gc.ChildElementPlanList.Count() == 0)
                        {
                            GridElement ge = new GridElement()
                            {
                                CellOffsetAndSize = CellOffsetAndSize,
                            };

                            LeafList.Add(ge);

                            // Now that we have the containing cell offset and width compute box offsets and width

                            List<string> MessageList = new List<string>();
                            foreach (string s in gc.BoxPlanList)
                            {
                                ge.BoxOffsetAndSizeList.Add(ge.GetBoxOffsetAndSize(s, MessageList));
                            }
                      
                        }
                        // advance the current allocation
                        CurrentOffset += ThisCellSize;

                        // Now recurse to rows inside
                        Recurse(gc.ChildElementPlanList, LeafList, CellOffsetAndSize);
                    }
                }
            }

    }
}
