using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public class BoundaryLineSegment : BoundaryRoot
    {
        public List<Point3D> PointList { get; set; } = new List<Point3D>();

        public override XElement GetProperties()
        {
            XElement bound = new XElement("boundary");

            XElement plist = new XElement("line");

            bound.Add(plist);

            foreach (Point3D p in PointList)
            {
                plist.Add(p.GetProperties());
            }

            return bound;
        }

        public override bool LoadProperties(XElement line, out string message)
        {
            message = "OK";

            // Get a list of points
            List<XElement> xList = line.Elements("point").ToList();
          
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