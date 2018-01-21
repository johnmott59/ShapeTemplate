
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
    public partial class xxGearBase 
    {

	public override XElement GetProperties(string PropertyName = "")
    {
          XElement root = new XElement("template",
                new XAttribute("prop", PropertyName),
                new XAttribute("user", "User0"),
                new XAttribute("name", "xxGearBase".ToLower()));

 
			root.Add(new XElement("property", new XAttribute(nameof(Scale).ToLower(), Scale)));
	 
			root.Add(new XElement("property", new XAttribute(nameof(ToothCount).ToLower(), ToothCount)));
	
			root.Add(ToothShape.GetProperties("toothshape"));
	                
            return root;
	}

	}
}
