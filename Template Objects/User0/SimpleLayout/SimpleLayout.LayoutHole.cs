using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class LayoutHole : ILoadAndSaveProperties
    {
        public XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("layouthole", new XAttribute("prop", PropertyName));

            root.Add(new XElement("property", new XAttribute(nameof(OffsetX).ToLower(), OffsetX)));

            root.Add(new XElement("property", new XAttribute(nameof(OffsetY).ToLower(), OffsetY)));

            root.Add(new XElement("property", new XAttribute(nameof(HoleType).ToLower(), HoleType)));

            root.Add(new XElement("property", new XAttribute(nameof(HoleTypeIndex).ToLower(), HoleTypeIndex)));

            return root;
        }


        public bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;

            if (!Utilities.GetFloatProperty(xTemplateNode, nameof(OffsetX).ToLower(), out fTmp, out message)) return false;
            OffsetX = fTmp;

            if (!Utilities.GetFloatProperty(xTemplateNode, nameof(OffsetY).ToLower(), out fTmp, out message)) return false;
            OffsetY = fTmp;

            if (!Utilities.GetStringProperty(xTemplateNode, nameof(HoleType).ToLower(), out sTmp, out message)) return false;
            HoleType = sTmp;

            if (!Utilities.GetIntProperty(xTemplateNode, nameof(HoleTypeIndex).ToLower(), out iTmp, out message)) return false;
            HoleTypeIndex = iTmp;

            return true;
        }

      

    }

}
