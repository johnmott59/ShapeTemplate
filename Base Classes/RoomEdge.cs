using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ShapeTemplateLib
{

    public  partial class RoomEdge : ILoadAndSaveProperties
    {
        public bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;
            xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "point2d", "from", out message);
            if (xNode == null) return false;
            if (!From.LoadProperties(xNode, out message)) return false;
            xNode = Utilities.GetNamedElementWithPropAttribute(xTemplateNode, "point2d", "to", out message);
            if (xNode == null) return false;
            if (!To.LoadProperties(xNode, out message)) return false;

            if (!Utilities.GetIntProperty(xTemplateNode, "isexterioredge", out iTmp, out message)) return false;
            IsExteriorEdge = iTmp;

            if (!Utilities.GetIntProperty(xTemplateNode, "isopenspaceedge", out iTmp, out message)) return false;
            IsOpenSpaceEdge = iTmp;

            if (!Utilities.GetFloatProperty(xTemplateNode, "height", out fTmp, out message)) return false;
            Height = fTmp;

            if (!Utilities.GetFloatProperty(xTemplateNode, "width", out fTmp, out message)) return false;
            Width = fTmp;

            if (!Utilities.GetStringProperty(xTemplateNode, "id", out sTmp, out message)) return false;
            ID = sTmp;

            if (!Utilities.GetStringProperty(xTemplateNode, "holegroupid", out sTmp, out message)) return false;
            HoleGroupID = sTmp;

            return true;
        }

        public XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("roomedge", new XAttribute("prop", PropertyName));

            root.Add(From.GetProperties("from"));

            root.Add(To.GetProperties("to"));

            root.Add(new XElement("property", new XAttribute(nameof(IsExteriorEdge).ToLower(), IsExteriorEdge)));

            root.Add(new XElement("property", new XAttribute(nameof(IsOpenSpaceEdge).ToLower(), IsOpenSpaceEdge)));

            root.Add(new XElement("property", new XAttribute(nameof(Height).ToLower(), Height)));

            root.Add(new XElement("property", new XAttribute(nameof(Width).ToLower(), Width)));

            root.Add(new XElement("property", new XAttribute(nameof(ID).ToLower(), String.IsNullOrEmpty(ID) ? "" : ID)));

            root.Add(new XElement("property", new XAttribute(nameof(HoleGroupID).ToLower(), String.IsNullOrEmpty(HoleGroupID) ? "" : HoleGroupID)));

            return root;
        }

        public bool Equals(RoomEdge input)
        {
            return this.From.Equals(input.From) && this.To.Equals(input.To);
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

        public Point2D GetProportionalPoint(int numerator, int denominator)
        {
            var fraction = (float)numerator / (float)denominator;

            return GetProportionalPoint(fraction);
        }

        public Point2D GetProportionalPoint(float fraction)
        {
            return new Point2D()
            {
                X = From.X + fraction * (To.X - From.X),
                Y = From.Y + fraction * (To.Y - From.Y)
            };
        }
        /// <summary>
        /// FLip the from and to points, used to wind a set of edges so that they are from->to, from->to
        /// </summary>
        public void Flip()
        {
            Point2D tmp = this.From;
            this.From = this.To;
            this.To = tmp;
      
        }
        /// <summary>
        /// Check to see if a point 'c' is inside the line segment formed by a and b
        /// Algorithm from 
        /// https://stackoverflow.com/questions/328107/how-can-you-determine-a-point-is-between-two-other-points-on-a-line-segment
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool PointInSegment(Point2D a, Point2D b,Point2D c)
        {
            var crossproduct = (c.Y - a.Y) * (b.X - a.X) - (c.X - a.X) * (b.Y - a.Y);
            if (crossproduct != 0) return false;

            var dotproduct = (c.X - a.X) * (b.X - a.X) + (c.Y - a.Y) * (b.Y - a.Y);
            if (dotproduct < 0) return false;

            var squaredlengthba = (b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y);
            if (dotproduct > squaredlengthba) return false;

            return true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HoleSize">size of the hole</param>
        /// <param name="numerator">The fractional starting point of the hole, 1/2, 1/4 etc</param>
        /// <param name="denominator">The fractional starting point of the hole, 1/2, 1/4 etc</param>
        /// <returns></returns>
        public Tuple<Point2D, Point2D> GetHoleCoordinates(int HoleSize, int numerator, int denominator)
        {
            // If the hole size is >= the length of the edge return the edge

            if (HoleSize >= EdgeLength) return new Tuple<Point2D, Point2D>(From, To);

            // If the hole size is 0 return the point at this fraction

            if (HoleSize <= 0)
            {
                Point2D p = GetProportionalPoint(numerator, denominator);
                return new Tuple<Point2D, Point2D>(p, p);
            }

            var fraction = (float)numerator / (float)denominator;

            // what percentage of the length of this edge is this hole?

            var percentlength = (float)HoleSize / EdgeLength;

            // now we know the percentage. Get the point that is before and after this percentage length

            Point2D nf = GetProportionalPoint(fraction - percentlength);
            Point2D nt = GetProportionalPoint(fraction + percentlength);

            return new Tuple<Point2D, Point2D>(nf, nt);
        }

    }
}
