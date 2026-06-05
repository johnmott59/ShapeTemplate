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

    public  partial class StraightStairsWithRunList : TemplateBaseClass
    {
        public override XElement Compile()
        {
            /*
             * Straight stairs compile directly into base meshes, so we wrap those in a group tag with no attributes
             */
            XElement root = new XElement("group");

            int RunLength = 0;
            for (int i = 0; i < RunList.Length; i++)
            {
                root.Add(SingleStair(new Point3D(RunLength, i * VerticalDistance,0), Width, Rise, RunList[i]));
                RunLength += RunList[i];
            }

            return root;
        }

    }

}
