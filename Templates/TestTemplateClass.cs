
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
    public partial class TestTemplateClass : TemplateBaseClass
    {

	
		// Constructor, initialize any object or array properties
		public TestTemplateClass() {
							LSListList = new LineSegmentList[0];
				
	}
	

        /// <summary>
        ///  LSListList  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
     		public LineSegmentList[] LSListList { get; set; } 
	 
        /// <summary>
        ///  ID  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
     		public string ID { get; set; } 
	 
	}
}
