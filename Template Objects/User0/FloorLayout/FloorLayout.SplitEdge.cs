using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates;
using ShapeTemplateLib;
using ShapeTemplateLib.BasicShapes;
using ShapeTemplateLib.Templates.User0;
using System.Xml.Linq;

namespace ShapeTemplateLib.Templates.User0
{
    // Rename this floorwithrooms and delete the old template
    public partial class FloorLayout : TemplateBaseClass
    {



        /// <summary>
        /// This will split an edge, creating a hole. It will update all affected rooms
        /// </summary>
        /// <param name="oEdge"></param>
        /// <param name="Width"></param>
        /// <param name="Numerator"></param>
        /// <param name="Denominator"></param>
        /// <returns></returns>
        public void SplitEdge(FLEdge oEdge,int Width, int Numerator,int Denominator)
        {
            // we shouldn't see the same edge twice

            Tuple<Vertex, Vertex> currentEdges = oEdge.Vertices;

            // If width is >= the the edge return
            if (Width >= oEdge.EdgeLength) return;

            // Get the index of this edge
            int EdgeIndex = EdgeList.ToList().IndexOf(oEdge);

            // Clear the doorcandidate flag, we don't want this edge re-processed
            oEdge.DoorCandidate = 0;

            // Find the coordinates of the hole in this edge
            Tuple<Vertex, Vertex> hole = oEdge.GetHoleCoordinates(Width, Numerator, Denominator);

            List<Vertex> tmp = new List<Vertex>();
            tmp.Add(VertexList.Where(m => m.Index == oEdge.p1).FirstOrDefault());
            tmp.Add(hole.Item1);
            tmp.Add(hole.Item2);
            tmp.Add(VertexList.Where(m => m.Index == oEdge.p2).FirstOrDefault());

            // Add these two vertices
            Vertex f2 = new Vertex()
            {
                X = hole.Item1.X,
                Y = hole.Item1.Y,
                Index = NextVertexIndex()
            };

            var v = VertexList.ToList();
            v.Add(f2);
            VertexList = v.ToArray();

            // Create the new 'from' point on the second edge
            Vertex t1 = new Vertex()
            {
                X = hole.Item2.X,
                Y = hole.Item2.Y,
                Index = NextVertexIndex()
            };

            v = VertexList.ToList();
            v.Add(t1);
            VertexList = v.ToArray();   

            // add a new edge that will become the edge we split to. copy properties over.
            FLEdge oNewEdge = new FLEdge()
            {
                oFloor = oEdge.oFloor,
                Index=NextEdgeIndex(),
                p1 = t1.Index,
                p2 = oEdge.p2,
                IsExteriorEdge = oEdge.IsExteriorEdge,
                IsOpenSpaceEdge = oEdge.IsOpenSpaceEdge,
                DoorCandidate = oEdge.DoorCandidate,
                WindowCandidate = oEdge.WindowCandidate

            };

            // Add to the list
            var w = EdgeList.ToList();
            w.Add(oNewEdge);
            EdgeList = w.ToArray();
            
            // Adjust the coordinates of the existing edge 
            oEdge.p2 = f2.Index;

            Tuple<Vertex, Vertex> mod1 = oEdge.Vertices;

            /*
             * Now add the new edge to every room that contains the existing edge. we're not worried about
             * where the edge goes in the list of edges, this doesn't promise that things are end to end.
             * Later processing can collect the points for an edge and organize them clockwise etc. but 
             * since the same edges are shared we can't guarantee an order.
             */
            foreach (var room in RoomList)
            {
                if (room.EdgeIndexList.Contains(oEdge.Index))
                {
                    var el = room.EdgeIndexList.ToList();
                    el.Add(oNewEdge.Index);
                    room.EdgeIndexList = el.ToArray();
                }
            }
        }
    }
}
