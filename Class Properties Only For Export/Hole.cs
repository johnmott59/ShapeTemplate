using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{

    public partial class Hole
    {
        // The starting offset of the hole
        public float OffsetX { get; set; }
        public float OffsetY { get; set; }

        // outline of the hole
        public string HoleType { get; set; }
        public int HoleTypeIndex { get; set; }

        public Hole()
        {

        }

        public static Hole CopyFrom(Hole oFrom)
        {
            Hole oHole = new Hole();
            oHole.OffsetX = oFrom.OffsetX;
            oHole.OffsetY = oFrom.OffsetY;

            oHole.HoleType = oFrom.HoleType;
            oHole.HoleTypeIndex = oFrom.HoleTypeIndex;

            return oHole;
        }
    }

}
