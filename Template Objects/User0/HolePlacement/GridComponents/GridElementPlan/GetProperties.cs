using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class HolePlacementTemplate
    {
        public partial class GridElementPlan
        {

            public XElement GetProperties(string PropertyName = "")
            {
                XElement root = new XElement("gridelementplan", new XAttribute("prop", PropertyName));

                root.Add(new XElement("property", new XAttribute(nameof(CellFormat).ToLower(), CellFormat)));

                XElement XChildElementPlanList = new XElement("list", new XAttribute("name", nameof(ChildElementPlanList).ToLower()));
                root.Add(XChildElementPlanList);
                XChildElementPlanList.Add(GetChildElementPlanList().Elements());

                XElement XBoxPlanList = new XElement("list", new XAttribute("name", nameof(BoxPlanList).ToLower()));
                root.Add(XBoxPlanList);
                XBoxPlanList.Add(GetBoxPlanList().Elements());

                return root;
            }

            //--------------------------------
            protected XElement GetChildElementPlanList()
            {
                // Add the vertices and the edges
                XElement XList = new XElement("childelementplanlist");

                foreach (GridElementPlan r in ChildElementPlanList)
                {
                    XList.Add(r.GetProperties());
                }

                return XList;
            }


            //--------------------------------
            protected XElement GetBoxPlanList()
            {
                // Add the vertices and the edges
                XElement XList = new XElement("boxplanlist");

                foreach (string r in BoxPlanList)
                {
                    XList.Add(new XElement("string", new XAttribute("value", r))); 
                }

                return XList;
            }


        }
    }
}
