using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    public partial class RoomEdgeGroup
    {
        public RoomEdgeGroup()
        {
            RoomEdgeList = new RoomEdge[0];
        }

        public RoomEdge[] RoomEdgeList { get; set; }

        public XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("roomedgegroup", new XAttribute("prop", PropertyName));

            XElement XRoomEdgeList = new XElement("list", new XAttribute("name", nameof(RoomEdgeList).ToLower()));
            root.Add(XRoomEdgeList);
            XRoomEdgeList.Add(GetRoomEdgeList().Elements());

            return root;
        }

        //--------------------------------
        protected XElement GetRoomEdgeList()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("roomedgelist");

            foreach (RoomEdge r in RoomEdgeList)
            {
                XList.Add(r.GetProperties());
            }

            return XList;
        }

        public bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;

            if (!LoadRoomEdgeList(xTemplateNode, out message)) return false;

            return true;
        }
        public bool LoadRoomEdgeList(XElement Xele, out string message)
        {
            message = "OK";

            XElement XList = Utilities.GetListElement(Xele, "roomedgelist");

            // ok if there are no items
            if (XList == null) return true;

            List<RoomEdge> list = new List<RoomEdge>();
            foreach (XElement x in XList.Elements("roomedge"))
            {
                RoomEdge o = new RoomEdge();
                if (!o.LoadProperties(x, out message)) return false;
                list.Add(o);
            }

            RoomEdgeList = list.ToArray();

            return true;
        }
    }
}
