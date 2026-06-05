using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib;

namespace ShapeTemplateLib
{
    public partial class Group 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public Group RotateX(float radians)
        {
            Group g = new Group();
            this.GroupChildrenList.Add(g);

            g.TransformType =  eTransformType.RotateX;
            g.TransformValue = radians;

            return g;
        }

        public Group RotateX(int degrees)
        {
            return RotateX((float)degrees * 0.0174533F);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public Group RotateY(float radians)
        {
            Group g = new Group();
            this.GroupChildrenList.Add(g);

            g.TransformType = eTransformType.RotateY;
            g.TransformValue = radians;

            return g;
        }

        public Group RotateY(int degrees)
        {
            return RotateY((float)degrees * 0.0174533F);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>

        public Group RotateZ(float radians)
        {
            Group g = new Group();
            this.GroupChildrenList.Add(g);

            g.TransformType = eTransformType.RotateZ;
            g.TransformValue = radians;

            return g;
        }

        public Group RotateZ(int degrees)
        {
            return RotateZ((float)degrees * 0.0174533F);
        }
    }
}
