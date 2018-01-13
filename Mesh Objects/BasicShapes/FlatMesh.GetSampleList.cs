using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.BasicShapes
{
    public partial class FlatMesh : IGetSample
    {
        /// <summary>
        /// Return a list of sample instances
        /// </summary>
        /// <returns></returns>
        public List<SampleInstance> GetSampleList()
        {
            List<SampleInstance> list = new List<SampleInstance>() {
                new SampleInstance()
                {
                    ShortDescription = "Rectangle",
                    LongDescription = "Simple rectangle",
                    Sample = new FlatMesh()
                    {
                        Boundary = new BoundaryRectangle(40, 60),
                        MeshDisplayProperties = new MeshDisplayProperties()
                        {
                            SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                            MaterialColor = "#0000FF"
                        }
                    }.GetProperties()
                },

                new SampleInstance()
                {
                    ShortDescription = "ellipse",
                    LongDescription = "Simple ellipse",
                    Sample = new FlatMesh()
                    {
                        Boundary = new  BoundaryEllipse(40,60),
                        MeshDisplayProperties = new MeshDisplayProperties()
                        {
                            SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                            MaterialColor = "#00FF00"
                        }
                    }.GetProperties()
                },

                new SampleInstance()
                {
                    ShortDescription = "ellipse with hole",
                    LongDescription = "This ellipse has a hole in it ",
                    Sample = new FlatMesh()
                    {
                        Boundary = new  BoundaryEllipse(40,60),
                        MeshDisplayProperties = new MeshDisplayProperties()
                        {
                            SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                            MaterialColor = "#00FF00"
                        },
                        HoleList=new List<Hole>()
                        {
                             new Hole()
                             {
                                  Boundary=new BoundaryRectangle(20,20),
                                  Offset=new Point3D(10,10,0),
                             }
                        }
                    }.GetProperties()
                },

                new SampleInstance()
                {
                    ShortDescription = "star with holes",
                    LongDescription = "A star shape with three holes in it. A list of points is used to define the outline ",
                    Sample = new FlatMesh()
                    {
                        Boundary = new BoundaryPolygon()
                        {
                             PointList=new Point3D[]
                             {
                                new Point3D(30,0,0),
                                new Point3D(48.54F,35.26f,0),
                                new Point3D(9.27f,28.53f,0),
                                new Point3D(-18.54f,57.06f,0),
                                new Point3D(-24.27f,17.63f,0),
                                new Point3D(-59.99f,-0.00f,0),
                                new Point3D(-24.27f,-17.63f,0),
                                new Point3D(-18.54f,-57.06f,0),
                                new Point3D(9.27f,-28.53f,0),
                                new Point3D(48.54f,-35.26f,0),
                                new Point3D(30,0,0),
                             }
                        },
                        MeshDisplayProperties = new MeshDisplayProperties()
                        {
                            SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                            MaterialColor = "#00FF00"
                        },
                        HoleList=new List<Hole>()
                        {
                             new Hole()
                             {
                                  Boundary=new BoundaryRectangle(10,10),
                                  Offset=new Point3D(-20,-15,0),
                             },
                            new Hole()
                             {
                                  Boundary=new BoundaryRectangle(10,10),
                                  Offset=new Point3D(20,-15,0),
                             },
                              new Hole()
                             {
                                  Boundary=new BoundaryEllipse(10,10),
                                  Offset=new Point3D(20,10,0),
                             }
                        }
                    }.GetProperties()
                }
            };


            return list;

        }
    }
}
