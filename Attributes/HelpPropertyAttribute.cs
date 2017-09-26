using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{

    /// <summary>
    /// The APIPropertyAttribute attribute lets as provide the name of this property in the XML as well as a sample value.
    /// Not all properties of a class are necessarily in the XML.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class HelpPropertyAttribute : System.Attribute
    {
        /// <summary>
        /// How this property is positioned in XML
        /// </summary>
        public enum eXPropertyPosition
        {
            /// <summary>
            /// This property is an XElement, possibly with children
            /// </summary>
            XElement,
            /// <summary>
            /// This property is a single value and is an attribute of the parent
            /// </summary>
            AttributeOfParent,
            /// <summary>
            /// This property is an attribute of a property XElement in a template
            /// </summary>
            TemplateProperty

        }
        /// <summary>
        /// Name of this item in XML
        /// </summary>
        public string XName { get; }

        /// <summary>
        ///  Constructor
        /// </summary>
        public HelpPropertyAttribute(string XName = "") {
            this.XName = XName;
        }
        /// <summary>
        /// How this property is positioned 
        /// </summary>
        public eXPropertyPosition XPropertyPosition { get; set; }

        /// <summary>
        /// For documentation, a sample value
        /// </summary>
        public string SampleValue { get; set; } = "";
    }

}
