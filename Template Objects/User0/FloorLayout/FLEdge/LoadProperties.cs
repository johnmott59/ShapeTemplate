using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FLEdge
    {
        public bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;

            if (!Utilities.GetIntProperty(xTemplateNode, nameof(Index).ToLower(), out iTmp, out message)) return false;
            Index = iTmp;

            if (!Utilities.GetIntProperty(xTemplateNode, nameof(IsExteriorEdge).ToLower(), out iTmp, out message)) return false;
            IsExteriorEdge = iTmp;

            if (!Utilities.GetIntProperty(xTemplateNode, nameof(IsOpenSpaceEdge).ToLower(), out iTmp, out message)) return false;
            IsOpenSpaceEdge = iTmp;

            if (!Utilities.GetIntProperty(xTemplateNode, nameof(DoorCandidate).ToLower(), out iTmp, out message)) return false;
            DoorCandidate = iTmp;

            if (!Utilities.GetIntProperty(xTemplateNode, nameof(DoorPresent).ToLower(), out iTmp, out message)) return false;
            DoorPresent = iTmp;

            if (!Utilities.GetIntProperty(xTemplateNode, nameof(WindowCandidate).ToLower() , out iTmp, out message)) return false;
            WindowCandidate = iTmp;

            if (!Utilities.GetIntProperty(xTemplateNode, nameof(p1).ToLower(), out iTmp, out message)) return false;
            p1 = iTmp;

            if (!Utilities.GetIntProperty(xTemplateNode, nameof(p2).ToLower(), out iTmp, out message)) return false;
            p2 = iTmp;

            return true;
        }
    }
}
