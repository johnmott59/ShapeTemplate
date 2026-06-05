
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;
using System.Web.Script.Serialization;
using System.Drawing;

namespace ShapeTemplateLib.Templates.User0 
{
    public partial class FloorWithRooms 
    {

		public override XElement Compile () {

            List<Point2D> List = new List<Point2D>();

            /*
             * Find the unique points out of this list of rooms
             */
            foreach (RoomEdgeGroup reg in this.RoomEdgeGroupList)
            {
                foreach (RoomEdge re in reg.RoomEdgeList) {
                    if (!List.Contains(re.From)) List.Add(re.From);
                    if (!List.Contains(re.To)) List.Add(re.To);
                }
            }

            // Move the points into a simple layout struct
            SimpleLayout oLayout = new SimpleLayout();
            oLayout.HorizontalScale = 10;
            oLayout.VerticalScale = 10;
            oLayout.VertexList = new List<Vertex>();
            int ndx = 0;
            foreach (Point2D l in List)
            {
                oLayout.VertexList.Add(new Vertex()
                {
                    Index = ndx++,
                    X = l.X,
                    Y = l.Y
                });
            }
            /*
             * Create the unique edges 
             */
            foreach (RoomEdgeGroup reg in this.RoomEdgeGroupList)
            {
                foreach (RoomEdge re in reg.RoomEdgeList)
                {
                    Vertex v1 = oLayout.VertexList.Where(m => m.X == re.From.X && m.Y == re.From.Y).FirstOrDefault();
                    Vertex v2 = oLayout.VertexList.Where(m => m.X == re.To.X && m.Y == re.To.Y).FirstOrDefault();

                    // does this edge exist

                    oLayout.EdgeList.Add(
                    new Edge()
                    {
                        p1 = v1.Index,
                        p2 = v2.Index,
                        Width = (int) re.Width,
                        Height= (int) re.Height,
                        ID =re.ID,
                        HoleGroupID=re.HoleGroupID
                    });
                }
            }
            /*
             * Compile this simple layout and return its value
             */
            return oLayout.Compile();
		}  

	
	}
}
