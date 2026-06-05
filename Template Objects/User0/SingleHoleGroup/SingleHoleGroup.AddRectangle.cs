using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;
using ShapeTemplateLib;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SingleHoleGroup
    {
        public void AddRectangle(float StartX, float StartY, int Width, int Height)
        {
            Point2D p = new Point2D(StartX, StartY);
            BoundaryRectangle br = new BoundaryRectangle(Width, Height);

            _AddRect(p, br);

        }

        protected void _AddRect(Point2D Offset,BoundaryRectangle br)
        {
            List<BoundaryRectangle> brlist = RectangleArray.ToList();
            // Add the polygon to the list of polygons
            int NewIndex = brlist.Count;
            brlist.Add(br);
            RectangleArray = brlist.ToArray();

            /*
             * Now add the hole description to the hole array
             */
            List<LayoutHole> hl = oHoleGroup.HoleList.ToList();

            hl.Add(new LayoutHole()
            {
                HoleType = "rect",
                HoleTypeIndex = NewIndex,
                OffsetX = Offset.X,
                OffsetY = Offset.Y
            });

            oHoleGroup.HoleList = hl.ToArray();
        }
  
    }
}
