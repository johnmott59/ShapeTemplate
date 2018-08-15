using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    /*
     * The compilable root base class implements the ability to load and save properties as well has a compile() entry point.
     * The Compile entry point is an opportunity for the class to build xml which is ready to be sent to the API. This compilation
     * may involve converting to basic meshes or it may involve rendering properties, with the API doing the conversion internally
     */
    public class CompilableRoot : ILoadAndSaveProperties, ICompile
    {



        public virtual XElement GetProperties(string PropertyName = "")
        {
            return new XElement("root");
        }

        public virtual bool LoadProperties(XElement ele, out string message)
        {
            message = "OK";
            return true;
        }

        public virtual XElement Compile()
        {
            return new XElement("root");
        }

        /*
         * Utility routines for assisting in extracting template values
         */
        public bool GetPropertyInt(XElement template, string propertyname, out int value, out string message)
        {
            int iTmp;
            value = 0;
            message = "OK";

            // find this property
            XAttribute oAtt = Utilities.GetPropertyAttribute(template, propertyname);
            if (oAtt == null)
            {
                message = $"Missing {propertyname} property";
                return false;
            }

            if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
            value = iTmp;

            return true;
        }

        public bool GetPropertyFloat(XElement template, string propertyname, out float value, out string message)
        {
            float iTmp;
            value = 0;
            message = "OK";

            // find this property
            XAttribute oAtt = Utilities.GetPropertyAttribute(template, propertyname);
            if (oAtt == null)
            {
                message = $"Missing {propertyname} property";
                return false;
            }

            if (!Utilities.GetFloatFromAttribute(oAtt, out iTmp, out message)) return false;
            value = iTmp;

            return true;
        }

    }
}
