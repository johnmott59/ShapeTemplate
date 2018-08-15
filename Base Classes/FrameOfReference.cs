using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShapeTemplateLib
    {
        public class FrameOfReference : ILoadAndSaveProperties
    {
        System.Random r = new Random();

        // Test variable to let control code pass a loop iteration down
        public int TestIteration { get; set; }

            Point3D OriginLocal = new Point3D(0, 0, 0);

            Vector3D XAxisLocal = new Vector3D(1, 0, 0);
            Vector3D YAxisLocal = new Vector3D(0, 1, 0);
            Vector3D ZAxisLocal = new Vector3D(0, 0, 1);

        /*
         * Create the transform needed to go from the local to world
         */
            Vector3D XAxisWorld = new Vector3D(1, 0, 0);
            Vector3D YAxisWorld = new Vector3D(0, 1, 0);
            Vector3D ZAxisWorld = new Vector3D(0, 0, 1);

            Matrix4x4 LocalToWorldTransform;
            Matrix4x4 WorldToLocalTransform;

            public FrameOfReference()
            {
                // Set the initial frame of reference world and local as the world and create transforms

                CreateTransforms();
            }

       /// <summary>
       ///  Transform this point from world to local coordinates of this frame of reference so that it will end up in proper world
       /// </summary>
       /// <param name="p"></param>
       /// <returns></returns>
        public Point3D jkmtest(Point3D p)
        {
            // this is a point which has already been locally transformed and now we want to move it to the coordinate system defined
            // by this frame of reference. to do this we transform the point through the world to local transform. Then it will be 
            // in its final resting place

            Vector3D v = WorldToLocalTransform.MultiplyVector(new Vector3D() { X = p.X, Y = p.Y, Z = p.Z });

            return new Point3D() { X = v.X, Y = v.Y, Z = v.Z };
        }
        
        /// <summary>
        /// JKM TEST TEST TEST 
        /// For development purpose use this class to save rotation and translation values to test out how they will work together
        /// there is one 'frameofreference' for each mesh object, and this routine is called for each point.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
            public Point3D TransformPoint(Point3D p)
            {
            Vector3D v = new Vector3D() { X = p.X, Y = p.Y, Z = p.Z };

            int i = 0; // TestIteration;

            // ALL THIS NEEDS TO BE THOUGHT THROUGH.
            // We want to allow a local transformation on this mesh but then position it according to its local frame of reference,
            // which is a coordinate system and an origin.

            // i think it should look like

            // 1. do local transform rotate in normal xyz, where object is defined
            // 2. do a rotation from world to local, which should orient the object
            // 3. position the object with the localorigon, shuod be a matter of adding local origin to x,y,z

#if false
            // Remember that scaling is done here on a point by point basis, so the result isn't the same shape.
            double s = 1.0; // + r.NextDouble() * i;
            
            Matrix4x4 translate = Matrix4x4.Translation(0,0,0);
            Matrix4x4 scale = Matrix4x4.Scaling((float)s);
            Matrix4x4 rotate = Matrix4x4.RotateY(i*5);

            // The order of operations is arg1, then arg2 
            Matrix4x4 local = Matrix4x4.Multiply(scale,translate);
            local = Matrix4x4.Multiply(local,rotate);

            // Perform local transformation

            v = local.MultiplyVector(v);
#endif

            // Now position this from the world to the local transform
            v = WorldToLocalTransform.MultiplyVector(v);

            // Add the origin of the local transform
            v.X += this.OriginLocal.X;
            v.Y += this.OriginLocal.Y;
            v.Z += this.OriginLocal.Z;

            return new Point3D(v.X, v.Y, v.Z);
            }
#if false
        public Point3D TransformPoint(Point3D p)
        {
            Vector3D v = new Vector3D() { X = p.X, Y = p.Y, Z = p.Z };

            v = WorldToLocalTransform.MultiplyVector(v);

            return new Point3D(v.X + OriginLocal.X, v.Y + OriginLocal.Y, v.Z + OriginLocal.Z);
        }
#endif

        /*
         * Rotate the local frame of reference, meaning rotate X,Y and Z axis about its X, then create new world to local transforms
         */
        public FrameOfReference LocalRotateX(int Degrees)
        {
            return LocalRotateX((float)Degrees * 0.0174533);
        }
        public FrameOfReference LocalRotateX(double Radians)
        {
            // Get a matrix for rotation about x
            Matrix4x4 mX = Matrix4x4.RotateX(Radians);
            /*
             * Create a new frame of reference that is rotated locally around the current X
             */
            FrameOfReference fr = new FrameOfReference()
            {
                OriginLocal = new Point3D(this.OriginLocal.X, this.OriginLocal.Y, this.OriginLocal.Z),
                XAxisLocal = mX.MultiplyVector(this.XAxisLocal),
                YAxisLocal = mX.MultiplyVector(this.YAxisLocal),
                ZAxisLocal = mX.MultiplyVector(this.ZAxisLocal)
            };

            fr.CreateTransforms();

            return fr;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Degrees"></param>
        /// <returns></returns>
        public FrameOfReference LocalRotateY(int Degrees)
        {
            return LocalRotateY((float)Degrees * 0.0174533);
        }
        public FrameOfReference LocalRotateY(double Radians)
        {
            // Get a matrix for rotation about x
            Matrix4x4 mY = Matrix4x4.RotateY(Radians);
            /*
             * Create a new frame of reference that is rotated locally around the current X
             */
            FrameOfReference fr = new FrameOfReference()
            {
                OriginLocal = new Point3D(this.OriginLocal.X, this.OriginLocal.Y, this.OriginLocal.Z),
                XAxisLocal = mY.MultiplyVector(this.XAxisLocal),
                YAxisLocal = mY.MultiplyVector(this.YAxisLocal),
                ZAxisLocal = mY.MultiplyVector(this.ZAxisLocal)
            };

            fr.CreateTransforms();

            return fr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Degrees"></param>
        /// <returns></returns>
        public FrameOfReference LocalRotateZ(int Degrees)
        {
            return LocalRotateZ((float)Degrees * 0.0174533);
        }
        public FrameOfReference LocalRotateZ(double Radians)
        {
            // Get a matrix for rotation about x
            Matrix4x4 mZ = Matrix4x4.RotateZ(Radians);
            /*
             * Create a new frame of reference that is rotated locally around the current z
             */
            FrameOfReference fr = new FrameOfReference()
            {
                OriginLocal = new Point3D(this.OriginLocal.X, this.OriginLocal.Y, this.OriginLocal.Z),
                XAxisLocal = mZ.MultiplyVector(this.XAxisLocal),
                YAxisLocal = mZ.MultiplyVector(this.YAxisLocal),
                ZAxisLocal = mZ.MultiplyVector(this.ZAxisLocal)
            };

            fr.CreateTransforms();

            return fr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeltaX"></param>
        /// <param name="DeltaY"></param>
        /// <param name="DeltaZ"></param>
        /// <returns></returns>
        public FrameOfReference Translate(float DeltaX, float DeltaY, float DeltaZ)
            {
            /*
             * Return a new transform that will go from the world origin to a position that is translated from the current frame of reference.
             */
            // Treat the deltax,y and z as point in the world coordinate system and rotate it through the world->local transform

            Vector3D v = new Vector3D() { X = DeltaX, Y = DeltaY, Z = DeltaZ };


               v = WorldToLocalTransform.MultiplyVector(v);

                // Ok, now we have a vector in the new world space that represents a translation of the delta amount. The axis stay the same since it was a translation

                FrameOfReference frNew = new FrameOfReference()
                {
                    OriginLocal = new Point3D(this.OriginLocal.X + v.X, this.OriginLocal.Y + v.Y, this.OriginLocal.Z + v.Z),
                    XAxisLocal = this.XAxisLocal,
                    YAxisLocal = this.YAxisLocal,
                    ZAxisLocal = this.ZAxisLocal
                };

                frNew.CreateTransforms();

                return frNew;

            }

#if false
        protected Vector3D MultiplyVectorThroughMatrix(Vector3D v, Matrix3x3 m)
        {
            Vector3D vn = new Vector3D() { X = 0, Y = 0, Z = 0 };

            vn.X += v.X * m.M11;
            vn.X += v.Y * m.M12;
            vn.X += v.Z * m.M13;

            vn.Y += v.X * m.M21;
            vn.Y += v.Y * m.M22;
            vn.Y += v.Z * m.M23;

            vn.Z += v.X * m.M31;
            vn.Z += v.Y * m.M32;
            vn.Z += v.Z * m.M33;

            return vn;
        }
#endif


        protected void CreateTransforms()
        {
#if false
            OriginLocal = cub.Point3dArray[0];
            XAxisLocal = new Vector3d(OriginLocal, cub.Point3dArray[1]);
            YAxisLocal = new Vector3d(OriginLocal, cub.Point3dArray[3]);
            ZAxisLocal = Vector3d.CrossProduct(XAxisLocal, YAxisLocal);
#endif

            Vector3D.Normalize(XAxisLocal);
            Vector3D.Normalize(YAxisLocal);
            Vector3D.Normalize(ZAxisLocal);

            Vector3D X1 = XAxisWorld;
            Vector3D X1Prime = XAxisLocal;

            Vector3D X2 = YAxisWorld;
            Vector3D X2Prime = YAxisLocal;

            Vector3D X3 = ZAxisWorld;
            Vector3D X3Prime = ZAxisLocal;

            LocalToWorldTransform = new Matrix4x4()
            {
                M11 = (float)Vector3D.Dot(X1, X1Prime),
                M12 = (float)Vector3D.Dot(X1, X2Prime),
                M13 = (float)Vector3D.Dot(X1, X3Prime),
                M21 = (float)Vector3D.Dot(X2, X1Prime),
                M22 = (float)Vector3D.Dot(X2, X2Prime),
                M23 = (float)Vector3D.Dot(X2, X3Prime),
                M31 = (float)Vector3D.Dot(X3, X1Prime),
                M32 = (float)Vector3D.Dot(X3, X2Prime),
                M33 = (float)Vector3D.Dot(X3, X3Prime),
            };


            WorldToLocalTransform = new Matrix4x4()
            {
                M11 = (float)Vector3D.Dot(X1Prime, X1),
                M12 = (float)Vector3D.Dot(X1Prime, X2),
                M13 = (float)Vector3D.Dot(X1Prime, X3),
                M21 = (float)Vector3D.Dot(X2Prime, X1),
                M22 = (float)Vector3D.Dot(X2Prime, X2),
                M23 = (float)Vector3D.Dot(X2Prime, X3),
                M31 = (float)Vector3D.Dot(X3Prime, X1),
                M32 = (float)Vector3D.Dot(X3Prime, X2),
                M33 = (float)Vector3D.Dot(X3Prime, X3),
            };
        }

        public XElement GetProperties(string PropertyName = "")
        {
            return new XElement("frameofreference",
                new XAttribute("prop","frameofreference"),
                new XAttribute("Origin", string.Format("{0},{1},{2}", this.OriginLocal.X, this.OriginLocal.Y, this.OriginLocal.Z)),
                new XAttribute("XAxis", string.Format("{0},{1},{2}", this.XAxisLocal.X, this.XAxisLocal.Y, this.XAxisLocal.Z)),
                new XAttribute("YAxis", string.Format("{0},{1},{2}", this.YAxisLocal.X, this.YAxisLocal.Y, this.YAxisLocal.Z)),
                new XAttribute("ZAxis", string.Format("{0},{1},{2}", this.ZAxisLocal.X, this.ZAxisLocal.Y, this.ZAxisLocal.Z)),

                // APR 2018 Debug to pass value into render from driver
                new XAttribute("iteration",this.TestIteration)

                );
                
        }

        public bool LoadProperties(XElement ele, out string Message)
        {
            Message = "OK";
            float X, Y, Z;

            // APR 2018 Debug to pass value into render from driver
            XAttribute iteration = ele.Attribute("iteration");
            TestIteration = Convert.ToInt32(iteration.Value);

            XAttribute Origin = ele.Attribute("Origin");
            if (Origin != null)
            {
                if (!DecodeCoordinates(Origin.Value, out X, out Y, out Z, out Message))
                    return false;
                OriginLocal.X = X;
                OriginLocal.Y = Y;
                OriginLocal.Z = Z;
            }

            XAttribute XAxis = ele.Attribute("XAxis");
            if (XAxis != null)
            {
                if (!DecodeCoordinates(XAxis.Value, out X, out Y, out Z, out Message))
                    return false;
                XAxisLocal.X = X;
                XAxisLocal.Y = Y;
                XAxisLocal.Z = Z;
            }

            XAttribute YAxis = ele.Attribute("YAxis");
            if (YAxis != null)
            {
                if (!DecodeCoordinates(YAxis.Value, out X, out Y, out Z, out Message))
                    return false;
                YAxisLocal.X = X;
                YAxisLocal.Y = Y;
                YAxisLocal.Z = Z;
            }

            XAttribute ZAxis = ele.Attribute("ZAxis");
            if (ZAxis != null)
            {
                if (!DecodeCoordinates(ZAxis.Value, out X, out Y, out Z, out Message))
                    return false;
                ZAxisLocal.X = X;
                ZAxisLocal.Y = Y;
                ZAxisLocal.Z = Z;
            }
            /*
             * Create the word to local transforms
             */
            this.CreateTransforms();

            return true;
           
        }

        // Decode the list of coordinates
        bool DecodeCoordinates(string values, out float X, out float Y, out float Z, out string message)
        {
            X = 0;
            Y = 0;
            Z = 0;
            message = "OK";

            string[] list = values.Split(",".ToCharArray());
            if (list.Length != 3)
            {
                message = "Expected 3 coordinates in " + values;
                return false;
            }
            if (!float.TryParse(list[0],out X))
            {
                message = "Error decoding X as a float: " + list[0];
                return false;
            }
            if (!float.TryParse(list[1], out Y))
            {
                message = "Error decoding Y as a float: " + list[1];
                return false;
            }
            if (!float.TryParse(list[2], out Z))
            {
                message = "Error decoding Z as a float: " + list[2];
                return false;
            }

            return true;
        }

    }


    }

    

