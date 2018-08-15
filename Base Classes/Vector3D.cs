using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    /// <summary>
    /// This class holds an X,Y and Z value representing a vector in 3D space
    /// The name used for this element to the API is the field name for whatever contains it.
    /// The Vector3D and the Point3D contain the same thing, and X Y and Z value, the difference is intent
    /// and over time there may be vector or matrix routines that will want a class that can do vector operations
    /// </summary>
    [HelpItem(eItemFlavor.Data, "vector3d")]
    public class Vector3D : Point3D
    {
        public Vector3D()
        {

        }

        public Vector3D(float X, float Y, float Z) : base(X, Y, Z)
        {

        }

        public double Magnitude
        {
            get { return Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        public static void Normalize(Vector3D v)
        {
            float m = (float) Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            if (m > 0.001)
            {
                v.X /= m; v.Y /= m; v.Z /= m;
            }
        }

        public static double Dot(Vector3D v1, Vector3D v2) // A . B = |A|*|B|*cos(angle)
        {
            return (v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z);
        }


        public new  XElement GetProperties(string PropertyName="")
        {
            return new XElement("vector3d",
                new XAttribute("prop", PropertyName),
                new XAttribute("x", this.X),
                new XAttribute("y", this.Y),
                new XAttribute("z", this.Z));
        }
    }
}
