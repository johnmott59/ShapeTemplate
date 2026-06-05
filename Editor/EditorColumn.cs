using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    /// <summary>
    /// This class contains a column of data and is inside a row
    /// </summary>
    public class EditorColumn
    {
        public enum eColumnType
        {
            Empty,
            Label,
            Control
        }

        public eColumnType ColumnType { get; set; } = eColumnType.Empty;

        public int Width { get; set; }      // Value between 1 and 12

        public string Label { get; set; }   // if this is a label column, the label value

        public string PropertyName { get; set; }    // If this is a control column this is property name

        public string ControlName { get; set; }     // if this is a control column this is the control name

    }
}
