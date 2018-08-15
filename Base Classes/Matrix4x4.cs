using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

/*
 * Some of the logic in this file came from the definitions in System.Numerics in https://github.com/dotnet/corefx, which is 
 * licensed with an MIT license
 * 
 * 
The MIT License (MIT)

Copyright (c) .NET Foundation and Contributors

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */
namespace ShapeTemplateLib
{

    public class Matrix4x4 : ILoadAndSaveProperties
    {

        // Initialize as a unit matrix

        public float M11 { get; set; } = 1;
        public float M12 { get; set; } = 0;
        public float M13 { get; set; } = 0;
        public float M14 { get; set; } = 0;

        public float M21 { get; set; } = 0;
        public float M22 { get; set; } = 1;
        public float M23 { get; set; } = 0;
        public float M24 { get; set; } = 0;

        public float M31 { get; set; } = 0;
        public float M32 { get; set; } = 0;
        public float M33 { get; set; } = 1;
        public float M34 { get; set; } = 0;

        public float M41 { get; set; } = 0;
        public float M42 { get; set; } = 0;
        public float M43 { get; set; } = 0;
        public float M44 { get; set; } = 1;

        public static Matrix4x4 Translation(float dX,float dY, float dZ)
        {
            Matrix4x4 tr = new Matrix4x4();

            tr.M41 = dX;
            tr.M42 = dY;
            tr.M43 = dZ;
            tr.M44 = 1.0f;

            return tr;

        }

        public static Matrix4x4 Scaling(float Scale)
        {
            return Matrix4x4.Scaling(Scale, Scale, Scale);
        }

        public static Matrix4x4 Scaling(float xScale, float yScale, float zScale)
        {
            Matrix4x4 sc = new Matrix4x4();

            sc.M11 = xScale;
            sc.M12 = 0.0f;
            sc.M13 = 0.0f;
            sc.M14 = 0.0f;

            sc.M21 = 0.0f;
            sc.M22 = yScale;
            sc.M23 = 0.0f;
            sc.M24 = 0.0f;

            sc.M31 = 0.0f;
            sc.M32 = 0.0f;
            sc.M33 = zScale;
            sc.M34 = 0.0f;

            sc.M41 = 0.0f;
            sc.M42 = 0.0f;
            sc.M43 = 0.0f;
            sc.M44 = 1.0f;

            return sc;

        }

        /// <summary>
        /// Multiply a 3D vector through this matrix. 
        /// Sine 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector3D MultiplyVector(Vector3D position)
        {
            return new Vector3D(
                position.X * this.M11 + position.Y * this.M21 + position.Z * this.M31 + this.M41,
                position.X * this.M12 + position.Y * this.M22 + position.Z * this.M32 + this.M42,
                position.X * this.M13 + position.Y * this.M23 + position.Z * this.M33 + this.M43);
        }

        // Convenient entry point for degrees when they are integers.
        public static Matrix4x4 RotateX(int Degrees)
        {
            return RotateX(Degrees * 0.0174533);
        }

        /// <summary>
        /// Return a matrix ready for a rotation about the X axis
        /// 
        // [  1  0  0  0 ]
        // [  0  c  s  0 ]
        // [  0 -s  c  0 ]
        // [  0  0  0  1 ]
        /// 
        /// </summary>
        /// <param name="Radians"></param>
        /// <returns></returns>
        /// 
        public static Matrix4x4 RotateX(double Radians)
        {
            float CosTheta = (float) Math.Cos(Radians);
            float SinTheta = (float) Math.Sin(Radians);

            Matrix4x4 m = new Matrix4x4()
            {
                M11 = 1,
                M12 = 0,
                M13 = 0,
                M14 = 0,

                M21 = 0,
                M22 = CosTheta,
                M23 = SinTheta,
                M24 = 0,

                M31 = 0,
                M32 = -SinTheta,
                M33 = CosTheta,
                M34 = 0,

                M41 = 0,
                M42 = 0,
                M43 = 0,
                M44 = 1,

            };

            return m;

        }


        public static Matrix4x4 RotateY(int Degrees)
        {
            return RotateY (Degrees * 0.0174533);
        }

        /// <summary>
        /// Return a matrix ready for a rotation about the Y axis
        /// 
        // [  c  0 -s  0 ]
        // [  0  1  0  0 ]
        // [  s  0  c  0 ]
        // [  0  0  0  1 ]
        /// 
        /// </summary>
        /// <param name="Radians"></param>
        /// <returns></returns>
        public static Matrix4x4 RotateY(double Radians)
        {
            float CosTheta = (float)Math.Cos(Radians);
            float SinTheta = (float)Math.Sin(Radians);

            Matrix4x4 m = new Matrix4x4()
            {
                M11 = CosTheta,
                M12 = 0,
                M13 = -SinTheta,
                M14 = 0,

                M21 = 0,
                M22 = 1,
                M23 = 0,
                M24 = 0,

                M31 = SinTheta,
                M32 = 0,
                M33 = CosTheta,
                M34 = 0,

                M41 = 0,
                M42 = 0,
                M43 = 0,
                M44 = 1,

            };

            return m;

        }

        public static Matrix4x4 RotateZ(int Degrees)
        {
            return RotateZ(Degrees * 0.0174533);
        }

        /// <summary>
        /// Return a matrix ready for a rotation about the Z axis
        /// 
        // [  c  s  0  0 ]
        // [ -s  c  0  0 ]
        // [  0  0  1  0 ]
        // [  0  0  0  1 ]
        /// 
        /// </summary>
        /// <param name="Radians"></param>
        /// <returns></returns>
        public static Matrix4x4 RotateZ(double Radians)
        {
            float CosTheta = (float)Math.Cos(Radians);
            float SinTheta = (float)Math.Sin(Radians);

            Matrix4x4 m = new Matrix4x4()
            {
                M11 = CosTheta,
                M12 = SinTheta,
                M13 = 0,
                M14 = 0,

                M21 = -SinTheta,
                M22 = CosTheta,
                M23 = 0,
                M24 = 0,

                M31 = 0,
                M32 = 0,
                M33 = 1,
                M34 = 0,

                M41 = 0,
                M42 = 0,
                M43 = 0,
                M44 = 1,
            };

            return m;

        }

        /// <summary>
        /// Creates a matrix that rotates around an arbitrary vector.
        /// </summary>
        /// <param name="axis">The axis to rotate around.</param>
        /// <param name="angle">The angle to rotate around the given axis, in radians.</param>
        /// <returns>The rotation matrix.</returns>
        public static Matrix4x4 CreateFromAxisAngle(Vector3D axis, float angle)
        {
            // a: angle
            // x, y, z: unit vector for axis.
            //
            // Rotation matrix M can compute by using below equation.
            //
            //        T               T
            //  M = uu + (cos a)( I-uu ) + (sin a)S
            //
            // Where:
            //
            //  u = ( x, y, z )
            //
            //      [  0 -z  y ]
            //  S = [  z  0 -x ]
            //      [ -y  x  0 ]
            //
            //      [ 1 0 0 ]
            //  I = [ 0 1 0 ]
            //      [ 0 0 1 ]
            //
            //
            //     [  xx+cosa*(1-xx)   yx-cosa*yx-sina*z zx-cosa*xz+sina*y ]
            // M = [ xy-cosa*yx+sina*z    yy+cosa(1-yy)  yz-cosa*yz-sina*x ]
            //     [ zx-cosa*zx-sina*y zy-cosa*zy+sina*x   zz+cosa*(1-zz)  ]
            //
            float x = axis.X, y = axis.Y, z = axis.Z;
            float sa = (float) Math.Sin(angle), ca = (float) Math.Cos(angle);
            float xx = x * x, yy = y * y, zz = z * z;
            float xy = x * y, xz = x * z, yz = y * z;

            Matrix4x4 result = new Matrix4x4();

            result.M11 = xx + ca * (1.0f - xx);
            result.M12 = xy - ca * xy + sa * z;
            result.M13 = xz - ca * xz - sa * y;
            result.M14 = 0.0f;
            result.M21 = xy - ca * xy - sa * z;
            result.M22 = yy + ca * (1.0f - yy);
            result.M23 = yz - ca * yz + sa * x;
            result.M24 = 0.0f;
            result.M31 = xz - ca * xz + sa * y;
            result.M32 = yz - ca * yz - sa * x;
            result.M33 = zz + ca * (1.0f - zz);
            result.M34 = 0.0f;
            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1.0f;

            return result;
        }


        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Matrix4x4 Multiply(Matrix4x4 value1, Matrix4x4 value2)
        {
            Matrix4x4 result = new Matrix4x4();

            // First row
            result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41;
            result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42;
            result.M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43;
            result.M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44;

            // Second row
            result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41;
            result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42;
            result.M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43;
            result.M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44;

            // Third row
            result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41;
            result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42;
            result.M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43;
            result.M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44;

            // Fourth row
            result.M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41;
            result.M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42;
            result.M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43;
            result.M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44;

            return result;
        }

        public XElement GetProperties(string PropertyName = "")
        {
            return new XElement("matrix4x4",
                new XAttribute("prop", PropertyName),
                new XAttribute("M11", this.M11),
                new XAttribute("M12", this.M12),
                new XAttribute("M13", this.M13),
                new XAttribute("M14", this.M14),

                new XAttribute("M21", this.M21),
                new XAttribute("M22", this.M22),
                new XAttribute("M23", this.M23),
                new XAttribute("M24", this.M24),

                new XAttribute("M31", this.M31),
                new XAttribute("M32", this.M32),
                new XAttribute("M33", this.M33),
                new XAttribute("M34", this.M34),

                new XAttribute("M41", this.M41),
                new XAttribute("M42", this.M42),
                new XAttribute("M43", this.M43),
                new XAttribute("M44", this.M44));

        }

        public bool LoadProperties(XElement ele, out string Message)
        {
            Message = "OK";

            float value = 0;
            if (!Utilities.GetFloatAttribute(ele, "M11", out value, out Message)) return false;
            this.M11 = value;

            if (!Utilities.GetFloatAttribute(ele, "M12", out value, out Message)) return false;
            this.M12 = value;

            if (!Utilities.GetFloatAttribute(ele, "M13", out value, out Message)) return false;
            this.M13 = value;

            if (!Utilities.GetFloatAttribute(ele, "M14", out value, out Message)) return false;
            this.M14 = value;


            if (!Utilities.GetFloatAttribute(ele, "M21", out value, out Message)) return false;
            this.M21 = value;

            if (!Utilities.GetFloatAttribute(ele, "M22", out value, out Message)) return false;
            this.M22 = value;

            if (!Utilities.GetFloatAttribute(ele, "M23", out value, out Message)) return false;
            this.M23 = value;

            if (!Utilities.GetFloatAttribute(ele, "M24", out value, out Message)) return false;
            this.M24 = value;


            if (!Utilities.GetFloatAttribute(ele, "M31", out value, out Message)) return false;
            this.M31 = value;

            if (!Utilities.GetFloatAttribute(ele, "M32", out value, out Message)) return false;
            this.M32 = value;

            if (!Utilities.GetFloatAttribute(ele, "M33", out value, out Message)) return false;
            this.M33 = value;

            if (!Utilities.GetFloatAttribute(ele, "M34", out value, out Message)) return false;
            this.M34 = value;


            if (!Utilities.GetFloatAttribute(ele, "M41", out value, out Message)) return false;
            this.M41 = value;

            if (!Utilities.GetFloatAttribute(ele, "M42", out value, out Message)) return false;
            this.M42 = value;

            if (!Utilities.GetFloatAttribute(ele, "M43", out value, out Message)) return false;
            this.M43 = value;

            if (!Utilities.GetFloatAttribute(ele, "M44", out value, out Message)) return false;
            this.M44 = value;

            return true;
        }


    }
}
