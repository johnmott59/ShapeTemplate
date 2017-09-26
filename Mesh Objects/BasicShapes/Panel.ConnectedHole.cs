using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// a simple panel has a front, back connecting mesh and one or more holes

namespace ShapeTemplateLib.BasicShapes
{
    public partial class Panel 
    {
        /// <summary>
        /// A connected hole is part of a panel. It is composed of two definitions, one for the front and one 
        /// for the back, and the mesh display properties for the connecting mesh
        /// </summary>
        [HelpItem(eItemFlavor.Data,"connectedhole")]
        public class ConnectedHole : ILoadAndSaveProperties
        {
            /// <summary>
            /// This field contains the ID value of the hole on the front of the mesh to use to connect
            /// </summary>
            [HelpProperty(XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue ="fmdoor")]
            public string FrontID { get; set; }

            /// <summary>
            /// This field contains the ID value of the hole on the back of the mesh to use to connect
            /// </summary>
            [HelpProperty( XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue = "bmdoor")]
            public string BackID { get; set; }

            /// <summary>
            /// These are the display properties of the mesh connecting the two holes
            /// </summary>
            [HelpProperty]
            public MeshDisplayProperties DisplayProperties { get; set; } = new MeshDisplayProperties();

            public XElement GetProperties(string PropertyName="")
            {
                return new XElement("connectedhole", 
                    new XAttribute("prop",PropertyName),
                    new XAttribute(nameof(FrontID).ToLower(), FrontID),
                    new XAttribute(nameof(BackID).ToLower(), BackID),
                    DisplayProperties.GetProperties());
            }

            public bool LoadProperties(XElement ele, out string Message)
            {
                Message = "OK";

                if (ele.Attribute(nameof(FrontID).ToLower()) == null)
                {
                    Message = nameof(FrontID).ToLower() + " required for connectedhole";
                    return false;
                }
                this.FrontID = ele.Attribute(nameof(FrontID).ToLower()).Value;
                if (String.IsNullOrEmpty(this.FrontID))
                {
                    Message = nameof(FrontID).ToLower() + " requires a value";
                    return false;
                }

                if (ele.Attribute(nameof(BackID).ToLower()) == null)
                {
                    Message = nameof(BackID).ToLower() + " required for connectedhole";
                    return false;
                }
                this.BackID = ele.Attribute(nameof(BackID).ToLower()).Value;
                if (String.IsNullOrEmpty(this.FrontID))
                {
                    Message = nameof(BackID).ToLower() + " requires a value";
                    return false;
                }

                if (ele.Element(nameof(DisplayProperties).ToLower()) != null)
                {
                    this.DisplayProperties = new MeshDisplayProperties();
                    if (!DisplayProperties.LoadProperties(ele.Element(nameof(DisplayProperties).ToLower()), out Message)) return false;
                }

                return true;

            }
        }
    }
}
