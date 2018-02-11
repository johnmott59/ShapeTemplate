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
        public override XElement Compile()
        {
            XElement root = new XElement("straightstairs");

            for (int i = 0; i < StairCount; i++)
            {
                root.Add(SingleStair(new Point3D(i * HorizontalDistance, i * VerticalDistance, i* LateralDistance), Width, Rise, Run));
            }

            return root;
        }

    }

}
