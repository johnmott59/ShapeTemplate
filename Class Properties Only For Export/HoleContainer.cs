using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    /// <summary>
    /// The HoleContainer class is used to hold an array of Holes. We use arrays to hold lists of items that are going to 
    /// go back and forth between JS and .NET because we can use the same class structure and common code. Keeping an array
    /// inside a wrapper lets us do that.
    /// </summary>
    [HelpItem(eItemFlavor.Data, "holecontainer")]
    public partial class HoleContainer 
    {
        /// <summary>
        /// List of Holes
        /// </summary>
        [HelpProperty("HoleContainer")]

        // The reason that this is an array instead of a list is that its a data structure that may be exported to and imported
        // from JSON, and this data structure may be referenced both in JS and in C# so its best to keep it the same

        public Hole[] HoleList { get; set; } = new Hole[0];


        // The CopyFrom method is used in Javascript to convert a version of the object retrieved via JSON
        // into a full object of this type. An object retrieved via JSON.parse() will have the correct property
        // names but not be an object of this type, this will take that property-only version and create a full
        // object
        public static HoleContainer CopyFrom(HoleContainer oFrom)
        {
            HoleContainer oOutline = new HoleContainer();
            oOutline.HoleList = new Hole[oFrom.HoleList.Length];
            for (int i=0; i < oFrom.HoleList.Length; i++)
            {
                Hole p = oFrom.HoleList[i];
                oOutline.HoleList[i] = Hole.CopyFrom(p);
            }

            return oOutline;
        }
    }


}