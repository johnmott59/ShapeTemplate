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
        public List<GridElementPlan> GridElementPlanList { get; set; } = new List<GridElementPlan>();
        public float Width { get; set; }
        public float Height { get; set; }
        
    }
}
