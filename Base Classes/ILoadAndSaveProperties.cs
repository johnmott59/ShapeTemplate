using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    public interface ILoadAndSaveProperties
    {
        /*
         * The properties for either a basic shape or a template let you set and retrieve properties and then 'compile' to basic shapes.
         */
        XElement GetProperties();

        bool LoadProperties(XElement ele,out string Message);  // 1 == success. 0 == failure with message

    }
}

