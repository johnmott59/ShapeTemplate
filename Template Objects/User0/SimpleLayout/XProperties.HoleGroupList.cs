#define DOTNET
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SimpleLayout 
    {
        //-----------------------------------
        protected XElement GetHoleGroupList()
        {
            XElement XHoleGroupList = new XElement("holegrouplist");
            foreach (HoleGroup hg in HoleGroupList)
            {
                XElement XHoleGroup = new XElement("holegroup",
                    new XAttribute("holegroupid", hg.HoleGroupID));
                XHoleGroupList.Add(XHoleGroup);
                foreach (LayoutHole h in hg.HoleList)
                {
                    XElement boundary = new XElement("boundary");

                    boundary.Add(new XAttribute("offsetx", h.OffsetX));
                    boundary.Add(new XAttribute("offsety", h.OffsetY));
                    boundary.Add(new XAttribute("holetype", h.HoleType));
                    boundary.Add(new XAttribute("holetypeindex", h.HoleTypeIndex));

                    XHoleGroup.Add(boundary);
                }
            }
            return XHoleGroupList;
        }
        //----------------------------------------------------------------
        protected bool LoadHoleGroupList(XElement ele, out string message)
        {
            message = "OK";

            XElement XHoleGroupList = Utilities.GetListElement(ele, "holegrouplist");

            // an empty layout would have no segments.
            if (XHoleGroupList == null) return true;

            foreach (XElement XHoleGroup in XHoleGroupList.Elements("holegroup"))
            {
                string id;
             

                // Create a new hold group -- a set of holes with an ID
                HoleGroup hg = new HoleGroup();
                HoleGroupList.Add(hg);

                if (!Utilities.GetStringAttribute(XHoleGroup, "holegroupid", out id, out message)) return false;
                hg.HoleGroupID = id;

                // Process each boundary
                foreach (XElement XBoundary in XHoleGroup.Elements("boundary"))
                {
                    float offsetx, offsety;
                    int holetypeindex;
                    string holetype;

                    if (!Utilities.GetFloatAttribute(XBoundary, "offsetx", out offsetx, out message)) return false;
                    if (!Utilities.GetFloatAttribute(XBoundary, "offsety", out offsety, out message)) return false;
                    if (!Utilities.GetIntAttribute(XBoundary, "holetypeindex", out holetypeindex, out message)) return false;
                    if (!Utilities.GetStringAttribute(XBoundary, "holetype", out holetype, out message)) return false;

                    // Add this hole to this holegroup
                    LayoutHole h = new LayoutHole();
                    h.OffsetX = offsetx;
                    h.OffsetY = offsety;
                    h.HoleType = holetype;
                    h.HoleTypeIndex = holetypeindex;
#if DOTNET
                    // convert to list, add, then reconvert to array
                    List<LayoutHole> tmp = new List<LayoutHole>(hg.HoleList);
                    tmp.Add(h);
                    hg.HoleList = tmp.ToArray();
#else
                    hg.push(h);     // JS add
#endif

                }
            }

            return true;
        }
    }
}
