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
            public HolePlacementTemplate() : this(0, 0) { }

            public HolePlacementTemplate(float Width, float Height)
            {
                this.Width = Width;
                this.Height = Height;
            }

    }
}
