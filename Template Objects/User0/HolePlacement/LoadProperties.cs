
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;
using System.Web.Script.Serialization;

namespace ShapeTemplateLib.Templates.User0 
{
    public partial class HolePlacementTemplate 
    {


        public override bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;

            if (!Utilities.GetIntProperty(xTemplateNode, nameof(Width), out iTmp, out message)) return false;
            Width = iTmp;

            if (!Utilities.GetIntProperty(xTemplateNode, nameof(Height), out iTmp, out message)) return false;
            Height = iTmp;
            if (!LoadGridElementPlanList(xTemplateNode, out message)) return false;

            return true;
        }
        public bool LoadGridElementPlanList(XElement Xele, out string message)
        {
            message = "OK";

            XElement XList = Utilities.GetListElement(Xele, "gridelementplanlist");

            // ok if there are no items
            if (XList == null) return true;

            GridElementPlanList = new List<GridElementPlan>();
            foreach (XElement x in XList.Elements("gridelementplan"))
            {
                GridElementPlan o = new GridElementPlan();
                if (!o.LoadProperties(x, out message)) return false;
                GridElementPlanList.Add(o);
            }

            return true;
        }


    }
}
