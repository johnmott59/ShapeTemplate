using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{

    // An edge is a from and to point, possibly a list of holes, a width and a height
    public partial class Edge
    {
        public string ID { get; set; } = "";
        public string HoleGroupID { get; set; } = "";
        public int p1 { get; set; }
        public int p2 { get; set; }
        public int Width { get; set; } = 5;
        public int Height { get; set; } = 20;

        // The CopyFrom method is used in Javascript to convert a version of the object retrieved via JSON
        // into a full object of this type. An object retrieved via JSON.parse() will have the correct property
        // names but not be an object of this type, this will take that property-only version and create a full
        // object
         public static Edge CopyFrom(Edge oSource)
        {
            Edge e = new Edge();
            e.ID = oSource.ID;
            e.HoleGroupID = oSource.HoleGroupID;
            e.p1 = oSource.p1;
            e.p2 = oSource.p2;
            e.Width = oSource.Width;
            e.Height = oSource.Height;

            return e;
        }


    }

    
    
}
