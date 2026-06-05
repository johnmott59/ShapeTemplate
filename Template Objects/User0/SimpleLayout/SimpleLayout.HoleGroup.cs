using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{  
    public partial class HoleGroup
    {
        public HoleGroup CopyOf()
        {
            string message = "";

            HoleGroup hg = new HoleGroup();
            hg.LoadProperties(this.GetProperties(), out message);

            return hg;
        }


        public bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;

            if (!Utilities.GetStringProperty(xTemplateNode, nameof(HoleGroupID), out sTmp, out message)) return false;
            HoleGroupID = sTmp;
            if (!LoadHoleList(xTemplateNode, out message)) return false;

            return true;
        }

        public bool LoadHoleList(XElement Xele, out string message)
        {
            message = "OK";

            XElement XList = Utilities.GetListElement(Xele, "holelist");

            // ok if there are no items
            if (XList == null) return true;

            List<LayoutHole> list = new List<LayoutHole>();
            foreach (XElement x in XList.Elements("layouthole"))
            {
                LayoutHole o = new LayoutHole();
                if (!o.LoadProperties(x, out message)) return false;
                list.Add(o);
            }

            HoleList = list.ToArray();

            return true;
        }

        public  XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("holegroup", new XAttribute("prop", PropertyName));

            root.Add(new XElement("property", new XAttribute(nameof(HoleGroupID).ToLower(), HoleGroupID)));

            XElement XHoleList = new XElement("list", new XAttribute("name", nameof(HoleList).ToLower()));
            root.Add(XHoleList);
            XHoleList.Add(GetHoleList().Elements());

            return root;
        }

        //--------------------------------
        protected XElement GetHoleList()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("holelist");

            foreach (LayoutHole r in HoleList)
            {
                XList.Add(r.GetProperties());
            }

            return XList;
        }
    }
}
