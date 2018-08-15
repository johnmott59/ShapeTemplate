using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{

    public  partial class StraightStairs 
    {
       
        public override bool LoadProperties(XElement ele, out string message)
        {
            int iTmp;
            message = "OK";

            // find each property
            XAttribute oAtt = Utilities.GetPropertyAttribute(ele,nameof(VerticalDistance));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt,  out iTmp, out message)) return false;
                VerticalDistance = iTmp;
            }

            XElement xLocalTransform = Utilities.GetNamedElementWithPropAttribute(ele, "matrix4x4", "localtransform");
            if (xLocalTransform != null)
            {
                if (!LocalTransform.LoadProperties(xLocalTransform, out message)) return false;
            }

            XElement xFrameOfReference = Utilities.GetNamedElementWithPropAttribute(ele, "frameofreference");
            //ele.Element("frameofreference");
            if (xFrameOfReference != null)
            {
                if (!oFrameOfReference.LoadProperties(xFrameOfReference, out message)) return false;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(HorizontalDistance));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
                HorizontalDistance = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(LateralDistance));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
                LateralDistance = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(StairCount));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
                StairCount = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(Width));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
                Width = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(Rise));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
                Rise = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(Run));
            if (oAtt != null)
            {
                if (!Utilities.GetIntFromAttribute(oAtt, out iTmp, out message)) return false;
                Run = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(LeftSideTexture));
            if (oAtt != null)
            {
                LeftSideTexture = oAtt.Value;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(RightSideTexture));
            if (oAtt != null)
            {
                RightSideTexture = oAtt.Value;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(StairTexture));
            if (oAtt != null)
            {
                StairTexture = oAtt.Value;
            }

            return true;
        }


    }

}
