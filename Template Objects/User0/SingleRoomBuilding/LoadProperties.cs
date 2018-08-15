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
    public partial class SingleRoomBuilding 
    {

        public override bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float iTmp;
            message = "OK";

            if (!GetPropertyFloat(xTemplateNode, nameof(Width), out iTmp, out message)) return false;
            Width = iTmp;

            if (!GetPropertyFloat(xTemplateNode, nameof(Height), out iTmp, out message)) return false;
            Height = iTmp;

            if (!GetPropertyFloat(xTemplateNode, nameof(Length), out iTmp, out message)) return false;
            Length = iTmp;

            if (!GetPropertyFloat(xTemplateNode, nameof(HorizontalScale), out iTmp, out message)) return false;
            HorizontalScale = iTmp;

            if (!GetPropertyFloat(xTemplateNode, nameof(VerticalScale), out iTmp, out message)) return false;
            VerticalScale = iTmp;

            if (!GetPropertyFloat(xTemplateNode, nameof(Thickness), out iTmp, out message)) return false;
            Thickness = iTmp;

            if (!GetPropertyFloat(xTemplateNode, nameof(RoofOffset), out iTmp, out message)) return false;
            RoofOffset = iTmp;

            XElement xNode;

            xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "hole", "door",out message);
            if (xNode == null) return false;
            if (!Door.LoadProperties(xNode, out message)) return false;

            xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "hole", "frontwindow", out message);
            if (xNode == null) return false;
            if (!FrontWindow.LoadProperties(xNode, out message)) return false;

            xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "hole", "leftwindow", out message);
            if (xNode == null) return false;
            if (!LeftWindow.LoadProperties(xNode, out message)) return false;

            xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "hole", "rearwindow", out message);
            if (xNode == null) return false;
            if (!RearWindow.LoadProperties(xNode, out message)) return false;

            xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "hole", "rightwindow", out message);
            if (xNode == null) return false;
            if (!RightWindow.LoadProperties(xNode, out message)) return false;


            return true;
        }

   
    }
}
