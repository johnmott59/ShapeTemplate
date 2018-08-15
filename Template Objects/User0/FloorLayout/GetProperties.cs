using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class FloorLayout
    {
        public override XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("template",
          new XAttribute("prop", PropertyName),
          new XAttribute("user", "User0"),
          new XAttribute("name", "FloorLayout".ToLower()));

            XElement XVertexList = new XElement("list", new XAttribute("name", nameof(VertexList).ToLower()));
            root.Add(XVertexList);
            XVertexList.Add(GetVertexList().Elements());

            XElement XEdgeList = new XElement("list", new XAttribute("name", nameof(EdgeList).ToLower()));
            root.Add(XEdgeList);
            XEdgeList.Add(GetEdgeList().Elements());


            XElement XRoomList = new XElement("list", new XAttribute("name", nameof(RoomList).ToLower()));
            root.Add(XRoomList);
            XRoomList.Add(GetRoomList().Elements());

            return root;
        }

        //--------------------------------
        protected XElement GetRoomList()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("roomlist");

            foreach (FLRoom r in RoomList)
            {
                XList.Add(r.GetProperties());
            }

            return XList;
        }

        //--------------------------------
        protected XElement GetVertexList()
        {
            // Add the vertices and the edges
            XElement XVertexList = new XElement("vertexlist");
            foreach (Vertex v in VertexList)
            {
                XVertexList.Add(new XElement("vertex",
                        new XAttribute("index", v.Index),
                        new XAttribute("x", v.X),
                        new XAttribute("y", v.Y))
                        );
            }

            return XVertexList;
        }


        //--------------------------------
        protected XElement GetEdgeList()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("edgelist");

            foreach (FLEdge r in EdgeList)
            {
                XList.Add(r.GetProperties());
            }

            return XList;
        }


    }
}

