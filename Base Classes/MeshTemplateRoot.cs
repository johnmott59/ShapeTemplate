using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplate
{
    public class MeshTemplateRoot : IMeshTemplate
    {
        public virtual XElement GetElement()
        {
            return new XElement("root");
        }
    }

}
