using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ShapeTemplateLib
{
    public partial class ItemDoc
    {
        /// <summary>
        /// Class name
        /// </summary>
        public string Name { get; set; }
     
        /// <summary>
        /// Name of element for API if this is a top level node, usually the class name all lower case
        /// </summary>
        public string XName { get; set; }
    
        /// <summary>
        /// Type of this class
        /// </summary>
        public Type ItemType { get; set; }
     
        /// <summary>
        /// Description of this class, taken from XML documentation
        /// </summary>
        public string Description { get; set; }
      
        /// <summary>
        /// Flabor of this, basic shape class, template class, data class or enumeration
        /// </summary>
        public eItemFlavor ItemFlavor { get; set; }

        /// <summary>
        /// List of properties for this shape or template, if this is class
        /// </summary>
        public List<PropertyDoc> PropertyDocList { get; set; } = new List<PropertyDoc>();
        
        /// <summary>
        /// List of values of this enumeration, if this is an enumberation
        /// </summary>
        public List<EnumerationDoc> EnumerationDocList { get; set; } = new List<EnumerationDoc>();

    }
}
