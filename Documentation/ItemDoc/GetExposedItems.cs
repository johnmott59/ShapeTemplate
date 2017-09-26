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
        private static List<string> _ExposedItemList = null;
        /// <summary>
        /// Static list of the names of the exposed items, data classes, enumerations, basic shape and templates
        /// This will be generated once for each run of whatever program instantiates the library
        /// </summary>
        public static List<string> ExposedItemList
        {
            get
            {
                if (_ExposedItemList == null)
                {
                    _ExposedItemList = GetExposedItems();
                }
                return _ExposedItemList;
            }
        }
        /// <summary>
        /// This routine will return the list of exposed classes and other data structures for use with documentation
        /// </summary>
        /// <returns></returns>
        public static List<string> GetExposedItems()
        {
            List<string> list = new List<string>();
            /*
             * Loop through all of the types in this dll, extracting the classes that are of the class type that we want
             */
            Assembly myAssembly = Assembly.GetExecutingAssembly();

            foreach(Type t in myAssembly.GetTypes().Where(m => m.GetCustomAttribute<HelpItemAttribute>() != null))
            {
                list.Add(t.Name);
            }

            return list;

        }

    }
}
