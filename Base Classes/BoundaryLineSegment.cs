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
    [HelpItem(eItemFlavor.Data, "linesegment")]
    public class BoundaryLineSegment : BoundaryRoot
    {
        /// <summary>
        /// List of points for the boundarylinesegment
        /// </summary>
        [HelpProperty("pointlist")]
        public List<Point3D> PointList { get; set; } = new List<Point3D>();

        public override XElement GetProperties(string PropertyName = "")
        {
            XElement bound = new XElement("boundary", new XAttribute("prop", PropertyName));

            XElement plist = new XElement("linesegment");

            bound.Add(plist);

            foreach (Point3D p in PointList)
            {
                plist.Add(p.GetProperties());
            }

            return bound;
        }
        /// <summary>
        /// This load routine is called by boundary root, the boundary element has already been processed
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public override bool LoadProperties(XElement line, out string message)
        {
            message = "OK";

            // Get a list of points
            List<XElement> xList = line.Elements("point3d").ToList();
          
            if (xList.Count == 0)
            {
                message = "Missing points in line";
                return false;
            }
  
            /*
             * Load the listof points
             */
            foreach (XElement xp in xList)
            {
                Point3D p = new Point3D();
                if (!p.LoadProperties(xp, out message)) return false;
                this.PointList.Add(p);
            }
            return true;
        }


    }


}