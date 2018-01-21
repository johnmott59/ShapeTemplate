
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
    public partial class GearBaseTemplate : TemplateRoot
    {


        /// <summary>
        ///  HorizontalScale  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
		public float HorizontalScale { get; set; } 

	
        /// <summary>
        ///  VerticalScale  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
		public float VerticalScale { get; set; } 

	
        /// <summary>
        ///  Width  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
		public float Width { get; set; } 

	
        /// <summary>
        ///  Length  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
		public float Length { get; set; } 

	
        /// <summary>
        ///  Height  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
		public float Height { get; set; } 

	
        /// <summary>
        ///  Thickness  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
		public float Thickness { get; set; } 

	
        /// <summary>
        ///  RoofOffset  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
		public float RoofOffset { get; set; } 

	
        /// <summary>
        ///  Door  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.XElement)]
		public SRBRectHole Door { get; set; } 

	
        /// <summary>
        ///  FrontWindow  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.XElement)]
		public SRBRectHole FrontWindow { get; set; } 

	
        /// <summary>
        ///  LeftWindow  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.XElement)]
		public SRBRectHole LeftWindow { get; set; } 

	
        /// <summary>
        ///  RearWindow  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.XElement)]
		public SRBRectHole RearWindow { get; set; } 

	
        /// <summary>
        ///  RightWindow  
        /// </summary>
	    [HelpProperty(SampleValue = "", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.XElement)]
		public SRBRectHole RightWindow { get; set; } 

	
	}
}
