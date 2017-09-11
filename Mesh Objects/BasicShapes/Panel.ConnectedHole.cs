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
        // A connected hole is composed of two definitions, one for the front and one for the back, and the mesh display 
        // properties for the connector

        public class ConnectedHole : ILoadAndSaveProperties
        {
            public string FrontID { get; set; }
            public string BackID { get; set; }
            public MeshDisplayProperties DisplayProperties { get; set; } = new MeshDisplayProperties();

            public XElement GetProperties()
            {
                return new XElement("connectedhole", new XAttribute("frontid", this.FrontID), new XAttribute("backid", this.BackID),
                    DisplayProperties.GetProperties());
            }

            public bool LoadProperties(XElement ele, out string Message)
            {
                Message = "OK";

                if (ele.Attribute("frontid") == null)
                {
                    Message = "frontid required for connectedhole";
                    return false;
                }
                this.FrontID = ele.Attribute("frontid").Value;
                if (String.IsNullOrEmpty(this.FrontID))
                {
                    Message = "frontid requires a value";
                    return false;
                }

                if (ele.Attribute("backid") == null)
                {
                    Message = "backid required for connectedhole";
                    return false;
                }
                this.BackID = ele.Attribute("backid").Value;
                if (String.IsNullOrEmpty(this.FrontID))
                {
                    Message = "backid requires a value";
                    return false;
                }

                if (ele.Element("displayproperties") != null)
                {
                    this.DisplayProperties = new MeshDisplayProperties();
                    if (!DisplayProperties.LoadProperties(ele.Element("displayproperties"), out Message)) return false;
                }

                return true;

            }
        }
    }
}
