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

            public bool LoadProperties(XElement xTemplateNode, out string message)
            {
                float fTmp;
                int iTmp;
                string sTmp;

                message = "OK";
                XElement xNode;

                if (!Utilities.GetStringProperty(xTemplateNode, nameof(CellFormat), out sTmp, out message)) return false;
                CellFormat = sTmp;
                if (!LoadChildElementPlanList(xTemplateNode, out message)) return false;
                if (!LoadBoxPlanList(xTemplateNode, out message)) return false;

                return true;
            }
            public bool LoadChildElementPlanList(XElement Xele, out string message)
            {
                message = "OK";

                XElement XList = Utilities.GetListElement(Xele, "childelementplanlist");

                // ok if there are no items
                if (XList == null) return true;

                ChildElementPlanList  = new List<GridElementPlan>();
                foreach (XElement x in XList.Elements("gridelementplan"))
                {
                    GridElementPlan o = new GridElementPlan();
                    if (!o.LoadProperties(x, out message)) return false;
                    ChildElementPlanList.Add(o);
                }


                return true;
            }

            public bool LoadBoxPlanList(XElement Xele, out string message)
            {
                message = "OK";

                XElement XList = Utilities.GetListElement(Xele, "boxplanlist");

                // ok if there are no items
                if (XList == null) return true;

                BoxPlanList = new List<string>();
                foreach (XElement x in XList.Elements("string"))
                {
                    BoxPlanList.Add(x.Attribute("value").Value);
                }
               
                return true;
            }


        }
    }
}
