using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class HorizontalPanel
    {
        public HorizontalPanel()
        {
            Height = 0;
            Thickness = 5;
            DescriptorList = new string[0];
            HoleGroupID = "";
        }
        public float Height { get; set; }
        public float Thickness { get; set; }
        public string[] DescriptorList { get; set; }
        public string HoleGroupID { get; set; }
    }
}
