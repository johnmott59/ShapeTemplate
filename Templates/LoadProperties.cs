
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
    public partial class GearBaseTemplate 
    {

       public override bool LoadProperties(XElement xTemplateNode, out string message)
       {
			float fTmp;
			int iTmp;

			message = "OK";
			XElement xNode;
       
				if (!GetPropertyFloat(xTemplateNode, nameof(HorizontalScale), out fTmp, out message)) return false;
				HorizontalScale = fTmp;
			
				if (!GetPropertyFloat(xTemplateNode, nameof(VerticalScale), out fTmp, out message)) return false;
				VerticalScale = fTmp;
			
				if (!GetPropertyFloat(xTemplateNode, nameof(Width), out fTmp, out message)) return false;
				Width = fTmp;
			
				if (!GetPropertyFloat(xTemplateNode, nameof(Length), out fTmp, out message)) return false;
				Length = fTmp;
			
				if (!GetPropertyFloat(xTemplateNode, nameof(Height), out fTmp, out message)) return false;
				Height = fTmp;
			
				if (!GetPropertyFloat(xTemplateNode, nameof(Thickness), out fTmp, out message)) return false;
				Thickness = fTmp;
			
				if (!GetPropertyFloat(xTemplateNode, nameof(RoofOffset), out fTmp, out message)) return false;
				RoofOffset = fTmp;
			
				xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "srbrecthole", "door",out message);
				if (xNode == null) return false;
				if (!Door.LoadProperties(xNode, out message)) return false;
			
				xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "srbrecthole", "frontwindow",out message);
				if (xNode == null) return false;
				if (!FrontWindow.LoadProperties(xNode, out message)) return false;
			
				xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "srbrecthole", "leftwindow",out message);
				if (xNode == null) return false;
				if (!LeftWindow.LoadProperties(xNode, out message)) return false;
			
				xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "srbrecthole", "rearwindow",out message);
				if (xNode == null) return false;
				if (!RearWindow.LoadProperties(xNode, out message)) return false;
			
				xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "srbrecthole", "rightwindow",out message);
				if (xNode == null) return false;
				if (!RightWindow.LoadProperties(xNode, out message)) return false;
				   }



	}
}
