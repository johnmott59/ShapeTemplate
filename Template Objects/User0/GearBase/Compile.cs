
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

		public override XElement Compile () {
			// Return a combination of templates and/or basic shapes
			return new XElement("root");
		}  

	
	}
}
