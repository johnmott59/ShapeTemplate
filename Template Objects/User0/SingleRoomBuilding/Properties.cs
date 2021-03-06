﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;
using ShapeTemplateLib;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SingleRoomBuilding 
    {
        /// <summary>
        /// The HorizontalScale describes how the layout is scaled in the plane of the layout objects; how much to stretch the 
        /// layout points before rendering them as panels. Its used to allow the definition of objects around a consistent 
        /// unit size but then allow their modification before export for the target environment
        /// </summary>
        [HelpProperty(SampleValue = "1.0", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public float HorizontalScale { get; set; } = 1;

        /// <summary>
        /// The VerticalScale describes how the layout is scaled in in the vertical direction; how much to stretch the height. 
        /// Its used to allow the definition of objects around a consistent 
        /// unit size but then allow their modification before export for the target environment
        /// </summary>
        [HelpProperty(SampleValue = "1.0", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public float VerticalScale { get; set; } = 1;

        /// <summary>
        /// The building has a length, width and a height. Length and width are what we normally think of 
        /// as the horizontal dimensions of a building
        /// </summary>
        [HelpProperty(SampleValue = "30.0", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public float Width { get; set; } = 1;

        /// <summary>
        /// Wall thickness
        /// </summary>
        [HelpProperty(SampleValue = "5.0", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public float Thickness { get; set; } = 1;

        /// <summary>
        /// The building has a length, width and a height. Length and width are what we normally think of 
        /// as the horizontal dimensions of a building. 
        /// </summary>
        [HelpProperty(SampleValue = "40.0", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public float Length { get; set; } = 1;

        /// <summary>
        /// The height of a building is how tall it is
        /// </summary>
        [HelpProperty(SampleValue = "20.0", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public float Height { get; set; } = 1;

        /// <summary>
        /// The roof is as thick as the walls, but it can be offset to be indented
        /// </summary>
        [HelpProperty(SampleValue = "-5.0", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.TemplateProperty)]
        public float RoofOffset { get; set; } = 0;

        /// <summary>
        /// Door properties
        /// </summary>
        public Hole Door { get; set; }

        /// <summary>
        /// Front window properties
        /// </summary>
        public Hole FrontWindow { get; set; }

        /// <summary>
        /// Left window properties
        /// </summary>
        public Hole LeftWindow { get; set; }

        /// <summary>
        /// Rear window properties
        /// </summary>
        public Hole RearWindow { get; set; }

        /// <summary>
        /// Right window properties
        /// </summary>
        public Hole RightWindow { get; set; }

    }
}
