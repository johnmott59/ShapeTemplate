using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    public partial class RoomEdge
    {
        public RoomEdge()
        {
            From = new Point2D();
            To = new Point2D();
        }

        public Point2D From { get; set; }
        public Point2D To { get; set; }

        // These flags will be used to tell us whether this edge is an exterior wall or is next to open space
        public int IsExteriorEdge { get; set; }
        public int IsOpenSpaceEdge { get; set; }

        public float Width { get; set; }
        public float Height { get; set; }

        // The ID value is passed to the simple layout and is a way to identify this wall
        public string ID { get; set; }

        // There may be a group of holes defined for this edge defined at the floor level. The ID
        // field will be used to link them
        public string HoleGroupID { get; set; }
    }
}
