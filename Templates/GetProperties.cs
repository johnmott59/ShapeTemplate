
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

	public override XElement GetProperties(string PropertyName = "")
    {
          XElement root = new XElement("template",
                new XAttribute("prop", PropertyName),
                new XAttribute("user", "User0"),
                new XAttribute("name", "GearBaseTemplate".ToLower()));

 
			root.Add(new XElement("property", new XAttribute(nameof(HorizontalScale).ToLower(), HorizontalScale)));
	 
			root.Add(new XElement("property", new XAttribute(nameof(VerticalScale).ToLower(), VerticalScale)));
	 
			root.Add(new XElement("property", new XAttribute(nameof(Width).ToLower(), Width)));
	 
			root.Add(new XElement("property", new XAttribute(nameof(Length).ToLower(), Length)));
	 
			root.Add(new XElement("property", new XAttribute(nameof(Height).ToLower(), Height)));
	 
			root.Add(new XElement("property", new XAttribute(nameof(Thickness).ToLower(), Thickness)));
	 
			root.Add(new XElement("property", new XAttribute(nameof(RoofOffset).ToLower(), RoofOffset)));
	
			root.Add(Door.GetProperties("door"));
	
			root.Add(FrontWindow.GetProperties("frontwindow"));
	
			root.Add(LeftWindow.GetProperties("leftwindow"));
	
			root.Add(RearWindow.GetProperties("rearwindow"));
	
			root.Add(RightWindow.GetProperties("rightwindow"));
	                
            return root;
	}

	}
}
