﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    /// <summary>
    /// The hole in a flat mesh describes its placement and dimension. A hole is defined by a boundary and an offset from a
    /// (0,0) position on the mesh in XY space. A mesh can have any orientation, of course, but for the purposes of placing
    /// holes the mesh is positioned to an XY plane with +Z looking towards you, and the hole is defined as an offset and
    /// boundary in that XY space
    /// </summary>
    [HelpItem(eItemFlavor.Data, "hole")]
    public partial class Hole
    {
        public Hole()
        {
            Offset = new Point3D();
        }
        /// <summary>
        /// The ID value identifies this hole to other structures that may need it. It is not required
        /// </summary>
        [HelpProperty(XPropertyPosition = HelpPropertyAttribute.eXPropertyPosition.AttributeOfParent, SampleValue = "Window")]
        public string ID { get; set; } = "";             // this is used to identify this hole structure with a larger shape

        /// <summary>
        /// The offset of a hole is where it is positioned
        /// </summary>
        [HelpProperty]
        public Point3D Offset { get; set; } = new Point3D(0, 0, 0);

        /// <summary>
        /// The boundary for a hole can be a rectangle, ellipse or line segment
        /// </summary>
        [HelpProperty]
        public BoundaryRoot Boundary { get; set; }

        // The CopyFrom method is used in Javascript to convert a version of the object retrieved via JSON
        // into a full object of this type. An object retrieved via JSON.parse() will have the correct property
        // names but not be an object of this type, this will take that property-only version and create a full
        // object

        public static Hole CopyFrom(Hole oFrom)
        {
            Hole oHole = new Hole();

            oHole.ID = oFrom.ID;
            oHole.Offset = Point3D.CopyFrom(oFrom.Offset);
            oHole.Boundary = BoundaryRoot.CopyFrom(oFrom.Boundary);

            return oHole;
        }
    }
}
