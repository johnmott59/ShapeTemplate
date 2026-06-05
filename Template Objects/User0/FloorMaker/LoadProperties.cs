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

        public override bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;
            if (!LoadVertexList(xTemplateNode, out message)) return false;
            if (!LoadEdgeList(xTemplateNode, out message)) return false;
            if (!LoadRoomList(xTemplateNode, out message)) return false;

            return true;
        }


        protected bool LoadVertexList(XElement ele, out string message)
        {
            message = "OK";

            XElement XVertexList = Utilities.GetListElement(ele, "vertexlist");

            // an empty layout would have no vertices.
            if (XVertexList == null) return true;

            List<Vertex> list = new List<Vertex>();
            foreach (XElement XVertex in XVertexList.Elements("vertex"))
            {
                int index;
                float x, y;


                if (!Utilities.GetIntAttribute(XVertex, "index", out index, out message)) return false;

                if (!Utilities.GetFloatAttribute(XVertex, "x", out x, out message)) return false;

                if (!Utilities.GetFloatAttribute(XVertex, "y", out y, out message)) return false;

                list.Add(new Vertex() { Index = index, X = x, Y = y });
            }

            VertexList = list.ToArray();
            return true;
        }


        public bool LoadEdgeList(XElement Xele, out string message)
        {
            message = "OK";

            XElement XList = Utilities.GetListElement(Xele, "edgelist");

            // ok if there are no items
            if (XList == null) return true;

            List<FMEdge> list = new List<FMEdge>();
            foreach (XElement x in XList.Elements("fledge"))
            {
                FMEdge o = new FMEdge() { oFloor = this };
                if (!o.LoadProperties(x, out message)) return false;
                list.Add(o);
            }

            EdgeList = list.ToArray();

            return true;
        }

        public bool LoadRoomList(XElement Xele, out string message)
        {
            message = "OK";

            // Use the same name as GetProperties - "assembledroomlist"
            XElement XList = Utilities.GetListElement(Xele, nameof(AssembledRoomList).ToLower());

            // ok if there are no items
            if (XList == null) return true;

            List<FMAssembledRoom> list = new List<FMAssembledRoom>();
            foreach (XElement x in XList.Elements("fmassembledroom"))
            {
                FMAssembledRoom o = new FMAssembledRoom() { oFloor = this };
                if (!o.LoadProperties(x, out message)) return false;
                list.Add(o);
            }

            AssembledRoomList = list.ToArray();

            return true;
        }
    }
}
