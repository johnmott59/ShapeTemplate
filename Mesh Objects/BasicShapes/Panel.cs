using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// a simple panel has a front, back connecting mesh and one or more holes

namespace ShapeTemplateLib.BasicShapes
{
    /// <summary>
    /// The Panel is a basic shape consisting of two FlatMesh shapes that are a 'front' and 'back' and that are 
    /// connected via a connector mesh. The front and back meshes can have holes that don't connect to each other,
    /// and there can be holes that connect the front and back, so its a very versatile shape.
    /// </summary>
    [HelpItem(eItemFlavor.BasicShape,"panel")]
    public partial class Panel : TransformableRoot
    {

        [HelpProperty("frontmesh")]
        public FlatMesh FrontMesh { get; set; } = new FlatMesh();
        [HelpProperty("backmesh")]
        public FlatMesh BackMesh { get; set; } = new FlatMesh();


        /// <summary>
        /// The connector display properties has the display properties for the mesh that connectes the front and back
        /// </summary>
        [HelpProperty("")]
        public MeshDisplayProperties ConnectorDisplayProperties { get; set; } = new MeshDisplayProperties();

        /// <summary>
        ///  You can turn on or off individual segments of the connection mesh for different effects
        /// </summary>
        [HelpProperty("")]
        public List<bool> ConnectorSegmentVisible { get; set; } = new List<bool>();

        // List of the connections for holes from front to back
        [HelpProperty("connectedholelist")]
        public List<Panel.ConnectedHole> ConnectedHoleList { get; set; } = new List<ConnectedHole>();

        public override bool LoadProperties(XElement ele, out string message)
        {
            message = "OK";

            XElement xLocalTransform = Utilities.GetNamedElementWithPropAttribute(ele, "matrix4x4", "localtransform");
            if (xLocalTransform != null)
            {
                if (!LocalTransform.LoadProperties(xLocalTransform, out message)) return false;
            }

            XElement xFrameOfReference = Utilities.GetNamedElementWithPropAttribute(ele, "frameofreference");
            //ele.Element("frameofreference");
            if (xFrameOfReference != null)
            {
                if (!oFrameOfReference.LoadProperties(xFrameOfReference, out message)) return false;
            }

            XElement xFrontMesh = Utilities.GetNamedElementWithPropAttribute(ele, "flatmesh", "FrontMesh");          
            if (!FrontMesh.LoadProperties(xFrontMesh, out message)) return false;

            XElement xBackMesh = Utilities.GetNamedElementWithPropAttribute(ele, "flatmesh", "BackMesh");
            if (!BackMesh.LoadProperties(xBackMesh, out message)) return false;
            /*
             * The connector segment is optional
             */
            XElement xDisplayProperties = Utilities.GetNamedElementWithPropAttribute(ele, "meshdisplayproperties", "ConnectorDisplayProperties");
            // The display properties are optional
            if (xDisplayProperties != null)
            {
                if (!ConnectorDisplayProperties.LoadProperties(xDisplayProperties, out message)) return false;
            }
            /*
             * There is an optional list of segment visibility variables
             */
            XElement xVisibleProperties = ele.Element("connectorsegmentvisible");
            if (xVisibleProperties != null)
            {
                string value = xVisibleProperties.Value.Trim();

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
            XElement xConnectedHoles = Utilities.GetNamedElementWithPropAttribute(ele, "holelist","ConnectedHoleList");
            if (xConnectedHoles != null)
            {
                foreach (XElement xh in xConnectedHoles.Elements("connectedhole"))
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

        public override XElement GetProperties(string PropertyName="")
        {
            XElement sp = new XElement("panel",new XAttribute("prop",PropertyName));

            if (LocalTransform != null)
            {
                sp.Add(LocalTransform.GetProperties("localtransform"));
            }

            if (oFrameOfReference != null)
            {
                sp.Add(oFrameOfReference.GetProperties("frameofreference"));
            }


            sp.Add(FrontMesh.GetProperties("FrontMesh"));
            sp.Add(BackMesh.GetProperties("BackMesh"));


            sp.Add(ConnectorDisplayProperties.GetProperties("ConnectorDisplayProperties"));

            string bl = "";
            string comma = "";
            foreach (bool b in ConnectorSegmentVisible)
            {
                bl += comma + (b ? "1" : "0");
                comma = ",";
            }
            /*
             * Add in the connector segment visible list
             */
            sp.Add(new XElement("connectorsegmentvisible", bl));
         

            // Save the connected holes
            XElement xHole = new XElement("holelist",new XAttribute("prop","ConnectedHoleList"));
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
        public override XElement Compile()
        {
            return GetProperties();
        }
    }
}
