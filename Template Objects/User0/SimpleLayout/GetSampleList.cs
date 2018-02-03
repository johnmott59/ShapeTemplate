using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SimpleLayout : IGetSample
    {
        /// <summary>
        /// Return a list of sample instances
        /// </summary>
        /// <returns></returns>
        public List<SampleInstance> GetSampleList()
        {
            SimpleLayout oLayout = GetSample();

            List<SampleInstance> list = new List<SampleInstance>() {

            new SampleInstance()
                {
                    ShortDescription = "3 panel layout",
                    LongDescription = "Three walls with different holes",
                    Sample = oLayout.GetProperties()
                },
            };

            return list;
        }

        private SimpleLayout GetSample()
        {
            int Width = 100;
            int Length = 75;
            int Height = 50;
            int Thickness = 5;

            SimpleLayout oSimpleLayout = new SimpleLayout()
            {
                HorizontalScale = 1,
                VerticalScale = 1
            };
            // Create 4 vertices
            oSimpleLayout.VertexList = new List<Vertex>()
            {
                new Vertex() { Index=1, X=0, Y=0 },
                new Vertex() { Index=2, X=Width-10, Y=0 },
                new Vertex() { Index=3, X=Width+10, Y=Length },
                new Vertex() { Index=4, X=0, Y=Length }
            };

            oSimpleLayout.EdgeList = new List<Edge>()
            {
                new Edge() { Height= (int) Height, p1=1,p2=2, Width=(int)Thickness, ID="e1", HoleGroupID= "LeftID" },
                new Edge() { Height= (int) Height, p1=2,p2=3, Width=(int)Thickness, ID="e2", HoleGroupID="BackID" },
                new Edge() { Height= (int) Height, p1=3,p2=4, Width=(int)Thickness, ID="e3",  HoleGroupID="RightID" },

            };
            /*
             * Add each of the holes that are defined into the rectangle list, saving their indices
             */
            List<LayoutHole> list = new List<LayoutHole>();

            oSimpleLayout.BoundaryRectangleList.Add(new BoundaryRectangle(30, 10));
            list.Add(new LayoutHole()
            {
                HoleType = "rect",
                HoleTypeIndex = oSimpleLayout.BoundaryRectangleList.Count - 1,
                OffsetX = 30,
                OffsetY = 10
            });

            oSimpleLayout.HoleGroupList.Add(new HoleGroup()
            {
                HoleGroupID = "LeftID",
                HoleList = list.ToArray()
            });

            /// back hole
            list = new List<LayoutHole>();

            oSimpleLayout.BoundaryEllipseList.Add(new BoundaryEllipse(20, 5));
            list.Add(new LayoutHole()
            {
                HoleType = "ell",
                HoleTypeIndex = oSimpleLayout.BoundaryEllipseList.Count - 1,
                OffsetX = 30,
                OffsetY = Height / 2
            });

            oSimpleLayout.HoleGroupList.Add(new HoleGroup()
            {
                HoleGroupID = "BackID",
                HoleList = list.ToArray()
            });

            /// side
            list = new List<LayoutHole>();

            oSimpleLayout.BoundaryPolygonList.Add(new BoundaryPolygon()
            {
                PointList = new Point3D[]
                {
                    new Point3D(0, 0, 0),
                    new Point3D(15, 15, 0),
                    new Point3D(30, 30, 0),
                    new Point3D(10, 20, 0),
                }
            });
            list.Add(new LayoutHole()
            {
                HoleType = "poly",
                HoleTypeIndex = oSimpleLayout.BoundaryPolygonList.Count - 1,
                OffsetX = 30,
                OffsetY = 10
            });

            oSimpleLayout.HoleGroupList.Add(new HoleGroup()
            {
                HoleGroupID = "RightID",
                HoleList = list.ToArray()
            });
#if false
            // add a floor
            string[] DescriptorList = new string[]
            {
                "ee1.l.f",
                "ee1.r.f",
                "ee2.r.f",
            };

            // Define a horizontal panel for the roof
            oSimpleLayout.HorizontalPanelList.Add(new HorizontalPanel()
            {
                Height = Height / 2,
                Thickness = Thickness,
                DescriptorList = DescriptorList,
            });

#endif
            return oSimpleLayout;

        }
    }
}
