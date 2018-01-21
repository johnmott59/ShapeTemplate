using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public partial class Point2DContainer 
    {
        /// <summary>
        /// List of points for a 2D outline.
        /// </summary>
        [HelpProperty("Point2DContainer")]

        // The reason that this is an array instead of a list is that its a data structure that may be exported to and imported
        // from JSON, and this data structure may be referenced both in JS and in C# so its best to keep it the same

        public Point2D[] Point2DList { get; set; } = new Point2D[0];


        // The CopyFrom method is used in Javascript to convert a version of the object retrieved via JSON
        // into a full object of this type. An object retrieved via JSON.parse() will have the correct property
        // names but not be an object of this type, this will take that property-only version and create a full
        // object
        public static Point2DContainer CopyFrom(Point2DContainer oFrom)
        {
            Point2DContainer oOutline = new Point2DContainer();
            oOutline.Point2DList = new Point2D[oFrom.Point2DList.Length];
            for (int i=0; i < oFrom.Point2DList.Length; i++)
            {
                Point2D p = oFrom.Point2DList[i];
                oOutline.Point2DList[i] = Point2D.CopyFrom(p);
            }

            return oOutline;
        }
    }


}