using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// The single room is a template for creating a rectangular building with a single room with a space for a door and up
    /// to 4 windows and a flat roof. This will use the simplelayout template as its basis, so it will compile to a simplelayout
    /// </summary>
    [HelpItem(eItemFlavor.Template, "singleroombuilding")]
    public partial class SingleRoomBuilding : TemplateRoot
    {

        public override XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("template",
                new XAttribute("prop", PropertyName),
                new XAttribute("user", "User0"),
                new XAttribute("name", "singleroombuilding"),

            new XElement("property", new XAttribute(nameof(HorizontalScale).ToLower(), HorizontalScale)),
            new XElement("property", new XAttribute(nameof(VerticalScale).ToLower(), VerticalScale)),
            new XElement("property", new XAttribute(nameof(Width).ToLower(), Width)),
            new XElement("property", new XAttribute(nameof(Height).ToLower(), Height)),
            new XElement("property", new XAttribute(nameof(Length).ToLower(), Length)),
            new XElement("property",new XAttribute(nameof(Thickness).ToLower(),Thickness)),
            new XElement("property", new XAttribute(nameof(RoofOffset).ToLower(), RoofOffset))
            );


            
            root.Add(Door.GetProperties("door"));
            root.Add(FrontWindow.GetProperties("frontwindow"));
            root.Add(LeftWindow.GetProperties("leftwindow"));
            root.Add(RearWindow.GetProperties("rearwindow"));
            root.Add(RightWindow.GetProperties("rightwindow"));
          
            return root;
        }
   
    }
}
