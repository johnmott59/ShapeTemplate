using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SimpleLayout 
    {
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

        protected bool LoadVertexList(XElement ele, out string message)
        {
            message = "OK";

            XElement XVertexList = Utilities.GetListElement(ele, "vertexlist");

            // an empty layout would have no vertices.
            if (XVertexList == null) return true;

            foreach (XElement XVertex in XVertexList.Elements("vertex"))
            {
                int index;
                float x, y;


                if (!Utilities.GetIntAttribute(XVertex, "index", out index, out message)) return false;

                if (!Utilities.GetFloatAttribute(XVertex, "x", out x, out message)) return false;

                if (!Utilities.GetFloatAttribute(XVertex, "y", out y, out message)) return false;

                VertexList.Add(new Vertex() { Index = index, X = x, Y = y });
            }

            return true;
        }
    }
}
