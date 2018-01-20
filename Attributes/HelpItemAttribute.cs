using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{

   /// <summary>
   /// This is the type of the class; how its used. Its a basic shape, a template or simply data
   /// </summary>
    public enum eItemFlavor
    {
        /// <summary>
        /// The Data class type is for simple data types, like points or vectors
        /// </summary>
        Data,
        /// <summary>
        /// The Basic shape data type is for basic shapes, like flat meshes or panels
        /// </summary>
        BasicShape,
        /// <summary>
        /// The template class type is for templates, which are composites of basic types and possibly other templates
        /// </summary>
        Template,
        /// <summary>
        /// An enumeration type has a list of values
        /// </summary>
        Enumeration,
        /// <summary>
        /// The support class type is for classes that are support classes for a template to hold components for the template
        /// </summary>
        TemplateSupport
    }

    /// <summary>
    /// The Help Item Attribute lets us tag a class as to how its used.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class HelpItemAttribute : System.Attribute
    {
        /// <summary>
        /// How this class is used
        /// </summary>
        public eItemFlavor ItemFlavor { get; }
        /// <summary>
        /// element name for API. This is only required for basic shapes and templates
        /// </summary>
        public string XName { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ClassType"></param>
        /// <param name="XName"></param>
        public HelpItemAttribute(eItemFlavor ClassType,string XName)
        {
            this.ItemFlavor = ClassType;
            this.XName = XName;
        }
    }
}
