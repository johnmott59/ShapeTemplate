using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public class MeshDisplayProperties : ILoadAndSaveProperties
    {

        public MeshDisplayProperties()
        {

        }
        // Copy constructor
        public MeshDisplayProperties(MeshDisplayProperties CopyMe)
        {
            this.Name = CopyMe.Name;
            this.SidesToShow = CopyMe.SidesToShow;
            this.TextureFile = CopyMe.TextureFile;
            this.Color = CopyMe.Color;

        }

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

        public bool LoadProperties(XElement ele, out string message)
        {
            if (ele == null)
            {
                message = $"Missing displayproperties element";
                return false;
            }

            message = "OK";

            this.Color = Utilities.GetOptionalStringAttribute(ele,"materialcolor");
            this.Name = Utilities.GetOptionalStringAttribute(ele, "materialname");
            this.TextureFile = Utilities.GetOptionalStringAttribute(ele, "texturefilename");

            XAttribute xssd = ele.Attribute("sidestoshow");
            if (xssd != null)
            {
                eMeshElementVisibility v;
                if (!LoadSidesToShow(ele, out v, out message)) return false;
            }

            return true;

        }

        public XElement GetProperties()
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

        private bool LoadSidesToShow(XElement ele,out eMeshElementVisibility value, out string message)
        {
            // Get the string value of

            value = eMeshElementVisibility.ShowBoth;
            message = "OK";

            string v = "";
            if (!Utilities.GetStringAttribute(ele, "sidestoshow", out v, out message)) return false;

            switch (v)
            {
                case "both":
                    value = eMeshElementVisibility.ShowBoth;
                    break;
                case "back":
                    value = eMeshElementVisibility.ShowBack;
                    break;
                case "front":
                    value = eMeshElementVisibility.ShowFront;
                    break;
                case "neither":
                    value = eMeshElementVisibility.ShowNeither;
                    break;
                default:
                    message = $"Expected 'both', 'back','front' or 'neither'. Got {v}";
                    return false;
            }

            return true;
        }
    }

}
