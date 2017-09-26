using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.BasicShapes
{
    public partial class Panel : IGetSample
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
                    ShortDescription = "Two rectangles",
                    LongDescription = "Two parallel rectangles, the simplest form of a panel. The outline for a panel can be any closed polygon that doesn't intersect",
                    Sample = new Panel()
                    {
                         FrontMesh= new FlatMesh()
                         {
                              Boundary = new BoundaryRectangle(40, 60),
                              MeshDisplayProperties = new MeshDisplayProperties()
                              {
                                  SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                  MaterialColor = "#0000FF"
                              }
                         },
                         BackMesh=new FlatMesh()
                         {
                              Boundary = new BoundaryRectangle(40, 60,-30),
                              MeshDisplayProperties = new MeshDisplayProperties()
                              {
                                  SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                  MaterialColor = "#0000FF"
                              },
                             Offset =new Point3D(0,0,-30)
                         }
                    }.GetProperties()
                },
                new SampleInstance()
                {
                    ShortDescription = "Connected rectangles",
                    LongDescription = "These meshes are connected with a hole that goes through them both",
                    Sample = new Panel()
                    {
                         FrontMesh= new FlatMesh()
                         {
                              Boundary = new BoundaryRectangle(40, 60),
                              MeshDisplayProperties = new MeshDisplayProperties()
                              {
                                  SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                  MaterialColor = "#0000FF"
                              },
                              HoleList=new List<FlatMesh.Hole>()
                              {
                                   new FlatMesh.Hole()
                                   {
                                        Boundary=new BoundaryRectangle(15,15),
                                        Offset=new Point3D(10,10,0),
                                        ID="front"
                                   }
                              }
                         },
                         BackMesh=new FlatMesh()
                         {
                              Boundary = new BoundaryRectangle(40, 60,-30),
                              MeshDisplayProperties = new MeshDisplayProperties()
                              {
                                  SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                  MaterialColor = "#0000FF"
                              },
                              Offset =new Point3D(0,0,-30),
                              HoleList=new List<FlatMesh.Hole>()
                              {
                                   new FlatMesh.Hole()
                                   {
                                        Boundary=new BoundaryRectangle(15,15),
                                        Offset=new Point3D(10,10,0),
                                        ID="back"
                                   }
                              }
                         },
                         ConnectedHoleList=new List<ConnectedHole>()
                         {
                              new ConnectedHole()
                              {
                                  BackID="back",
                                  FrontID ="front",
                                  DisplayProperties= new MeshDisplayProperties()
                                  {
                                    SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                    MaterialColor = "#0000FF"
                                  },
                              }
                         }
                    }.GetProperties()
                },
            new SampleInstance()
                {
                    ShortDescription = "Connected and other holes",
                    LongDescription = "These meshes are connected with a hole that goes through them both. There are also holes in the individual panels that do not connect",
                    Sample = new Panel()
                    {
                         FrontMesh= new FlatMesh()
                         {
                              Boundary = new BoundaryRectangle(40, 60),
                              MeshDisplayProperties = new MeshDisplayProperties()
                              {
                                  SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                  MaterialColor = "#0000FF"
                              },
                              HoleList=new List<FlatMesh.Hole>()
                              {
                                   new FlatMesh.Hole()
                                   {
                                        Boundary=new BoundaryRectangle(15,15),
                                        Offset=new Point3D(8,10,0),
                                        ID="front"
                                   },
                                    new FlatMesh.Hole()
                                   {
                                        Boundary=new BoundaryEllipse(5,10),
                                        Offset=new Point3D(30,20,0),
                                   }
                              }
                         },
                         BackMesh=new FlatMesh()
                         {
                              Boundary = new BoundaryRectangle(40, 60,-30),
                              MeshDisplayProperties = new MeshDisplayProperties()
                              {
                                  SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                  MaterialColor = "#0000FF"
                              },
                              Offset =new Point3D(0,0,-30),
                              HoleList=new List<FlatMesh.Hole>()
                              {
                                   new FlatMesh.Hole()
                                   {
                                        Boundary=new BoundaryRectangle(15,15),
                                        Offset=new Point3D(10,10,0),
                                        ID="back"
                                   },
                                   new FlatMesh.Hole()
                                   {
                                        Boundary=new BoundaryEllipse(10,5),
                                        Offset=new Point3D(10,40,0),
                                   }
                              }
                         },
                         ConnectedHoleList=new List<ConnectedHole>()
                         {
                              new ConnectedHole()
                              {
                                  BackID="back",
                                  FrontID ="front",
                                  DisplayProperties= new MeshDisplayProperties()
                                  {
                                    SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                    MaterialColor = "#0000FF"
                                  },
                              }
                         }
                    }.GetProperties()
                },
                new SampleInstance()
                {
                    ShortDescription = "Panels connected",
                    LongDescription = "These meshes are connected. The front and back mesh of a panel can be connected, and that connection can show some or all of the connections. In this example the top of the panel is open, like a box that has an open top",
                    Sample = new Panel()
                    {
                         FrontMesh= new FlatMesh()
                         {
                              Boundary = new BoundaryRectangle(40, 60),
                              MeshDisplayProperties = new MeshDisplayProperties()
                              {
                                  SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                  MaterialColor = "#0000FF"
                              },
                         },
                         BackMesh=new FlatMesh()
                         {
                              Boundary = new BoundaryRectangle(40, 60,-30),
                              MeshDisplayProperties = new MeshDisplayProperties()
                              {
                                  SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                  MaterialColor = "#0000FF"
                              },
                              Offset =new Point3D(0,0,-30),
                         },
                         ConnectorDisplayProperties=new MeshDisplayProperties()
                         {
                             SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                             MaterialColor = "#0000FF"
                         },
                         ConnectorSegmentVisible= new List<bool>() { true,true,false,true }

                    }.GetProperties()
                },

                new SampleInstance()
                {
                    ShortDescription = "Connected with a hole",
                    LongDescription = "These meshes are connected with a hole that goes through them both and the meshes themselves are connected",
                    Sample = new Panel()
                    {
                         FrontMesh= new FlatMesh()
                         {
                              Boundary = new BoundaryRectangle(40, 60),
                              MeshDisplayProperties = new MeshDisplayProperties()
                              {
                                  SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                  MaterialColor = "#0000FF"
                              },
                              HoleList=new List<FlatMesh.Hole>()
                              {
                                   new FlatMesh.Hole()
                                   {
                                        Boundary=new BoundaryRectangle(15,15),
                                        Offset=new Point3D(10,10,0),
                                        ID="front"
                                   }
                              }
                         },
                         BackMesh=new FlatMesh()
                         {
                              Boundary = new BoundaryRectangle(40, 60,-30),
                              MeshDisplayProperties = new MeshDisplayProperties()
                              {
                                  SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                  MaterialColor = "#0000FF"
                              },
                              Offset =new Point3D(0,0,-30),
                              HoleList=new List<FlatMesh.Hole>()
                              {
                                   new FlatMesh.Hole()
                                   {
                                        Boundary=new BoundaryRectangle(15,15),
                                        Offset=new Point3D(10,10,0),
                                        ID="back"
                                   }
                              }
                         },
                         ConnectedHoleList=new List<ConnectedHole>()
                         {
                              new ConnectedHole()
                              {
                                  BackID="back",
                                  FrontID ="front",
                                  DisplayProperties= new MeshDisplayProperties()
                                  {
                                    SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                                    MaterialColor = "#0000FF"
                                  },
                              }
                         },
                        ConnectorDisplayProperties=new MeshDisplayProperties()
                         {
                             SidesToShow = MeshDisplayProperties.eMeshElementVisibility.ShowBoth,
                             MaterialColor = "#0000FF"
                         },
                         ConnectorSegmentVisible= new List<bool>() { true,true,false,true }
                    }.GetProperties()
                },

            };


            return list;

        }
    }
}
