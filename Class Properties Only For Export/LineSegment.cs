using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    /// <summary>
    /// The simple line segment is used to hold a from and a to point in 2D
    /// </summary>
    public partial class LineSegment 
    {
        public Point2D From { get; set; }
        public Point2D To { get; set; }

        // The CopyFrom method is used in Javascript to convert a version of the object retrieved via JSON
        // into a full object of this type. An object retrieved via JSON.parse() will have the correct property
        // names but not be an object of this type, this will take that property-only version and create a full
        // object
        public static LineSegment CopyFrom(LineSegment oSource)
        {
            LineSegment v = new LineSegment();

            v.From = oSource.From;
            v.To= oSource.To;

            return v;
        }
    }
}
