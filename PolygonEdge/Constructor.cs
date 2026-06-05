using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib;

namespace ShapeTemplateLib
{
    public partial class PolygonEdge
    {
        public PolygonEdge()
        {
        }

        public PolygonEdge(PolygonEdge pe)
        {
            this.From = new PointF(pe.From.X,pe.From.Y);
            this.To = new PointF(pe.To.X, pe.To.Y);

            this.Width = pe.Width;
            this.Height = pe.Height;
            this.ID = pe.ID;
            this.HoleGroupID = pe.HoleGroupID;
        }

        public PolygonEdge(PointF From, PointF To)
        {
            this.From = From;
            this.To = To;
            this.Width = 10;
            this.Height = 30;
            this.ID = "";
        }

        public PolygonEdge(LineSegment ls)
        {
            this.From = new PointF() { X = ls.From.X, Y = ls.From.Y };
            this.To = new PointF() { X = ls.To.X, Y = ls.To.Y };
            this.Width = ls.Thickness;
        }
    }
}
