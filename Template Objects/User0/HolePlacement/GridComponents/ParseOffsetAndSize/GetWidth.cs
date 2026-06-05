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
            public DirectionalOffsetAndSize GetWidth()
            {
                // extract all of the width tags

                double? dLeft = TagValueList.Where(m => m.Item1 == eTagWithValue.Left).FirstOrDefault()?.Item2;

                double? dRight = TagValueList.Where(m => m.Item1 == eTagWithValue.Right).FirstOrDefault()?.Item2;

                double? dWidth = TagValueList.Where(m => m.Item1 == eTagWithValue.Width).FirstOrDefault()?.Item2;

                double? dCenter = TagValueList.Where(m => m.Item1 == eTagWithValue.Center).FirstOrDefault()?.Item2;

                return GetWidth(GridCellWidth, dWidth, dLeft, dCenter, dRight);

             }

            protected DirectionalOffsetAndSize GetWidth(double GridCellWidth, double? Width, double? Left, double? Center, double? Right)
            {

                if (Right != null)
                {
                    Right = GridCellWidth - Right.Value;
                }

                // case 1: If all 4 parameters are null the width is the width of the cell and the offset is 0
                if (Width == null && Left == null && Center == null && Right == null)
                {
                    return new DirectionalOffsetAndSize() { Offset = 0, Size = GridCellWidth };
                }

                // case 2: only width is specified. this is a centered width
                if (Width != null && Left == null && Center == null && Right == null)
                {
                    Center = GridCellWidth / 2;
                    return new DirectionalOffsetAndSize() { Offset = Center.Value - Width.Value / 2, Size = Width.Value };
                }

                // case 3: width and left are specified
                if (Width != null && Left != null && Center == null && Right == null)
                {
                    return new DirectionalOffsetAndSize() { Offset = Left.Value, Size = Width.Value };
                }

                // case 4: width and right  are specified
                if (Width != null && Left == null && Center == null && Right != null)
                {
                    return new DirectionalOffsetAndSize() { Offset = Right.Value - Width.Value, Size = Width.Value };
                }

                // case 5: left and right  are specified
                if (Width == null && Left != null && Center == null && Right != null)
                {
                    return new DirectionalOffsetAndSize() { Offset = Left.Value, Size = Right.Value - Left.Value };
                }

                // case 6: width and center are specified.
                if (Width != null && Left == null && Center != null && Right == null)
                {
                    Left = Center.Value - Width.Value / 2;
                    return new DirectionalOffsetAndSize() { Offset = Left.Value, Size = Width.Value };
                }

                // case 7: left and center are specified.
                if (Width == null && Left != null && Center != null && Right == null)
                {
                    Width = (Center.Value - Left.Value) * 2;
                    return new DirectionalOffsetAndSize() { Offset = Left.Value, Size = Width.Value };
                }

                // case 8: right and center are specified.
                if (Width == null && Left == null && Center != null && Right != null)
                {
                    Width = (Right.Value - Center.Value) * 2;
                    return new DirectionalOffsetAndSize() { Offset = Right.Value - Width.Value, Size = Width.Value };
                }

                return new DirectionalOffsetAndSize();
            }


        }

    }
}
