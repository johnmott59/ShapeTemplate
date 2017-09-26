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
        /// Retrieve a single class marked with the help attribute
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="classname"></param>
        /// <returns></returns>
        public static ItemDoc GetMarkedClass(XElement ele, string classname)
        {
            /*
             * Loop through all of the types in this dll, extracting the classes that are of the class type that we want
             */
            Assembly myAssembly = Assembly.GetExecutingAssembly();

            Type t = myAssembly.GetTypes().Where(
                m => m.Name == classname &&
                m.GetCustomAttribute<HelpItemAttribute>() != null).FirstOrDefault();

            if (t != null) return new ItemDoc(ele, t);

            return null;

        }

    }
}
