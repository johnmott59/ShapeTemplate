using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public partial class BoundaryRoot 
    {
        // This value is set by the subclass
        public string BoundaryType { get; set; } = "";

        public BoundaryRoot()
        {
            this.BoundaryType = "";
        }

        public static BoundaryRoot CopyFrom(BoundaryRoot oFrom)
        {
            switch (oFrom.BoundaryType)
            {
                case "rectangle":
                    return BoundaryRectangle.CopyFrom((BoundaryRectangle)oFrom);
                case "ellipse":
                    return BoundaryEllipse.CopyFrom((BoundaryEllipse)oFrom);
                case "polygon":
                    return BoundaryPolygon.CopyFrom((BoundaryPolygon)oFrom);
            }
            return new BoundaryRoot();
        }

 
    }
}