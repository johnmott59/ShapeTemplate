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
        /// Show the XML elements used for an instance of this class
        /// </summary>
        /// <returns></returns>
        public XElement XMLStructure ()
        {
            XElement node = new XElement(XName);

            // add each attribute
            foreach (PropertyDoc pd in PropertyDocList.Where(m=>m.XPropertyPosition == HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent))
            {
                node.Add(new XAttribute(pd.XName, pd.SampleValue));
            }

            // Add in any property nodes if this is a template
            foreach (PropertyDoc pd in PropertyDocList.Where(m => m.XPropertyPosition == HelpPropertyAttribute.eXPropertyPosition.TemplateProperty))
            {
                node.Add(new XElement("property", new XAttribute(pd.XName, pd.SampleValue)));
            }

            // Add in element nodes

            foreach (PropertyDoc pd in PropertyDocList.Where(m => m.XPropertyPosition == HelpPropertyAttribute.eXPropertyPosition.XElement))
            {
                if (pd.TypeIsExposed && !pd.IsList)
                {
                    node.Add(new XElement(pd.PropertyType.Name.ToLower(), new XAttribute("prop", ""))); 
                }
                else
                {
                    node.Add(new XElement(pd.XName));
                }

            }

            return node;
        }
    }
}
