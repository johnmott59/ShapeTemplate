using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public partial class HoleContainer : ILoadAndSaveProperties
    {
        public XElement GetProperties(string PropertyName = "")
        {
            XElement ele = new XElement("holecontainer", new XAttribute("prop", PropertyName));
            // Add a list of points
            foreach (Hole p in HoleList)
            {
                ele.Add(p.GetProperties(""));
            }

            return ele;
        }

        public bool LoadProperties(XElement xContainer, out string Message)
        {
            Message = "OK";
            /*
             * Add the children to a list of holes
             */
            List<Hole> list = new List<Hole>();
  
            foreach(XElement ele in xContainer.Elements("hole"))
            {
                Hole p = new Hole();
                if (!p.LoadProperties(ele, out Message)) return false;
                list.Add(p);
            }

            // Convert to an array
            HoleList = list.ToArray();
            
            return true;
        }
    }


}