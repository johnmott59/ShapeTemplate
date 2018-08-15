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

        public static bool GetStringProperty(XElement ele, string PropertyName, out string sValue, out string message)
        {
            sValue = "";
            message = "";

            XAttribute oAtt = GetPropertyAttribute(ele, PropertyName);
            if (oAtt == null)
            {
                message = PropertyName + " property not found";
                return false;
            }

            sValue = oAtt.Value;

            return true;

        }

        public static bool GetFloatProperty(XElement ele, string PropertyName, out float fValue, out string message)
        {
            fValue = 0;
            message = "";

            XAttribute oAtt = GetPropertyAttribute(ele, PropertyName);
            if (oAtt == null)
            {
                message = PropertyName + " property not found";
                return false;
            }

            string sValue = oAtt.Value;
            if (sValue.Trim() == "")
            {
                message = PropertyName + " is empty, expecting an float value";
                return false;
            }

            if (!float.TryParse(sValue, out fValue))
            {
                message = PropertyName + " is not an float";
                return false;
            }

            return true;


        }

        public static bool GetIntProperty(XElement ele,string PropertyName,out int iValue, out string message)
        {
            iValue = 0;
            message = "";

            XAttribute oAtt = GetPropertyAttribute(ele, PropertyName);
            if (oAtt == null)
            {
                message = PropertyName + " property not found";
                return false;
            }

            string sValue = oAtt.Value;
            if (sValue.Trim() == "")
            {
                message = PropertyName + " is empty, expecting an integer";
                return false;
            }

            if (!Int32.TryParse(sValue,out iValue))
            {
                message = PropertyName + " is not an integer";
                return false;
            }

            return true;


        }


        public static XElement GetNamedElementWithPropAttribute(XElement ele, string ElementName)
        {
            return GetNamedElementWithPropAttribute(ele, ElementName, ElementName);
        }

        public static XElement GetNamedElementWithPropAttribute(XElement xContainer, string ElementName, string PropValue, out string message)
        {
            message = "OK";

            XElement xEle = xContainer.Elements(ElementName)
                .FirstOrDefault(el => el.Attribute("prop") != null && el.Attribute("prop").Value == PropValue);

            if (xEle == null)
            {
                message = $"Missing {ElementName} node with property {PropValue}";
                return null;
            }

            return xEle;
        }

     

        public static XElement GetNamedElementWithPropAttribute(XElement ele, string ElementName,string PropValue)
        {
            return ele.Elements(ElementName)
                .FirstOrDefault(el => el.Attribute("prop") != null && el.Attribute("prop").Value == PropValue);
        }

        public static XElement GetListElement(XElement ele, string ListName)
        {
            return ele.Elements("list")
                .FirstOrDefault(el => el.Attribute("name") != null && el.Attribute("name").Value == ListName);
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

        public static bool GetFloatFromAttribute(XAttribute oAtt, out float value, out string message)
        {
            message = "OK";
            value = 0;

            string s = oAtt.Value;

            if (string.IsNullOrWhiteSpace(s))
            {
                message = $"Expected float, found empty attribute {oAtt.Name}";
                return false;
            }

            if (!float.TryParse(s, out value))
            {
                message = $"Invalid float value for attribute {oAtt.Name}";
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
