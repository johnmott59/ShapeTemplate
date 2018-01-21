using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;


namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// The simplelayout is a layout of walls, holes and floor/ceiling panels. Edges and vertices form the wall panels. Panels can have
    /// holes, which are defined via a HoleGroup. A holegroup consists of some number of BoundaryRectangle, BoundaryPolygon or BoundaryEllipse
    /// outlines. The wall panels are created vertically and can intersect. Horizontal panels, used for floors and roofs, are defined in a HorizontalPanel
    /// object, and these can also have holes, defined in a HoleGroup. The scale of the rendered mesh can be controlled by the HorizontalScale and
    /// VerticalScale properties so that the resulting mesh can be generated in a size appropriate for the platform using the mesh.
    /// </summary>
    [HelpItem(eItemFlavor.Template, "simplelayout")]
    public partial class SimpleLayout : TemplateRoot
    {

        public override XElement Compile()
        {
            // Compilation for this template is the same as generating the properties

            return GetProperties();
        }






    }

}
