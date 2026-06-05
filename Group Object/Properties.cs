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
        // What kind of transform for this node?
        public eTransformType? TransformType { get; set; }

        // Value, either degrees or distance
        public float TransformValue { get; set; }

        // A group can contain a list of child groups, this is tree structure
        public List<Group> GroupChildrenList { get; set; } = new List<Group>();

        // At this level of the heirarchy we can hold meshes and templates

        public List<TemplateBaseClass> TemplateList = new List<TemplateBaseClass>();
        public List<TransformableRoot> MeshBaseClassList = new List<TransformableRoot>();

    }
}
