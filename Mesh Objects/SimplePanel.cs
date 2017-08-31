using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// a simple panel has a front, back connecting mesh and one or more holes

namespace ShapeTemplate.BasicShapes
{
    public class SimplePanel : IXElement, IMeshTemplate
    {
        /*
         * The front and back panels have boundaries and display properties
         */
        public class Panel : IXElement
        {
            public BoundaryRoot Boundary { get; set; } = new BoundaryRectangle() { Height = 20, Width = 20 };
            public MeshDisplayProperties MeshDisplayProperties { get; set; } = new MeshDisplayProperties();


            public XElement GetXElement()
            {
                throw new Exception("Not Implemented");
            }

            public XElement GetXElement(string parentName)
            {
                XElement parent = new XElement(parentName);
                parent.Add(Boundary.GetXElement());
                parent.Add(MeshDisplayProperties.GetXElement());

                return parent;
            }
        }

        public class Hole : IXElement
        {
            public Point3D Offset { get; set; } = new Point3D(0, 0, 0);
            public BoundaryRoot Boundary { get; set; }

            public XElement GetXElement()
            {
                XElement hole = new XElement("hole");
                hole.Add(Offset.GetXElement("offset"));

                hole.Add(Boundary.GetXElement());
                return hole;
            }

        }

        public class HoleManager : IXElement
        {
            /*
             * If there are holes in the panels and the panels are not in the XY plane their offset and relative X axis must be specified
             */
            public Point3D BackPanelOffset { get; set; }
            public Vector3D BackPanelXAxis { get; set; }

            public Point3D FrontPanelOffset { get; set; }
            public Vector3D FrontPanelXAxis { get; set; }

            // List of holes
            public List<Hole> HoleList { get; set; } = new List<Hole>();

            public XElement GetXElement()
            {
                if (HoleList.Count == 0) return new XElement("holes");

                XElement hm = new XElement("holes");
                hm.Add(BackPanelOffset.GetXElement("backpaneloffset"));
                hm.Add(BackPanelXAxis.GetXElement("backpanelxaxis"));
                hm.Add(FrontPanelOffset.GetXElement("frontpaneloffset"));
                hm.Add(FrontPanelXAxis.GetXElement("frontpanelxaxis"));

                foreach (Hole h in HoleList)
                {
                    hm.Add(h.GetXElement());
                }

                return hm;
            }
        }

        public Panel FrontPanel { get; set; } = new Panel();
        public Panel BackPanel { get; set; } = new Panel();

        public MeshDisplayProperties ConnectorDisplayProperties { get; set; } = new MeshDisplayProperties();
        public List<bool> ConnectorSegmentVisible { get; set; } = new List<bool>();

        // If there are holes their data is contained here
        public HoleManager HoleManagerObject { get; set; } = new SimplePanel.HoleManager();

        public XElement GetXElement()
        {
            XElement sp = new XElement("simplepanel");

            sp.Add(FrontPanel.GetXElement("frontpanel"));
            sp.Add(BackPanel.GetXElement("backpanel"));

            XElement conn = new XElement("connector");
            sp.Add(conn);
            conn.Add(ConnectorDisplayProperties.GetXElement());
            string bl = "";
            string comma = "";
            foreach (bool b in ConnectorSegmentVisible)
            {
                bl += comma + (b ? "1" : "0");
                comma = ",";
            }
            conn.Add(bl);

            sp.Add(HoleManagerObject.GetXElement());

            return sp;
        }
    }

}
