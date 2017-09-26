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
        ///  Static entry point to retrieve template classes
        /// </summary>
        /// <param name="ele"></param>
        /// <returns></returns>

        public static List<ItemDoc> GetBasicShapeClasses(XElement ele)
        {
            return GetMarkedClasses(ele, eItemFlavor.BasicShape);
        }

    }
}
