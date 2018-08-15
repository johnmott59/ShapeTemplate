using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;
using ShapeTemplateLib;

namespace ShapeTemplateLib.Templates.User0
{

    public  partial class StraightStairs : TemplateBaseClass
    {
        public override XElement Compile()
        {
            /*
             * Straight stairs compile directly into base meshes, so we wrap those in a group tag with no attributes
             */
            
            XElement root = new XElement("group");

            for (int i = 0; i < StairCount; i++)
            {
                root.Add(SingleStair(new Point3D(i * HorizontalDistance, i * VerticalDistance, i* LateralDistance), Width, Rise, Run));
            }

            return root;
        }

    }

}
