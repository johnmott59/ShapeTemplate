using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {


        // After we do an intersection make a note about whether or not the segments
        // are inside the outline or the open area.

        public bool InsideOutline { get; set; } = false;
        public bool InsideOpenArea { get; set; } = false;
        public bool IsInteriorWallSection { get; set; } = false;

        public int RoomCount { get; set; } = 0;    // how many rooms does this edge belong to. used in step 7

        public PointF From { get; set; }
        public PointF To { get; set; }

        public string ID { get; set; } = "";        // string that can be used to keep track of this edge

        public string HoleGroupID { get; set; } = "";    // Id of the hole group for this edge

        // The polygon edge list is used to build a graph from a layout. This field will be used to keep track of the index of this edge from the layout
        // as the graph process will mix up the edges.
        public int LayoutEdgeIndex { get; set; } = -1;

        // For purposes of a room layout store the width and height
        public float Width { get; set; } = 10;
        public float Height { get; set; } = 30;

        public PointF CenterPoint
        {
            get
            {
                float dx = To.X - From.X;
                float dy = To.Y - From.Y;

                /*
                 * The connection point for this edge is the normal
                 */
                return new PointF()
                {
                    X = (dx) / 2 + From.X,
                    Y = (dy) / 2 + From.Y
                };
            }
        }
        /// <summary>
        /// Get the length of this edge
        /// </summary>
        public float EdgeLength
        {
            get
            {
                var x2 = (To.X - From.X) * (To.X - From.X);
                var y2 = (To.Y - From.Y) * (To.Y - From.Y);

                return (float)Math.Sqrt(x2 + y2);
            }
        }

    }
}
