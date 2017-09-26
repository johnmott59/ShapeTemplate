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
        /// Documentation for an enumeration element
        /// </summary>
        public class EnumerationDoc
        {
            public string XName { get; set; }
            public string FieldName { get; set; }
            public string Description { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="FieldName"></param>
            /// <param name="ContainingType"></param>
            /// <param name="ele"></param>
            public EnumerationDoc(string FieldName, Type ContainingType, XElement ele)
            {
                this.FieldName = FieldName;
                /*
                 * See if this type is in the list
                 */
                // If we got the XML documentation input merge the description in
                if (ele != null)
                {
                    string membernamevalue = "F:" + ContainingType.FullName + "." + FieldName;
                    membernamevalue = membernamevalue.Replace('+', '.');
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
}
