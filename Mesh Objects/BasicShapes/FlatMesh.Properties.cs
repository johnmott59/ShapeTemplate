using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

// a simple panel has a front, back connecting mesh and one or more holes

namespace ShapeTemplateLib.BasicShapes
{
    /// <summary>
    /// A FlatMesh is a basic shape, and is simply a closed non-intersecting polygon. It has a boundary, mesh properties, 
    /// a relative XAxis, an offset, and possibly holes.
    /// 
    /// For purposes of placing holes a panel is defined in the XY plane.The winding of the boundary determines
    /// the relative Z axis, counter clockwise winding indicates that Z is 'out' or towards the viewer looking at the
    /// panel.
    /// </summary>
    [HelpItem(eItemFlavor.BasicShape,"flatmesh")]
    public partial class FlatMesh 
    {
        /// <summary>
        /// Default constructor, allocate items to prevent null reference
        /// </summary>
        public FlatMesh()
        {
            HoleList = new Hole[0];
            LocalTransform = new Matrix4x4();
        }

       
        /// <summary>
        /// The boundary property is used to define the outline of the FlatMesh. Use a derived class BoundaryRectangle,
        /// BoundaryEllipse and BoundaryLineSegment to instantiate a boundary. 
        /// </summary>
        [HelpProperty]
        public BoundaryRoot Boundary { get; set; } = new BoundaryRectangle() { Height = 20, Width = 20 };
       
        /// <summary>
        /// The Mesh display properties contain the color, visibility and name of a texture file
        /// </summary>
        /// <see cref="MeshDisplayProperties"/>
        [HelpProperty("displayproperties")]
        public MeshDisplayProperties MeshDisplayProperties { get; set; } = new MeshDisplayProperties();

        /// <summary>
        /// The offset field for a FlatMesh describes how the 'origin' of the mesh is offset in 3D space. You only
        /// have to specificy it if you are placing holes in the mesh and its not located at the origin in the XY plane.
        /// 
        /// Holes are placed in a FlatMesh relative to an origin (0,0,0) with the mesh in the cartesian XY. 
        /// You face the mesh in the XY plane, Y is to the right and Z comes out at you.
        /// </summary>
        /// <see cref="Point3D"/>
        [HelpProperty]
        public Point3D Offset { get; set; } = new Point3D(0, 0, 0);

        /// <summary>
        /// The xaxis field for a FlatMesh describes how the X axis of the mesh is oriented. You only
        /// have to specificy it if you are placing holes in the mesh and its not located at the origin in the XY plane.
        /// 
        /// Holes are placed in a FlatMesh relative to an origin (0,0,0) with the mesh in the cartesian XY. 
        /// You face the mesh in the XY plane, Y is to the right and Z comes out at you.
        /// </summary>
        /// <see cref="Vector3D"/>
        [HelpProperty]
        public Vector3D XAxis { get; set; } = new Vector3D(1, 0, 0);

        /// <summary>
        /// A flat mesh can have a list of holes. Each hole has an offset and a boundary. Holes are defined assuming
        /// that the mesh is positioned in the XY plane with Z facing forward so you visualize 'looking' at the mesh face on
        /// and positioning the hole right (+x), left (-x), up (+y) and down (-y). The boundary for the holes should be wound
        /// counter clockwise on the face of the mesh, so a triangle might be (0,0) -> (50,0) -> (25,25)
        /// </summary>
        [HelpProperty]
        //public List<Hole> HoleList { get; set; } = new List<Hole>();
        public Hole[] HoleList { get; set; } = new Hole[0];


}

}
    
