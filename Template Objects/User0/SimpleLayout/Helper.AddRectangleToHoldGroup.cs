using System;
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
        /// <summary>
        /// Helper routine to add a rectangle to a named hole group.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="OffsetX"></param>
        /// <param name="OffsetY"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public void AddRectangleToHoleGroup(string name,float OffsetX,float OffsetY,float Width, float Height)
        {
            // See if this hole group exists


            HoleGroup hg = HoleGroupList.Where(m => m.HoleGroupID == name).FirstOrDefault();
            if (hg == null)
            {
                hg = new HoleGroup() { HoleGroupID = name };
                HoleGroupList.Add(hg);
            }

            // add a new rectangle
            BoundaryRectangleList.Add(new BoundaryRectangle() { BoundaryType = "rect", Height = Height, Width = Width, ZDepth = 0 });
            int index = BoundaryRectangleList.Count - 1;

            // add this to the hole groupd
            List<LayoutHole> list = hg.HoleList.ToList();
            list.Add(new LayoutHole() { HoleType = "rect", HoleTypeIndex = index, OffsetX = OffsetX, OffsetY = OffsetY });
            hg.HoleList = list.ToArray();

            return;
        }

   
    }
}
