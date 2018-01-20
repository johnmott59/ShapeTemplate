using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ShapeTemplateLib
{
    /// <summary>
    /// Collect and hold documentation on each base class from its attribute
    /// </summary>
    public partial class ItemDoc
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="ItemType"></param>
        public ItemDoc(XElement ele, Type ItemType)
        {
            Name = ItemType.Name;
            this.ItemType = ItemType;

            // Retrieve the help attribute for this class to get the class type and description
            HelpItemAttribute hlp = ItemType.GetCustomAttribute<HelpItemAttribute>();

            this.ItemFlavor = hlp.ItemFlavor;
            this.XName = hlp.XName;

            string itemname = ItemType.FullName;
            // if this is a nested type name replace '+' with '.'
            itemname = itemname.Replace('+', '.');

            if (ele != null)
            {
                XElement member = ele.Descendants("member")
                        .Where(m => m.Attribute("name").Value.EndsWith(itemname)).FirstOrDefault();
                if (member != null)
                {
                    XElement summary = member.Element("summary");
                    if (summary != null)
                    {
                        Description = summary.Value;
                    }
                }
            }
            if (ItemType.IsEnum) {
                foreach (FieldInfo fi in ItemType.GetFields().Where(m => m.GetCustomAttribute<HelpPropertyAttribute>() != null))
                {
                    HelpPropertyAttribute hpa = fi.GetCustomAttribute<HelpPropertyAttribute>();

                    EnumerationDocList.Add(new EnumerationDoc(fi.Name, ItemType, ele) { XName = hpa.XName });
                }

#if false
                var enumName = ItemType.Name;
                foreach (var fieldInfo in ItemType.GetFields())
                {
                    if (fieldInfo.FieldType.IsEnum)
                    {
                        EnumerationValueList.Add(new EnumerationValue()
                        {
                            Name = fieldInfo.Name,
                            Value = (int) fieldInfo.GetRawConstantValue()
                        });
                    }
                }
#endif
                }
            else
            {
                PropertyDocList = new List<PropertyDoc>();
                /*
                 * Get the properties for this class that have help property attributes
                 */
                PropertyInfo[] pilist = ItemType.GetProperties();

                foreach (PropertyInfo pi in ItemType.GetProperties().Where(m => m.GetCustomAttribute<HelpPropertyAttribute>() != null))
                {
                    HelpPropertyAttribute hpa = pi.GetCustomAttribute<HelpPropertyAttribute>();
                    /*
                     * Merge the values from the [Attribute] on the property and the /// xml document
                     * The Property attribute may or may not have had the xml name. 
                     */
                    PropertyDoc pd = new PropertyDoc(pi.Name, pi.PropertyType, ItemType, ele)
                    {
                        XName = String.IsNullOrEmpty(hpa.XName) ? pi.Name.ToLower() : hpa.XName,
                        SampleValue = hpa.SampleValue,
                        XPropertyPosition = hpa.XPropertyPosition
                    };

                    PropertyDocList.Add(pd);
                }
            }
        }

    }
}
