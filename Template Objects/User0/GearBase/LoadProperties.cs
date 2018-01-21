
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
    public partial class GearBase 
    {

       public override bool LoadProperties(XElement xTemplateNode, out string message)
       {
			float fTmp;
			int iTmp;

			message = "OK";
			XElement xNode;
       
				if (!GetPropertyFloat(xTemplateNode, nameof(Scale), out fTmp, out message)) return false;
				Scale = fTmp;
			
				if (!GetPropertyFloat(xTemplateNode, nameof(Width), out fTmp, out message)) return false;
				Width = fTmp;
			
				if (!GetPropertyInt(xTemplateNode, nameof(ToothCount), out iTmp, out message)) return false;
				ToothCount = iTmp;
			
				xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "point2dcontainer", "toothshape",out message);
				if (xNode == null) return false;
				if (!ToothShape.LoadProperties(xNode, out message)) return false;
			
	   return true;
	   }

	}
}
