using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FLRoom
    {
        public bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;

            if (!Utilities.GetIntProperty(xTemplateNode, "connectstoopenarea", out iTmp, out message)) return false;
            ConnectsToOpenArea = iTmp;

            if (!Utilities.GetIntProperty(xTemplateNode, "backroom", out iTmp, out message)) return false;
            BackRoom = iTmp;
            if (!LoadEdgeIndexList(xTemplateNode, out message)) return false;

            return true;
        }
        public bool LoadEdgeIndexList(XElement Xele, out string message)
        {
            message = "OK";

            XElement XList = Utilities.GetListElement(Xele, "edgeindexlist");

            // ok if there are no items
            if (XList == null) return true;

            List<int> list = new List<int>();
            foreach (XElement x in XList.Elements("int"))
            {
                // Get the attribute value
                XAttribute att = x.Attribute("value");
                if (att == null)
                {
                    message = "Missing 'value' attribute of int";
                    return false;
                }
                int o = 0;
                if (!Int32.TryParse(att.Value,out o))
                {
                    message = "Invalid integer value " + att.Value;
                    return false;
                }
                list.Add(o);
            }

            EdgeIndexList = list.ToArray();

            return true;
        }
    }
}
