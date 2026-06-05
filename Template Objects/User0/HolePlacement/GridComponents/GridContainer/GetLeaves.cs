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
            // create boxes that represent the leaves of the grid tree. The leaves may or may not have boxes for holes
            public List<GridElement> GetLeaves(float Width, float Height)
            {
                // now create a tree structure we can use to render

                OffsetAndSize CurrentCellOffsetAndSize = new OffsetAndSize();

                List<GridElement> LeafList = new List<GridElement>();

                // Get width and height of grid
                CurrentCellOffsetAndSize.Width = Width;
                CurrentCellOffsetAndSize.Height = Height;
                CurrentCellOffsetAndSize.Offset.X = 0;
                CurrentCellOffsetAndSize.Offset.Y = 0;

                Recurse(GridElementPlanList, LeafList, CurrentCellOffsetAndSize);

                return LeafList;
            }
            // create boxes that represent the leaves of the grid tree. The leaves may or may not have boxes for holes
            public List<GridElement> GetLeaves()
            {
                // now create a tree structure we can use to render

                OffsetAndSize CurrentCellOffsetAndSize = new OffsetAndSize();

                List<GridElement> LeafList = new List<GridElement>();

                // Get width and height of grid
                CurrentCellOffsetAndSize.Width = Width;
                CurrentCellOffsetAndSize.Height = Height;
                CurrentCellOffsetAndSize.Offset.X = 0;
                CurrentCellOffsetAndSize.Offset.Y = 0;

                Recurse(GridElementPlanList, LeafList, CurrentCellOffsetAndSize);

                return LeafList;
            }

    }
}
