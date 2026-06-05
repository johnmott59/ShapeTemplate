using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FMEdge : ILoadAndSaveProperties
    {
        public XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement(nameof(FMEdge).ToLower(), new XAttribute("prop", PropertyName));

            root.Add(new XElement("property", new XAttribute(nameof(Index).ToLower(), Index)));

            root.Add(new XElement("property", new XAttribute(nameof(IsExteriorEdge).ToLower(), IsExteriorEdge)));

            root.Add(new XElement("property", new XAttribute(nameof(IsOpenSpaceEdge).ToLower(), IsOpenSpaceEdge)));

            root.Add(new XElement("property", new XAttribute(nameof(InteriorDoorCandidate).ToLower(), InteriorDoorCandidate)));

            root.Add(new XElement("property", new XAttribute(nameof(DoorPresent).ToLower(), DoorPresent)));

            root.Add(new XElement("property", new XAttribute(nameof(ExteriorWindowCandidate).ToLower(), ExteriorWindowCandidate)));

            root.Add(new XElement("property", new XAttribute(nameof(p1).ToLower(), p1)));

            root.Add(new XElement("property", new XAttribute(nameof(p2).ToLower(), p2)));

            root.Add(new XElement("property", new XAttribute(nameof(ID).ToLower(), ID == null ? "" : ID)));

            return root;
        }
    }
}
