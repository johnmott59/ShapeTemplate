using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplate
{
    public class MeshDisplayProperties : IXElement
    {
        public enum eMeshElementVisibility
        {
            ShowNeither = 0,
            ShowFront = 1,
            ShowBack = 2,
            ShowBoth = 3
        }

        public string Color { get; set; } = "0xFFFFFF";
        public string Name { get; set; } = "MaterialName";
        public string TextureFile { get; set; } = "";           // this is the texture file name
        public eMeshElementVisibility SidesToShow { get; set; } = eMeshElementVisibility.ShowBoth;

        public void GetXElement(XElement parent)
        {
            parent.Add(new XAttribute("materialcolor", this.Color),
                       new XAttribute("materialname", this.Name),
                       new XAttribute("texturefilename", this.TextureFile),
                       new XAttribute("sidestoshow", GetSidesToShow(this.SidesToShow))
                       );
        }


        public XElement GetXElement()
        {
            XElement disp = new XElement("displayproperties");

            GetXElement(disp);

            return disp;

        }

        private string GetSidesToShow(eMeshElementVisibility sides)
        {
            switch (sides)
            {
                case eMeshElementVisibility.ShowBoth: return "both";
                case eMeshElementVisibility.ShowBack: return "back";
                case eMeshElementVisibility.ShowFront: return "front";
                case eMeshElementVisibility.ShowNeither: return "neither";
            }
            return "both";
        }
    }

}
