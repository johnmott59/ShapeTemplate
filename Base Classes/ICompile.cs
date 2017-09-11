using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    /*
     * The compile interface will create basic shapes that are appropriate for creating a mesh
     */
    public interface ICompile
    {
        XElement Compile();
    }
}
