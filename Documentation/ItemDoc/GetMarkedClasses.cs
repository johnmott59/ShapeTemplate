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
        /// Pull out the Attribute descriptions for this class and its properties and save them in an ItemDoc.
        /// This will allow the documentation to be created. The ele argument is optional, if we are able to pass in
        /// the generated comments we can pull more detail out.
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="ClassType"></param>
        /// <returns></returns>
        public static List<ItemDoc> GetMarkedClasses(XElement ele, eItemFlavor ClassType)
        {
            List<ItemDoc> DocRootList = new List<ItemDoc>();
            /*
             * Loop through all of the types in this dll, extracting the classes that are of the class type that we want
             */
            Assembly myAssembly = Assembly.GetExecutingAssembly();

            foreach (Type type in myAssembly.GetTypes().Where(m =>
                m.GetCustomAttribute<HelpItemAttribute>() != null &&
                m.GetCustomAttribute<HelpItemAttribute>().ItemFlavor == ClassType
                ))
            {
                // Instantiate this class and get its ItemDoc descriptor
                //        DocRoot dr = (DocRoot) Activator.CreateInstance(type);

                DocRootList.Add(new ItemDoc(ele, type));
            }

            return DocRootList;
        }

        /// <summary>
        /// Entry point to retrieve all marked classes, for use in developing an index, perpaps
        /// </summary>
        /// <param name="ele"></param>
        /// <returns></returns>
        public static List<ItemDoc> GetMarkedClasses(XElement ele)
        {
            List<ItemDoc> DocRootList = new List<ItemDoc>();
            /*
             * Loop through all of the types in this dll, extracting the classes that are of the class type that we want
             */
            Assembly myAssembly = Assembly.GetExecutingAssembly();

            foreach (Type type in myAssembly.GetTypes().Where(m => m.GetCustomAttribute<HelpItemAttribute>() != null))
            {
                DocRootList.Add(new ItemDoc(ele, type));
            }

            return DocRootList;
        }
    }
}
