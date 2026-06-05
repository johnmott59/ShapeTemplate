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
        public void Add(Group g)
        {
            this.GroupChildrenList.Add(g);
        }

        public void Add(TemplateBaseClass oTemplate)
        {
            this.TemplateList.Add(oTemplate);
        }

        public void Add(TransformableRoot oMesh)
        {
            this.MeshBaseClassList.Add(oMesh);
        }
    }
}
