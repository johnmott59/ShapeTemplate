using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    /// <summary>
    /// This class is like system.drawing.rectanglef, used to hold an offset and a size. Its used with the 
    /// holeplacement template. 
    /// </summary>
    public class OffsetAndSize
    {
        public Point2D Offset { get; set; } 
        public float Width { get; set; }
        public float Height { get; set; }

        public bool IsContainedInCell { get; set; }

        public OffsetAndSize ()
        {
            Offset = new Point2D();
            Offset.X = 0;
            Offset.Y = 0;
        }
    }
}
