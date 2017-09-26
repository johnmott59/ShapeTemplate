using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
   public class Utilities
    {
        /// <summary>
        /// Return the named attribute of the property child. This is used to retrieve properties for templates
        /// of the form <property fieldname="value"/> where 'fieldname' is a property of the template class
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="propvalue"></param>
        /// <returns></returns>
        public static XAttribute GetPropertyAttribute(XElement ele, string propvalue)
        {
            // Locate the element with this attribute

            XElement p = ele.Elements("property").Where(m => m.FirstAttribute.Name == propvalue.ToLower()).FirstOrDefault();

            if (p == null) return null;

            return p.FirstAttribute;

        }
        public static XElement GetNamedElementWithPropAttribute(XElement ele, string ElementName)
        {
            return GetNamedElementWithPropAttribute(ele, ElementName, ElementName);
        }

        public static XElement GetNamedElementWithPropAttribute(XElement ele, string ElementName,string PropValue)
        {
            return ele.Elements(ElementName)
                .FirstOrDefault(el => el.Attribute("prop") != null && el.Attribute("prop").Value == PropValue);
        }

        public static bool GetChildElement(XElement parent,string childname,out XElement ele,out string message)
        {
            message = "OK";
            ele = parent.Element(childname);
            if (ele == null)
            {
                message = $"Missing {childname} in {parent.Name.LocalName}";
                return false;
            }

            return true;

        }
        /*
         * Get an optional string attribute
         */
        public static string GetOptionalStringAttribute(XElement ele, string attributeName)
        {
            // get this attribute
            XAttribute oAtt = ele.Attribute(attributeName);

            if (oAtt == null) return "";

            return oAtt.Value ;
        }

        public static bool GetStringAttribute(XElement ele, string attributeName, out string value, out string message)
        {
            message = "OK";
            value = "";
            // get this attribute
            XAttribute oAtt = ele.Attribute(attributeName);
            if (oAtt == null)
            {
                message = $"Missing attribute {attributeName}";
                return false;
            }

            value = oAtt.Value;

            return true;
        }

        public static bool GetIntFromAttribute(XAttribute oAtt, out int value, out string message)
        {
            message = "OK";
            value = 0;

            string s = oAtt.Value;

            if (string.IsNullOrWhiteSpace(s))
            {
                message = $"Expected int, found empty attribute {oAtt.Name}";
                return false;
            }

            if (!int.TryParse(s, out value))
            {
                message = $"Invalid int value for attribute {oAtt.Name}";
                return false;
            }

            return true;
        }

        public static bool GetIntAttribute(XElement ele, string attributeName, out int value, out string message)
        {
            message = "OK";
            value = 0;
            // get this attribute
            XAttribute oAtt = ele.Attribute(attributeName);
            if (oAtt == null)
            {
                message = $"Missing attribute {attributeName}";
                return false;
            }

            string s = oAtt.Value;

            if (string.IsNullOrWhiteSpace(s))
            {
                message = $"Expected int, found empty attribute {attributeName}";
                return false;
            }

            if (!int.TryParse(s, out value))
            {
                message = $"Invalid int value for attribute {attributeName}";
                return false;
            }

            return true;
        }


        public static bool GetFloatAttribute(XElement ele,string attributeName,out float value,out string message)
        {
            message = "OK";
            value = 0;
            // get this attribute
            XAttribute oAtt = ele.Attribute(attributeName);
            if (oAtt == null)
            {
                message = $"Missing attribute {attributeName}";
                return false;
            }

            string s = oAtt.Value;

            if (string.IsNullOrWhiteSpace(s))
            {
                message = $"Expected float, found empty attribute {attributeName}";
                return false;
            }

            if (!float.TryParse(s,out value))
            {
                message = $"Invalid float value for attribute {attributeName}";
                return false;
            }

            return true;
        }
    }
}
