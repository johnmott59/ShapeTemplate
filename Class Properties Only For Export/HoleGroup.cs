using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class HoleGroup
    {
        public HoleGroup()
        {
            HoleGroupID = "";
            HoleList = new Hole[0];
        }
        public string HoleGroupID { get; set; }
        public Hole[] HoleList;
    }
}
