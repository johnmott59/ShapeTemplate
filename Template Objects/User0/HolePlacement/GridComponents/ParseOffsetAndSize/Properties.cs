using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{
    /*
     *  Version 3 design
     *  
     *  these are all the valid permutations on the width, with correlaries on the height
     *   
     *   offsetandsize=""  -- takes up entire grid cell

        offsetandsize="width=30" - centered width of 30, full height

        offsetandsize="width=30; left=20%"  - x offset is 20, full height and width 80%

        offsetandzize="width=30; right=20%"  - x offset is 0, full height and width 80%

        offsetandsize="left=20%; right=30%"  - x offset is 20%, width is remainder, full height

        offsetandsize="width=40; center=40%  - the center line of the box is at 40%

        offsetandsize="left=20%; center=50%  - box is 60% wide centered at 50%
              -same as width=60%;center=50%

        offsetandsize="right=40%; center=50%  - box is 20% wide 
        
     */
    public partial class HolePlacementTemplate
    {
        private enum eTagWithValue
        {
            Unrecognized,
            Width,
            Height,
            Left,
            Center,
            Right,
            Top,
            Middle,
            Bottom
        }

        public class DirectionalOffsetAndSize
        {
            public double Offset;
            public double Size;
        }

        private partial class ParseOffsetAndSize
        {        
            public double GridCellWidth { get; set; }
            public double GridCellHeight { get; set; }
            protected Dictionary<string, string> nvPairs { get; set; }
            public List<string> StatusMessageList { get; set; } = new List<string>();
            public List<Tuple<eTagWithValue, double>> TagValueList { get; set; } = new List<Tuple<eTagWithValue, double>>();

 
        }

    }
}
