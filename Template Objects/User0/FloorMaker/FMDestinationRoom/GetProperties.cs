using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FMDestinationRoom
    {
        public XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement(nameof(FMAssembledRoom).ToLower(), new XAttribute("prop", PropertyName));

            XElement XEdgeIndexList = new XElement("list", new XAttribute("name", nameof(EdgeIndexList).ToLower()));
            root.Add(XEdgeIndexList);
            XEdgeIndexList.Add(GetEdgeIndexList().Elements());

            return root;
        }

        //--------------------------------
        protected XElement GetEdgeIndexList()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("edgeindexlist");

            foreach (int r in EdgeIndexList)
            {
                XList.Add(new XElement("int", new XAttribute("value", r)));
            }

            return XList;
        }


    }
}
