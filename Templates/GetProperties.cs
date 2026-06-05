
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
    public partial class TestTemplateClass 
    {

	public  XElement GetProperties(string PropertyName = "")
    {
		          XElement root = new XElement("testtemplateclass",new XAttribute("prop", PropertyName));
		
				XElement XLSListList = new XElement("list", new XAttribute("name", nameof(LSListList).ToLower()));
				root.Add(XLSListList);
				XLSListList.Add(GetLSListList().Elements());
			 
				root.Add(new XElement("property", new XAttribute(nameof(ID).ToLower(), ID)));
			                
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


	}
}
