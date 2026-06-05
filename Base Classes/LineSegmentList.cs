using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
        public partial class LineSegmentList : ILoadAndSaveProperties
        {

            // Constructor, initialize any object or array properties
            public LineSegmentList()
            {
                LSList = new LineSegment[0];
                ID = "";

        }

            /// <summary>
            ///  LSList  
            /// </summary>
            [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
            public LineSegment[] LSList { get; set; }

            /// <summary>
            ///  ID  
            /// </summary>
            [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
            public string ID { get; set; }

        public  XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("linesegmentlist", new XAttribute("prop", PropertyName));

            XElement XLSList = new XElement("list", new XAttribute("name", nameof(LSList).ToLower()));
            root.Add(XLSList);
            XLSList.Add(GetLSList().Elements());

            root.Add(new XElement("property", new XAttribute(nameof(ID).ToLower(), ID)));

            return root;
        }

        //--------------------------------
        protected XElement GetLSList()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("lslist");

            foreach (LineSegment r in LSList)
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
            if (!LoadLSList(xTemplateNode, out message)) return false;

            if (!Utilities.GetStringProperty(xTemplateNode, nameof(ID), out sTmp, out message)) return false;
            ID = sTmp;

            return true;
        }

        public bool LoadLSList(XElement Xele, out string message)
        {
            message = "OK";

            XElement XList = Utilities.GetListElement(Xele, "lslist");

            // ok if there are no items
            if (XList == null) return true;

            List<LineSegment> list = new List<LineSegment>();
            foreach (XElement x in XList.Elements("linesegment"))
            {
                LineSegment o = new LineSegment();
                if (!o.LoadProperties(x, out message)) return false;
                list.Add(o);
            }

            LSList = list.ToArray();

            return true;
        }
    }
    }
