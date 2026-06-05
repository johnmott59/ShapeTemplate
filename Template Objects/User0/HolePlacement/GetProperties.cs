
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;
using System.Web.Script.Serialization;

namespace ShapeTemplateLib.Templates.User0 
{
    public partial class HolePlacementTemplate 
    {

        public override XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("template",
          new XAttribute("prop", PropertyName),
          new XAttribute("user", "User0"),
          new XAttribute("name", "HolePlacementTemplate".ToLower()));

            root.Add(new XElement("property", new XAttribute(nameof(Width).ToLower(), Width)));

            root.Add(new XElement("property", new XAttribute(nameof(Height).ToLower(), Height)));

            XElement XGridElementPlanList = new XElement("list", new XAttribute("name", nameof(GridElementPlanList).ToLower()));
            root.Add(XGridElementPlanList);
            XGridElementPlanList.Add(GetGridElementPlanList().Elements());

            return root;
        }

        //--------------------------------
        protected XElement GetGridElementPlanList()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("gridelementplanlist");

            foreach (GridElementPlan r in GridElementPlanList)
            {
                XList.Add(r.GetProperties());
            }

            return XList;
        }


    }
}
