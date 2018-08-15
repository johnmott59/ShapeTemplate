using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{

    public partial class BoundaryEllipse : BoundaryRoot
    {

        public BoundaryEllipse(float Width, float Height,float ZDepth = 0) : this()
        {
            this.Width = Width;
            this.Height = Height;
            this.ZDepth = ZDepth;
        }

        public override XElement GetProperties(string PropertyName = "")
        {
            return new XElement("boundaryellipse", new XAttribute("prop",PropertyName), 
                    new XAttribute(nameof(Width).ToLower(), Width), 
                    new XAttribute(nameof(Height).ToLower(), Height),
                    new XAttribute(nameof(ZDepth).ToLower(), ZDepth)
                );
        }
        /// <summary>
        /// This load routine is called by boundary root, the boundary element has already been processed
        /// </summary>
        /// <param name="ele"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public override bool LoadProperties(XElement ele, out string message)
        {
            message = "OK";

            float value = 0;
            if (!Utilities.GetFloatAttribute(ele, nameof(Width).ToLower(), out value, out message)) return false;
            Width = value;

            value = 0;
            if (!Utilities.GetFloatAttribute(ele, nameof(Height).ToLower(), out value, out message)) return false;
            Height = value;

            value = 0;
            if (!Utilities.GetFloatAttribute(ele, nameof(ZDepth).ToLower(), out value, out message)) return false;
            ZDepth = value;

            return true;
        }

        /// <summary>
        /// If a boundaryroot object is a child of another class then loading from JSON requires an additional step
        /// because Deserialize won't know how to extract the object. Instead we collect the properties that are
        /// for the specific boundary type and extract them here.
        /// </summary>
        /// <param name="rectangleProperties"></param>
        /// <returns></returns>
        public static BoundaryEllipse LoadFromDictionary(Dictionary<string, object> rectangleProperties)
        {
            BoundaryEllipse br = new BoundaryEllipse();
            br.Width = (float)Convert.ToDouble(rectangleProperties["Width"]);
            br.Height = (float)Convert.ToDouble(rectangleProperties["Height"]);
            br.ZDepth = (float)Convert.ToDouble(rectangleProperties["ZDepth"]);
            return br;

        }

        // Get a list of 2D points for this rectangle. The class is defined in 3D but we use it in 2D situations
        public override List<PointF> GetPoints2D(float OffsetX, float OffsetY)
        {
            List<PointF> PointList = new List<PointF>();

            for (int i = 0; i <= 360; i += 9)
            {
                int angle = i >= 360 ? i - 360 : i;

                float radians = (float)angle * .0174533f;

                PointList.Add(new PointF()
                {
                    X = OffsetX + Width * (float)Math.Cos(radians),
                    Y = OffsetY + Height * (float)Math.Sin(radians)
                });
            }

            return PointList;

        }
    }




}