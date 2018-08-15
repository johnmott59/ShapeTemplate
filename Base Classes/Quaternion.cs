using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * This code for Quaternions came from the project:  Quaternion Mathematics and 3D Library with C# and GDI+ 
 * The url is https://www.codeproject.com/Articles/36868/Quaternion-Mathematics-and-D-Library-with-C-and-G
 *  
 * This is covered by the Code Project Open License (CPOL) 1.02
 * https://www.codeproject.com/info/cpol10.aspx
 * 
 * The license requires that any copyright notification be left in the file, however this file did not contain any
 * copyright text, so none was deleted.
 * 
 */
namespace ShapeTemplateLib
{
    /// <summary>
    /// This class holds a quaternion and allows operations on that quaternion. A quaternion is uniquly 
    /// powerful in its ability to allow rotation around an arbitrary vector.
    /// </summary>
    [HelpItem(eItemFlavor.Data, "Quaternion")]
    public struct Quaternion
    {
        public double X, Y, Z, W;

        public Quaternion(double w, double x, double y, double z)
        {
            W = w; X = x; Y = y; Z = z;
        }

        public Quaternion(double w, Vector3D v)
        {
            W = w; X = v.X; Y = v.Y; Z = v.Z;
        }

        public Vector3D V
        {
            set { X = value.X; Y = value.Y; Z = value.Z; }
            get { return new Vector3D((float) X, (float) Y, (float) Z); }
        }

        public void Normalise()
        {
            double m = W * W + X * X + Y * Y + Z * Z;
            if (m > 0.001)
            {
                m = Math.Sqrt(m);
                W /= m;
                X /= m;
                Y /= m;
                Z /= m;
            }
            else
            {
                W = 1; X = 0; Y = 0; Z = 0;
            }
        }

        public void Conjugate()
        {
            X = -X; Y = -Y; Z = -Z;
        }

        public void FromAxisAngle(Vector3D axis, double angleRadian)
        {
            double m = axis.Magnitude;
            if (m > 0.0001)
            {
                double ca = Math.Cos(angleRadian / 2);
                double sa = Math.Sin(angleRadian / 2);
                X = axis.X / m * sa;
                Y = axis.Y / m * sa;
                Z = axis.Z / m * sa;
                W = ca;
            }
            else
            {
                W = 1; X = 0; Y = 0; Z = 0;
            }
        }

        public Quaternion Copy()
        {
            return new Quaternion(W, X, Y, Z);
        }

        public void Multiply(Quaternion q)
        {
            this *= q;
        }

        //                  -1
        // V'=q*V*q     ,
        public Point3D Rotate(Point3D pt)
        {
            this.Normalise();
            Quaternion q1 = this.Copy();
            q1.Conjugate();

            Quaternion qNode = new Quaternion(0, pt.X, pt.Y, pt.Z);
            qNode = this * qNode * q1;
            pt.X = (float) qNode.X;
            pt.Y = (float) qNode.Y;
            pt.Z = (float) qNode.Z;

            return pt;
        }

        public void Rotate(Point3D[] nodes)
        {
            this.Normalise();
            Quaternion q1 = this.Copy();
            q1.Conjugate();
            for (int i = 0; i < nodes.Length; i++)
            {
                Quaternion qNode = new Quaternion(0, nodes[i].X, nodes[i].Y, nodes[i].Z);
                qNode = this * qNode * q1;
                nodes[i].X = (float) qNode.X;
                nodes[i].Y = (float) qNode.Y;
                nodes[i].Z = (float) qNode.Z;
            }
        }

        // Multiplying q1 with q2 is meaning of doing q2 firstly then q1
        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            double nw = q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z;
            double nx = q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y;
            double ny = q1.W * q2.Y + q1.Y * q2.W + q1.Z * q2.X - q1.X * q2.Z;
            double nz = q1.W * q2.Z + q1.Z * q2.W + q1.X * q2.Y - q1.Y * q2.X;
            return new Quaternion(nw, nx, ny, nz);
        }
    }
}
