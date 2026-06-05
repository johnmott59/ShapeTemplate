using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{

    public partial class HolePlacementTemplate
    {
       

        private partial class ParseOffsetAndSize
        {        


            /// <summary>
            /// Get the width. 
            /// </summary>
            /// <returns></returns>
            public DirectionalOffsetAndSize GetHeight()
            {
                // extract all of the height tags

                double? dTop = TagValueList.Where(m => m.Item1 == eTagWithValue.Top).FirstOrDefault()?.Item2;

                double? dBottom = TagValueList.Where(m => m.Item1 == eTagWithValue.Bottom).FirstOrDefault()?.Item2;

                double? dHeight = TagValueList.Where(m => m.Item1 == eTagWithValue.Height).FirstOrDefault()?.Item2;

                double? dMiddle = TagValueList.Where(m => m.Item1 == eTagWithValue.Middle).FirstOrDefault()?.Item2;

                return GetHeight(GridCellHeight, dHeight, dBottom, dMiddle, dTop);

            }

            protected DirectionalOffsetAndSize GetHeight(double GridCellHeight, double? Height, double? Bottom, double? Middle, double? Top)
            {

                if (Top != null)
                {
                    Top = GridCellHeight - Top.Value;
                }

                // case 1: If all 4 parameters are null the width is the width of the cell and the offset is 0
                if (Height == null && Bottom == null && Middle == null && Top == null)
                {
                    return new DirectionalOffsetAndSize() { Offset = 0, Size = GridCellHeight };
                }

                // case 2: only height is specified. this is a centered height
                if (Height != null && Bottom == null && Middle == null && Top == null)
                {
                    Middle = GridCellHeight / 2;
                    return new DirectionalOffsetAndSize() { Offset = Middle.Value - Height.Value / 2, Size = Height.Value };
                }

                // case 3: height and bottom are specified
                if (Height != null && Bottom != null && Middle == null && Top == null)
                {
                    return new DirectionalOffsetAndSize() { Offset = Bottom.Value, Size = Height.Value };
                }

                // case 4: height and top  are specified
                if (Height != null && Bottom == null && Middle == null && Top != null)
                {
                    return new DirectionalOffsetAndSize() { Offset = Top.Value - Height.Value, Size = Height.Value };
                }

                // case 5: bottom and top  are specified
                if (Height == null && Bottom != null && Middle == null && Top != null)
                {
                    return new DirectionalOffsetAndSize() { Offset = Bottom.Value, Size = Top.Value - Bottom.Value };
                }

                // case 6: height and middle are specified.
                if (Height != null && Bottom == null && Middle != null && Top == null)
                {
                    Bottom = Middle.Value - Height.Value / 2;
                    return new DirectionalOffsetAndSize() { Offset = Bottom.Value, Size = Height.Value };
                }

                // case 7: bottom and middle are specified.
                if (Height == null && Bottom != null && Middle != null && Top == null)
                {
                    Height = (Middle.Value - Bottom.Value) * 2;
                    return new DirectionalOffsetAndSize() { Offset = Bottom.Value, Size = Height.Value };
                }

                // case 8: middle and top are specified.
                if (Height == null && Bottom == null && Middle != null && Top != null)
                {
                    Height = (Top.Value - Middle.Value) * 2;
                    return new DirectionalOffsetAndSize() { Offset = Top.Value - Height.Value, Size = Height.Value };
                }

                return new DirectionalOffsetAndSize();
            }

        }

    }
}
