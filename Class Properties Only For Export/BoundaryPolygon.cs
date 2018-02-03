using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    /// <summary>
    /// The BoundaryLineSegment class is used to define an list of line segments. 
    /// This width and height are X and Y values;
    /// boundaries are defined in an XY plane that the viewer is facing, with X to the right, Y up and Z coming out. 
    /// These segments should form a closed 
    /// non-intersecting polygon and should be wound counter clockwise. A counter-clockwise triangle would be 
    /// (0,0) -> (50,0) -> (25,25)
    /// </summary>
    [HelpItem(eItemFlavor.Data, "boundarypolygon")]
    public partial class BoundaryPolygon 
    {
        /// <summary>
        /// List of points for the boundarylinesegment
        /// </summary>
        [HelpProperty("pointlist")]

        // The reason that this is an array instead of a list is that its a data structure that may be exported to and imported
        // from JSON, and this data structure may be referenced both in JS and in C# so its best to keep it the same

        public Point3D[] PointList { get; set; } = new Point3D[0];

        public BoundaryPolygon()
        {
            this.BoundaryType = "polygon";
        }

        

        // The CopyFrom method is used in Javascript to convert a version of the object retrieved via JSON
        // into a full object of this type. An object retrieved via JSON.parse() will have the correct property
        // names but not be an object of this type, this will take that property-only version and create a full
        // object
        public static BoundaryPolygon CopyFrom(BoundaryPolygon oFrom)
        {
            BoundaryPolygon oPolygon = new BoundaryPolygon();
            oPolygon.PointList = new Point3D[oFrom.PointList.Length];
            for (int i=0; i < oFrom.PointList.Length; i++)
            {
                Point3D p = oFrom.PointList[i];
                oPolygon.PointList[i] = Point3D.CopyFrom(p);
            }

            return oPolygon;
        }
    }


}