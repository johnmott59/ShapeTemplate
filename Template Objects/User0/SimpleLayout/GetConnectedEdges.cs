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
        /// <summary>
        ///  This routine is used to collect together sets of edges that have a common point. Intuituvely
        ///  its to extract the polygons, although they may not be closed and they may be a graph. It was
        ///  written to do post processing after a call to flip a graph into hallways/roads, this will return the 
        ///  list of edges that make up the rooms/buildings
        /// </summary>
        /// <returns></returns>
        public List<List<Edge>> GetConnectedEdges()
        {
            List<List<Edge>> result = new List<List<Edge>>();
            // For each vertex get all edges that contain it

            List<int> ProcessedIndexValues = new List<int>();

            foreach(Vertex v in VertexList)
            {
                List<Edge> EdgeList = new List<Edge>();

                Recurse(EdgeList, ProcessedIndexValues, v.Index);

                if (EdgeList.Count > 0)
                {
                    result.Add(EdgeList);
                }
              
            }

            return result;
        }

        private void Recurse(List<Edge> AccrueList, List<int> ProcessedIndexValues, int CurrentIndex)
        {
            // If we have already processed this index value return
            if (ProcessedIndexValues.Contains(CurrentIndex)) return;

            ProcessedIndexValues.Add(CurrentIndex);

            // get all edges associated with this index
            List<Edge> tmp = EdgeList.Where(m => m.p1 == CurrentIndex || m.p2 == CurrentIndex).ToList();

            if (tmp == null) return;    // might happen, a vertex could not be associated with an edge

            // only add edges that are not on the list already. If this is a new addition recurse
            
            foreach (Edge e in tmp)
            {
                if (!AccrueList.Contains(e))
                {
                    AccrueList.Add(e);

                    Recurse(AccrueList, ProcessedIndexValues, e.p1);
                    
                    Recurse(AccrueList, ProcessedIndexValues, e.p2);
                    
                }
            }

        }

    }
}
