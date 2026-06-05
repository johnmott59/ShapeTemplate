
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

       public  bool LoadProperties(XElement xTemplateNode, out string message)
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
