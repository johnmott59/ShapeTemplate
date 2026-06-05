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
        /// <summary>
        /// Add a cell
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
            public GridElementPlan AddCell(string format)
            {
                GridElementPlan gc = new GridElementPlan(format);
                GridElementPlanList.Add(gc);
                return gc;
            }

    }
}
