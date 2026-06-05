using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorMaker
    {
        public string VertexListToString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach(var v in VertexList)
                {
                    sb.AppendLine($"[{v.Index}] {v.X},{v.Y}");
                }
                return sb.ToString();
            }
        }
    }
}
