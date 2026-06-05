using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{

    public partial class BoundaryPolygon : BoundaryRoot
    {
        public override XElement GetProperties(string PropertyName = "")
        {
            XElement ele = new XElement("boundarypolygon", new XAttribute("prop", PropertyName));

            foreach (Point3D p in PointList)
            {
                ele.Add(p.GetProperties());
            }

            return ele;
        }
        /// <summary>
        /// This routine is called pointing to the boundarypolygon node
        /// </summary>
        /// <param name="line"></param>
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
            this.PointList = new Point3D[xList.Count];
            int index = 0;
            foreach (XElement xp in xList)
            {
                Point3D p = new Point3D();
                if (!p.LoadProperties(xp, out message)) return false;
                this.PointList[index++] = p;
            }
            return true;
        }

        // Get a list of 2D points for this rectangle. The class is defined in 3D but we use it in 2D situations
        public override List<PointF> GetPoints2D(float OffsetX, float OffsetY)
        {
            List<PointF> PFList = new List<PointF>();

            foreach (ShapeTemplateLib.Point3D p in this.PointList)
            {
                PFList.Add(new PointF()
                    {
                        X = OffsetX + p.X,
                        Y = OffsetY + p.Y
                    }
                    );
            }

            return PFList;

        }


    }


}