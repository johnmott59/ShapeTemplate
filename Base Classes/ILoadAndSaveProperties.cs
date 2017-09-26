using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    /// <summary>
    /// Interface for documentation objects to load, save and return documentation information
    /// </summary>
    public interface ILoadAndSaveProperties
    {
        /// <summary>
        ///  The properties for either a basic shape or a template let you set and retrieve properties and then 'compile' to basic shapes.
        ///  In the event that this instance is a property in another class the property name can be passed in
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        XElement GetProperties(string PropertyName="");

        /// <summary>
        /// The LoadProperties routine loads from an XElement to instantiate class properties
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="Message"></param>
        /// <returns></returns>

        bool LoadProperties(XElement ele,out string Message);  // 1 == success. 0 == failure with message

    }
}

