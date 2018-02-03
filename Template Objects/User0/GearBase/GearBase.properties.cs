
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

        /// <summary>
        ///  Scale  
        /// </summary>
	    [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
		public float Scale { get; set; } 

	
        /// <summary>
        ///  Width  
        /// </summary>
	    [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
		public float Width { get; set; } 

	
        /// <summary>
        ///  ToothCount  
        /// </summary>
	    [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
		public int ToothCount { get; set; } 

	
        /// <summary>
        ///  ToothShape  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.XElement)]
		public Point2DContainer ToothShape { get; set; }

        /// <summary>
        ///  GearHoleGroup  
        /// </summary>
        [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.XElement)]
        public HoleContainer GearHoles { get; set; } 

    }
}
