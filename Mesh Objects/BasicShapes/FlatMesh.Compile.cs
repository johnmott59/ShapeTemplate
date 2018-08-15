using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// a simple panel has a front, back connecting mesh and one or more holes

namespace ShapeTemplateLib.BasicShapes
{

    public partial class FlatMesh :  TransformableRoot
    {

        // For a basic shape the compile step is the same as getting properties
        public override XElement Compile()
        {
            return GetProperties();
        }

#if false
        // For a basic shape the compile step is the same as getting properties
        public XElement Compile(string ParentName)
        {
            return GetProperties(ParentName);
        }
#endif
}

}
    
