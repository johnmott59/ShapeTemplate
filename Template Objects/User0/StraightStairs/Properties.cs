using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public  partial class StraightStairs : TemplateRoot
    {
        /// <summary>
        /// The verticaldistance describes how they are placed vertically
        /// </summary>
        [HelpProperty(SampleValue ="10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int VerticalDistance { get; set; } = 10;

        /// <summary>
        /// The horizontaldistance describes how they are placed horizontally
        /// </summary>
        [HelpProperty( SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int HorizontalDistance { get; set; } = 10;

        /// <summary>
        /// The lateraldistance describes how the stairs drift sideways 
        /// </summary>
        [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int LateralDistance { get; set; } = 0;

        /// <summary>
        /// staircount is the count of stairs
        /// </summary>
        [HelpProperty(SampleValue ="10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int StairCount { get; set; } = 10;

        /// <summary>
        /// width is the width of stairs
        /// </summary>
        [HelpProperty(SampleValue = "30", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int Width { get; set; } = 30;

        /// <summary>
        /// rise is the how tall each stair is 
        /// </summary>
        [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int Rise { get; set; } = 10;

        /// <summary>
        /// run is the length of the stair 
        /// </summary>
        [HelpProperty(SampleValue = "10", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public int Run { get; set; } = 10;

        /// <summary>
        /// The left and right side of the stairs have a texture, and there is a texture for the steps
        /// leftsidetexture is the name of the texture file for the left side
        /// </summary>
        [HelpProperty( SampleValue = "leftside.png", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public string LeftSideTexture { get; set; } = "";

        /// <summary>
        /// The left and right side of the stairs have a texture, and there is a texture for the steps
        /// rightsidetexture is the name of the texture file for the left side
        /// 1. its an inline attrobute of its parent
        ///  2. its an attribute of node with named 'property'
        /// </summary>
        [HelpProperty( SampleValue = "leftside.png", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public string RightSideTexture { get; set; } = "";

        /// <summary>
        /// The left and right side of the stairs have a texture, and there is a texture for the steps
        /// stairtexture is the name of the texture file for the left side
        /// </summary>
        [HelpProperty(SampleValue = "stairtexture", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public string StairTexture { get; set; } = "";


    }

}
