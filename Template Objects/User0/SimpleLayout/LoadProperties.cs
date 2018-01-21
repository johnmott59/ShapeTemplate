﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SimpleLayout 
    {
        public override bool LoadProperties(XElement ele, out string message)
        {
            float iTmp;
            message = "OK";

            // find each property
            XAttribute oAtt = Utilities.GetPropertyAttribute(ele, nameof(HorizontalScale));
            if (oAtt != null)
            {
                if (!Utilities.GetFloatFromAttribute(oAtt, out iTmp, out message)) return false;
                HorizontalScale = iTmp;
            }

            oAtt = Utilities.GetPropertyAttribute(ele, nameof(VerticalScale));
            if (oAtt != null)
            {
                if (!Utilities.GetFloatFromAttribute(oAtt, out iTmp, out message)) return false;
                VerticalScale = iTmp;
            }

            if (!LoadRectangleList(ele, out message)) return false;

            if (!LoadEllipseList(ele, out message)) return false;

            if (!LoadPolygonList(ele, out message)) return false;

            if (!LoadVertexList(ele, out message)) return false;

            if (!LoadSegmentList(ele, out message)) return false;

            if (!LoadHoleGroupList(ele, out message)) return false;

            if (!LoadHorizontalPanelList(ele, out message)) return false;

            return true;
        }
    }
}
