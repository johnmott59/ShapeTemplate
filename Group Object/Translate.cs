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
        public Group TranslateY(float value)
        {
            if (value == 0) return this;

            return this.GetChildGroup(eTransformType.TranslateY, value);
        }

        public Group TranslateX(float value)
        {
            if (value == 0) return this;

            return this.GetChildGroup(eTransformType.TranslateX, value);
        }

        public Group TranslateZ(float value)
        {
            if (value == 0) return this;

            return this.GetChildGroup(eTransformType.TranslateZ, value);
        }
        /// <summary>
        /// Shorthand routine to build a translated tree.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <returns></returns>
        public Group Translate(float X, float Y, float Z)
        {
            Group gResult = null;

            if (X != 0)
            {
                gResult = this.GetChildGroup(eTransformType.TranslateX, X);
            }

            if (Y != 0)
            {
                if (gResult == null)
                {
                    gResult = this.GetChildGroup(eTransformType.TranslateY, Y);
                } else
                {
                    gResult = gResult.GetChildGroup(eTransformType.TranslateY,Y);
                }
            }

            if (Z != 0)
            {
                if (gResult == null)
                {
                    gResult = this.GetChildGroup(eTransformType.TranslateZ, Z);
                }
                else
                {
                    gResult = gResult.GetChildGroup(eTransformType.TranslateZ, Z);
                }
            }

            if (gResult == null) return this;

            else return gResult;
        }
    }
}
