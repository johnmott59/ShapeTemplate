using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;
using ShapeTemplateLib;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SingleRoomBuilding : TemplateBaseClass
    {
        /// <summary>
        /// Default constructor for the single room building, create a reasonably shaped
        /// </summary>
        public SingleRoomBuilding()
        {
            HorizontalScale = 1;
            VerticalScale = 1;

            Width = 40;
            Height = 20;
            Length = 50;
            Thickness = 5;
            RoofOffset = -3;

            // Define a front door and window

            Door = new Hole();
            Door.Offset.X = 10;
            Door.Offset.Y = 0;
            Door.Boundary = new BoundaryRectangle(5, 10);

            FrontWindow = new Hole();
            FrontWindow.Offset.X = 18;
            FrontWindow.Offset.Y = 5;
            FrontWindow.Boundary = new BoundaryRectangle(15, 5);

            LeftWindow = new Hole();
            LeftWindow.Boundary = new BoundaryRoot();

            RearWindow = new Hole();
            RearWindow.Boundary = new BoundaryRoot();

            RightWindow = new Hole();
            RightWindow.Boundary = new BoundaryRoot();

        }
      


    }
}
