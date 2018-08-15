using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public partial class Point2DContainer : ILoadAndSaveProperties
    {
        public XElement GetProperties(string PropertyName = "")
        {
            XElement ele = new XElement("point2dcontainer", new XAttribute("prop", PropertyName));
            // Add a list of points
            foreach (Point2D p in Point2DList)
            {
                ele.Add(p.GetProperties(""));
            }

            return ele;
        }

        public bool LoadProperties(XElement xContainer, out string Message)
        {
            Message = "OK";
            /*
             * Add the children to a list of points
             */
            List<Point2D> list = new List<Point2D>();
  
            foreach(XElement ele in xContainer.Elements("point2d"))
            {
                Point2D p = new Point2D();
                if (!p.LoadProperties(ele, out Message)) return false;
                list.Add(p);
            }

            // Convert to an array
            Point2DList = list.ToArray();
            
            return true;
        }
    }


}