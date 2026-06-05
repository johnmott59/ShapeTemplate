using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public partial class LineSegmentListList
    {

        // Constructor, initialize any object or array properties
        public LineSegmentListList()
        {
            LSListList = new LineSegmentList[0];
            ID = "";
        }


        /// <summary>
        ///  LSListList  
        /// </summary>
        [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public LineSegmentList[] LSListList { get; set; }

        /// <summary>
        ///  ID  
        /// </summary>
        [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public string ID { get; set; }

        public XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("linesegmentlistlist", new XAttribute("prop", PropertyName));

            XElement XLSListList = new XElement("list", new XAttribute("name", nameof(LSListList).ToLower()));
            root.Add(XLSListList);
            XLSListList.Add(GetLSListList().Elements());

            root.Add(new XElement("property", new XAttribute(nameof(ID).ToLower(), ID  == null ? "" : ID)));

            return root;
        }

        //--------------------------------
        protected XElement GetLSListList()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("lslistlist");

            foreach (LineSegmentList r in LSListList)
            {
                XList.Add(r.GetProperties());
            }

            return XList;
        }


        public bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;
            if (!LoadLSListList(xTemplateNode, out message)) return false;

            if (!Utilities.GetStringProperty(xTemplateNode, nameof(ID), out sTmp, out message)) return false;
            ID = sTmp;

            return true;
        }
        public bool LoadLSListList(XElement Xele, out string message)
        {
            message = "OK";

            XElement XList = Utilities.GetListElement(Xele, "lslistlist");

            // ok if there are no items
            if (XList == null) return true;

            List<LineSegmentList> list = new List<LineSegmentList>();
            foreach (XElement x in XList.Elements("linesegmentlist"))
            {
                LineSegmentList o = new LineSegmentList();
                if (!o.LoadProperties(x, out message)) return false;
                list.Add(o);
            }

            LSListList = list.ToArray();

            return true;
        }



    }
}
