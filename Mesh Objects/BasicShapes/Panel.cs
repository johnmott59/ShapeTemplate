using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// a simple panel has a front, back connecting mesh and one or more holes

namespace ShapeTemplateLib.BasicShapes
{
    public partial class Panel : ILoadAndSaveProperties, ICompile
    {
        public FlatMesh FrontMesh { get; set; } = new FlatMesh();
        public FlatMesh BackMesh { get; set; } = new FlatMesh();

        public MeshDisplayProperties ConnectorDisplayProperties { get; set; } = new MeshDisplayProperties();
        public List<bool> ConnectorSegmentVisible { get; set; } = new List<bool>();

        // List of the connections for holes from front to back
        public List<Panel.ConnectedHole> ConnectedHoleList { get; set; } = new List<ConnectedHole>();

        public bool LoadProperties(XElement ele, out string message)
        {
            message = "OK";

            XElement xp;
            if (!FrontMesh.LoadProperties(ele.Element("frontmesh"), out message)) return false;

            if (!BackMesh.LoadProperties(ele.Element("backmesh"), out message)) return false;

            // the connector is optional
            xp = ele.Element("connector");
            if (xp != null)
            {
                // The display properties are optional
                if (xp.Element("displayproperties") != null)
                {
                    if (!ConnectorDisplayProperties.LoadProperties(xp.Element("displayproperties"), out message)) return false;
                }
                /*
                 * Get the value, should be a series of 0 and 1
                 */
                string value = xp.Value.Trim();

                if (!String.IsNullOrEmpty(value))
                {
                    string[] list = value.Split(",".ToCharArray());
                    foreach (string s in list)
                    {
                        switch (s)
                        {
                            case "0":
                                ConnectorSegmentVisible.Add(false);
                                break;
                            case "1":
                                ConnectorSegmentVisible.Add(true);
                                break;
                            default:
                                message = $"Expected 0 or 1 for connector visibility, got {s}";
                                return false;
                        }
                    }
                }
            }
            /*
             * If there are holes, add them
             */
            if (ele.Element("connectedholelist") != null)
            {
                foreach (XElement xh in ele.Element("connectedholelist").Elements("connectedhole"))
                {
                    ConnectedHole h = new ConnectedHole();
                    if (!h.LoadProperties(xh, out message)) return false;

                    // Make sure the holes exist and that there is only one of them
                    if (FrontMesh.HoleList.Where(m=>m.ID == h.FrontID).Count() != 1)
                    {
                        message = "Front panel hole ID not found or is not unique";
                        return false;
                    }
                   
                    if (BackMesh.HoleList.Where(m => m.ID == h.BackID).Count() != 1)
                    {
                        message = "Back panel hole ID not found or is not unique";
                        return false;
                    }

                    ConnectedHoleList.Add(h);
                }
            }

            return true;
        }

        public XElement GetProperties()
        {
            XElement sp = new XElement("panel");

            sp.Add(FrontMesh.GetProperties("frontmesh"));
            sp.Add(BackMesh.GetProperties("backmesh"));

            XElement conn = new XElement("connector");
            sp.Add(conn);
            conn.Add(ConnectorDisplayProperties.GetProperties());
            string bl = "";
            string comma = "";
            foreach (bool b in ConnectorSegmentVisible)
            {
                bl += comma + (b ? "1" : "0");
                comma = ",";
            }
            conn.Add(bl);

            // Save the connected holes
            XElement xHole = new XElement("connectedholelist");
            sp.Add(xHole);
            foreach(ConnectedHole h in ConnectedHoleList)
            {
                xHole.Add(h.GetProperties());
            }
            return sp;
        }
        /*
         * For a basic shape the compile step is the same as the set of properties
         */
        public XElement Compile()
        {
            return GetProperties();
        }
    }
}
