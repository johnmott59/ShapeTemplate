using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// a simple panel has a front, back connecting mesh and one or more holes

namespace ShapeTemplateLib.BasicShapes
{
    /*
     * A panel has a boundary, mesh properties, an relative XAxis, an offset, and possibly holes.
     * For purposes of placing holes a panel is defined in the XY plane. The winding of the boundary determines 
     * the relative Z axis, counter clockwise winding indicates that Z is 'out' or towards the viewer looking at the 
     * panel.
     */
    public partial class FlatMesh : ILoadAndSaveProperties
    {
        public BoundaryRoot Boundary { get; set; } = new BoundaryRectangle() { Height = 20, Width = 20 };
        public MeshDisplayProperties MeshDisplayProperties { get; set; } = new MeshDisplayProperties();

        public Point3D Offset { get; set; } = new Point3D(0, 0, 0);
        public Vector3D XAxis { get; set; } = new Vector3D(1, 0, 0);

        // List of holes
        public List<Hole> HoleList { get; set; } = new List<Hole>();

        public XElement GetProperties()
        {
            throw new Exception("Not Implemented");
        }

        // For a basic shape the compile step is the same as getting properties
        public XElement Compile()
        {
            return GetProperties();
        }

        // For a basic shape the compile step is the same as getting properties
        public XElement Compile(string ParentName)
        {
            return GetProperties(ParentName);
        }

        public XElement GetProperties(string parentName)
        {
            XElement parent = new XElement(parentName);
            parent.Add(Boundary.GetProperties());
            if (MeshDisplayProperties != null) parent.Add(MeshDisplayProperties.GetProperties());
            if (Offset == null)
            {
                // add a default value
                parent.Add(new Point3D(0, 0, 0).GetProperties());
            } else
            {
                parent.Add(Offset.GetProperties("offset"));
            }

            if (XAxis == null)
            {
                // Add a default value
                parent.Add(new Vector3D(1f, 0f, 0f).GetProperties("xaxis"));
            } else
            {
                parent.Add(XAxis.GetProperties("xaxis"));
            }
          
            /*
             * Add the list of panel holes
             */
            XElement xHole = new XElement("holelist");
            parent.Add(xHole);
            foreach (Hole h in HoleList)
            {
                xHole.Add(h.GetProperties());
            }

            return parent;
        }

        public bool LoadProperties(XElement ele, out string message)
        {
            if (ele == null)
            {
                message = $"Missing panel element";
                return false;
            }

            message = "OK";
            BoundaryRoot b;
            if (!BoundaryRoot.LoadXElement(ele.Element("boundary"), out b, out message)) return false;
            this.Boundary = b;

            if (ele.Element("displayproperties") != null)
            {
                if (!MeshDisplayProperties.LoadProperties(ele.Element("displayproperties"), out message)) return false;
            }

            if (ele.Element("offset") != null)
            {
                if (!Offset.LoadProperties(ele.Element("offset"), out message)) return false;
            }

            if (ele.Element("xaxis") != null)
            {
                if (!XAxis.LoadProperties(ele.Element("xaxis"), out message)) return false;
            }
            /*
             * If there are holes, add them
             */
            if (ele.Element("holelist") != null)
            {
                foreach (XElement xh in ele.Element("holelist").Elements("hole"))
                {
                    Hole h = new Hole();
                    if (!h.LoadProperties(xh, out message)) return false;
                    HoleList.Add(h);
                }
            }

            return true;
        }
}

}
    
