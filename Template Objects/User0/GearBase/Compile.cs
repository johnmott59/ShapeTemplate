using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;
using System.Web.Script.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ShapeTemplateLib.Templates.User0 
{

    public partial class GearBase 
    {
		public override XElement Compile () {
            /*
             * Create a panel whose outline is a repetition of the tooth shape
             * The count of teeth form a regular polygon with each side of length 
             */
            double AnglePerTooth = 360 / ToothCount;
            double SideLength = 1;
            double CircumRadius = SideLength / (2 * Math.Tan(Math.PI / ToothCount));

          
            List<Point3D> FrontList = new List<Point3D>();
            List<Point3D> BackList = new List<Point3D>();
            List<bool> ConnectorList = new List<bool>();
            /*
             * Create the base pattern for the tooth, to scale
             */
            List<PointF> ScaledToothList = new List<PointF>();
            double TotalAngle = 0;
            for (int i = 0; i < ToothShape.Point2DList.Length; i++)
            {
                Point2D p2d = ToothShape.Point2DList[i];
                double scaledx = p2d.X ;
                double scaledy = CircumRadius + p2d.Y ;
                ScaledToothList.Add(new PointF() { X = (float) scaledx, Y = (float) scaledy });
            }
            /*
             * Now create a version of the tooth pattern that is rotated, once for each tooth
             */
            for (int i=0; i < ToothCount; i++)
            {
                Matrix rotate = new Matrix();
                rotate.Rotate((float) TotalAngle);
               
                PointF[] xform = new List<PointF>(ScaledToothList).ToArray();
                // Rotate this tooth pattern
                rotate.TransformPoints(xform);

                foreach (PointF rp in xform)
                {
                    FrontList.Add(new Point3D() { X = Scale * rp.X, Y = Scale * rp.Y, Z = 0 });
                    BackList.Add(new Point3D() { X = Scale * rp.X, Y = Scale * rp.Y, Z = Width });
                }

                TotalAngle -= AnglePerTooth;
                ConnectorList.Add(true);
            }
            
            BoundaryPolygon bpFront = new BoundaryPolygon();
            bpFront.PointList = FrontList.ToArray();

            BoundaryPolygon bpRear = new BoundaryPolygon();
            bpRear.PointList = BackList.ToArray();

            Panel p = new Panel()
            {
                oFrameOfReference = this.oFrameOfReference,         // Pass down frame of reference
                LocalTransform = this.LocalTransform,               // pass down local transform
                FrontMesh = new FlatMesh() { Boundary = bpFront, oFrameOfReference=this.oFrameOfReference },
                BackMesh = new FlatMesh() { Boundary = bpRear, oFrameOfReference=this.oFrameOfReference },
                ConnectorSegmentVisible = ConnectorList,
            };

            p.FrontMesh.HoleList = new Hole[GearHoles.HoleList.Length];
            p.BackMesh.HoleList = new Hole[GearHoles.HoleList.Length];

            p.ConnectedHoleList = new List<Panel.ConnectedHole>();

            for (int i=0; i < GearHoles.HoleList.Length; i++)
            {
                Hole oHole = GearHoles.HoleList[i];

                p.FrontMesh.HoleList[i] = Hole.CopyFrom(oHole);
                p.FrontMesh.HoleList[i].ID = $"fm{i}";

                p.BackMesh.HoleList[i] = Hole.CopyFrom(oHole);
                p.BackMesh.HoleList[i].Offset = new Point3D(0, 0, Width);
                p.BackMesh.HoleList[i].ID = $"bm{i}";

                p.ConnectedHoleList.Add(new Panel.ConnectedHole()
                {
                    FrontID = $"fm{i}",
                    BackID = $"bm{i}"
                });

            }
   
          //  GearHoles.HoleList = new Hole() {  Boundary=new BoundaryRectangle(10,10), ID=""}

            return p.Compile();

		}  

	
	}
}
