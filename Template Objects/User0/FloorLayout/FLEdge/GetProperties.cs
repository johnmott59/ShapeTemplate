using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FLEdge : ILoadAndSaveProperties
    {
        public XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement(nameof(FLEdge).ToLower(), new XAttribute("prop", PropertyName));

            root.Add(new XElement("property", new XAttribute(nameof(Index).ToLower(), Index)));

            root.Add(new XElement("property", new XAttribute(nameof(IsExteriorEdge).ToLower(), IsExteriorEdge)));

            root.Add(new XElement("property", new XAttribute(nameof(IsOpenSpaceEdge).ToLower(), IsOpenSpaceEdge)));

            root.Add(new XElement("property", new XAttribute(nameof(DoorCandidate).ToLower(), DoorCandidate)));

            root.Add(new XElement("property", new XAttribute(nameof(DoorPresent).ToLower(), DoorPresent)));

            root.Add(new XElement("property", new XAttribute(nameof(WindowCandidate).ToLower(), WindowCandidate)));

            root.Add(new XElement("property", new XAttribute(nameof(p1).ToLower(), p1)));

            root.Add(new XElement("property", new XAttribute(nameof(p2).ToLower(), p2)));

            return root;
        }
    }
}
