using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    public class TemplateRoot : ILoadAndSaveProperties
    {
        public virtual XElement GetProperties()
        {
            return new XElement("root");
        }
        
        public virtual bool LoadProperties(XElement ele, out string message)
        {
            message = "OK";
            return true;
        }

        public virtual XElement Compile()
        {
            return new XElement("root");
        }

    }

}
