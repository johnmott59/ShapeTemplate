using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SimpleLayout 
    {
        //---------------------------------
        protected XElement GetHorizontalPanelList()
        {
            XElement XHorizontalPanelList = new XElement("horizontalpanellist");
            foreach (HorizontalPanel hp in HorizontalPanelList)
            {
                XElement XHorizontalPanel = new XElement("horizontalpanel",
                    new XAttribute("thickness", hp.Thickness),
                    new XAttribute("height", hp.Height),
                    new XAttribute("holegroupid",hp.HoleGroupID)
                    );

                // add the descriptors
                foreach (string descriptor in hp.DescriptorList)
                {
                    XHorizontalPanel.Add(new XElement("descriptor",
                        new XAttribute("value", descriptor))
                        );
                }

                XHorizontalPanelList.Add(XHorizontalPanel);
                
            }

            return XHorizontalPanelList;
        }

        protected bool LoadHorizontalPanelList(XElement ele, out string message)
        {
            message = "OK";

            XElement XHorizontalPanelList = Utilities.GetListElement(ele, "horizontalpanellist");

            // an empty layout would have no segments.
            if (XHorizontalPanelList == null) return true;

            // There should be one child, a HorizontalPanelList, that's the container
            XHorizontalPanelList = XHorizontalPanelList.Element("horizontalpanellist");
            if (XHorizontalPanelList == null) return true;


            foreach (XElement XHorizontalPanel in XHorizontalPanelList.Elements("horizontalpanel"))
            {
                float Height, Thickness;
                string hgid;

                if (!Utilities.GetFloatAttribute(XHorizontalPanel, "height", out Height, out message)) return false;
                if (!Utilities.GetFloatAttribute(XHorizontalPanel, "thickness", out Thickness, out message)) return false;
                hgid = Utilities.GetOptionalStringAttribute(XHorizontalPanel, "holegroupid");
                /*
                 * Collect the point descriptors
                 */
                List<string> DescriptorList = new List<string>();
                foreach (XElement XDescriptor in XHorizontalPanel.Elements("descriptor"))
                {
                    string value = XDescriptor.Attribute("value").Value;
                    DescriptorList.Add(value);
                }

                HorizontalPanelList.Add(new HorizontalPanel()
                {
                    Thickness = Thickness,
                    Height = Height,
                    DescriptorList=DescriptorList.ToArray(),
                    HoleGroupID = hgid
                });

            }

            return true;
        }
    }
}

