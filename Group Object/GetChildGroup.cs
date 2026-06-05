using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib;

namespace ShapeTemplateLib
{
    public partial class Group 
    {
        public Group GetChildGroup(eTransformType TransformType, float TransformValue)
        {
            Group g = new Group();
            this.GroupChildrenList.Add(g);

            g.TransformType = TransformType;
            g.TransformValue = TransformValue;

            return g;
        }

        public Group GetChildGroupWithTemplate(eTransformType TransformType, float TransformValue,TemplateBaseClass Template)
        {
            Group g = new Group();
            this.GroupChildrenList.Add(g);

            g.TransformType = TransformType;
            g.TransformValue = TransformValue;

            g.TemplateList.Add(Template);

            return g;
        }
    }
}
