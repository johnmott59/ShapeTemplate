using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Web.Script.Serialization;

// A hole descriptor is used to place holes in panels. 

namespace ShapeTemplateLib
{

    public partial class Hole : ILoadAndSaveProperties
    {
        public XElement GetProperties(string PropertyName = "")
        {
            XElement hole = new XElement("hole",
                    new XAttribute("prop", PropertyName),
                    new XAttribute("id", this.ID));

            hole.Add(Offset.GetProperties("offset"));
            hole.Add(Boundary.WrapAndGetProperties("boundary"));

            return hole;
        }

        public bool LoadProperties(XElement ele, out string message)
        {
            message = "OK";

            // Possibly get ID

            if (ele.Attribute("id") != null)
            {
                this.ID = ele.Attribute("id").Value;
            }

            XElement xOffset = Utilities.GetNamedElementWithPropAttribute(ele, "point3d", "offset");
            if (xOffset != null)
            {
                if (!Offset.LoadProperties(xOffset, out message)) return false;
            }

            XElement xBoundary = Utilities.GetNamedElementWithPropAttribute(ele, "boundary", "boundary");
            if (xBoundary != null)
            {
                BoundaryRoot bound;
                if (!BoundaryRoot.UnwrapAndLoadProperties(xBoundary, out bound, out message)) return false;
                this.Boundary = bound;
            }

            return true;
        }
        /// <summary>
        /// When loading the Hole class from JSON we have to separately load the Boundary type 
        /// </summary>
        /// <param name="JSON"></param>

        public static Hole LoadFromJSON(string JSON)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            Hole oHole = ser.Deserialize<Hole>(JSON);

            // We have to extract the boundary from the JSON a field at a time since its contained within the JSON for the hole

            var fieldlist = ser.DeserializeObject(JSON) as Dictionary<string, object>;

            KeyValuePair<string, object> kvp = fieldlist.Where(m => m.Key == "Boundary").FirstOrDefault();
            Dictionary<string, object> dictBoundaryProperties = (Dictionary<string, object>)kvp.Value;

            oHole.Boundary = BoundaryRoot.LoadFromDictionary(oHole.Boundary.BoundaryType, dictBoundaryProperties);

            return oHole;

        }




    }
}

