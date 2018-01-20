using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    public class PropertyDoc
    {
        public enum eEditControl
        {
            TextBox,
            CheckBox,
            RadioGroup,
            SingleDropdown,
            DualDropDown,
            BoxDimensions,
            EllipseDimensions
        }

        public HelpPropertyAttribute.eXPropertyPosition XPropertyPosition { get; set; }

        public string PropertyName { get; set; }            // property name in the class
        public string XName { get; set; }                   // property name for the API
        public Type PropertyType { get; set; }              // type of this property
        public string Description { get; set; }             // the description comes from the XML Documentation, if available
    //    public eEditControl EditControl { get; set; }     // type of edit control
    //    public bool IsXAttribute { get; set; }              // Is this an XAttribute of the class or XElement
        public string SampleValue { get; set; }             // Example value
        public bool TypeIsExposed { get; set; }              // Is this an exposed type, a basic shape, template or data class?
        public bool IsList { get; set; }


/// <summary>
/// Constructor
/// </summary>
/// <param name="PropertyName"></param>
/// <param name="PropertyType"></param>
/// <param name="ContainingType"></param>
/// <param name="ele"></param>
    
        public PropertyDoc(string PropertyName, Type PropertyType,Type ContainingType,XElement ele)
        {
            this.PropertyName = PropertyName;

            /*
             * If this is a list, note that and pull out the thing that this is a list of
             */
            if ((PropertyType.IsGenericType && PropertyType.GetGenericTypeDefinition() == typeof(List<>)))
            {
                this.PropertyType = PropertyType.GetGenericArguments()[0];
                IsList = true;
            }
            else
            {
                this.PropertyType = PropertyType;
            }
            // If the property is an array, remove the []
            string pname = this.PropertyType.Name;
            if (this.PropertyType.IsArray) pname = pname.Replace("[]", "");

            // Remember whether this property is an exposed type, a basic shape, template or data class
            this.TypeIsExposed = ItemDoc.ExposedItemList.Where(m => m == pname).Count() > 0 ? true : false;
            /*
             * See if this type is in the list
             */
            // If we got the XML documentation input merge the description in
            if (ele != null)
            {
                string membernamevalue = "P:" + ContainingType.FullName + "." + PropertyName;
                XElement member = ele.Descendants("member")
                        .Where(m => m.Attribute("name").Value == membernamevalue).FirstOrDefault();

                if (member != null)
                {
                    XElement summary = member.Element("summary");
                    if (summary != null)
                    {
                        Description = summary.Value;
                    }
                }
            }
        }
    }

}
