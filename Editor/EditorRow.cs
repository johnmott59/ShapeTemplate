using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    /// <summary>
    /// This class is used by the template to describe a row for the editor. It consists of columns
    /// </summary>
    public class EditorRow
    {
        public List<EditorColumn> EditorColumnList { get; set; } = new List<EditorColumn>();
    }
}
