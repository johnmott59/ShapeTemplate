using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    /// <summary>
    /// This class contains the editor data thats contained within a tab in a multi-tab interface. There is always at least one tab, although if there 
    /// is only one tab there is no multi tab display
    /// </summary>
    public class EditorTab
    {
        public List<EditorRow> EditorRowList { get; set; } = new List<EditorRow>();
    }
}
