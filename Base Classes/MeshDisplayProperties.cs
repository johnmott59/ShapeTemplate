using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    /// <summary>
    /// The Mesh display properties are associated with a single mesh that has a single UV. The properties
    /// include color, material name, name of texture file and which sides of the mesh to show. The name
    /// of the material will be placed into the output for use by the consumer of the mesh.
    /// </summary>
    [HelpItem(eItemFlavor.Data, "meshdisplayproperties")]
    public class MeshDisplayProperties : ILoadAndSaveProperties
    {
        public MeshDisplayProperties()
        {

        }
        // Copy constructor
        public MeshDisplayProperties(MeshDisplayProperties CopyMe)
        {
            this.MaterialName = CopyMe.MaterialName;
            this.SidesToShow = CopyMe.SidesToShow;
            this.TextureFileName = CopyMe.TextureFileName;
            this.MaterialColor = CopyMe.MaterialColor;

        }
        /// <summary>
        /// This enumeration specifies what sides of the mesh to display. Each mesh is a single set of triangles with a unique UV,
        /// its the smallest aggregation unit. A mesh has a 'front' and a 'back', defined in the context of whatever shape is 
        /// using it. The front and back fall out from the fact that there are a set of triangles and a UV range, so either side
        /// of the triangle can be rendered.
        /// </summary>
        [HelpItem(eItemFlavor.Enumeration,"meshdisplayvisibility")]
        public enum eMeshElementVisibility
        {
            /// <summary>
            /// Show Neither side of the mesh, effectively hiding the mesh
            /// </summary>
            [HelpProperty("neither")]
            ShowNeither = 0,
            /// <summary>
            /// Show the Front of the mesh. Front and back make sense in the context of the shape that's using the mesh
            /// </summary>
            [HelpProperty("front")]
            ShowFront = 1,
            /// <summary>
            /// Show the back of the mesh. Front and Back make sense in the context of the shape that's using the mesh
            /// </summary>
            [HelpProperty("back")]
            ShowBack = 2,
            /// <summary>
            /// Show both sides of the mesh. Note that the UV for the 'back' of the mesh goes from (1,1) -> (0,0)
            /// </summary>
            [HelpProperty("both")]
            ShowBoth = 3
        }

        /// <summary>
        /// The color of the mesh is specified in hex RRGGBB. A leading '#' is accepted but not required
        /// </summary>
        [HelpProperty("materialcolor", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue ="#0000FF")]
        public string MaterialColor { get; set; } = "0xFFFFFF";
        /// <summary>
        /// The name of the material is a value passed to the output for use in the consumer
        /// </summary>
        [HelpProperty("materialname", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue ="FrontMesh")]
        public string MaterialName { get; set; } = "MaterialName";
        /// <summary>
        /// A mesh can have a texture file associated with it. This value is passed to the output.
        /// </summary>
        [HelpProperty("texturefilename", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue ="texture.png")]
        public string TextureFileName { get; set; } = "";           // this is the texture file name
        /// <summary>
        /// A mesh has two sides, and this property specifies which sides to show. The mesh has a notion of 'front' and 'back',
        /// but those are defined in the context of whatever shape includes the mesh
        /// </summary>
        [HelpProperty("sidestoshow", XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue ="both")]
        public eMeshElementVisibility SidesToShow { get; set; } = eMeshElementVisibility.ShowBoth;

        public XElement GetXElement(string PropertyName="")
        {
            return new XElement("meshdisplayproperties",
                new XAttribute("prop",PropertyName),                
                new XAttribute("materialcolor", this.MaterialColor),
                new XAttribute("materialname", this.MaterialName),
                new XAttribute("texturefilename", this.TextureFileName),
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

            this.MaterialColor = Utilities.GetOptionalStringAttribute(ele,"materialcolor");
            this.MaterialName = Utilities.GetOptionalStringAttribute(ele, "materialname");
            this.TextureFileName = Utilities.GetOptionalStringAttribute(ele, "texturefilename");

            XAttribute xssd = ele.Attribute("sidestoshow");
            if (xssd != null)
            {
                eMeshElementVisibility v;
                if (!LoadSidesToShow(ele, out v, out message)) return false;
                this.SidesToShow = v;
            }

            return true;

        }

        public XElement GetProperties(string PropertyName="")
        {
            return GetXElement(PropertyName);
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
