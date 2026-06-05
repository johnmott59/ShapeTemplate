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
        public override XElement Compile()
        {
            /*
             * Start with a global frame of reference
             */
            return CompileLevel(new FrameOfReference());
        }

        /*
         * If a group is part of a template or a higher level group node then we will want to know its frame of reference
         * as a base. If we're processing a scene file and encounter a group node as a highest node then its frame is global
         * 
         * Compilation will build a new frame of reference at the current nesting level based on the transform at this level
         * This entry point is used internally to build the tree but can also be called if a group is, for example, a member
         * of a template
         */
        public XElement CompileLevel(FrameOfReference oCurrentFrame)
        {
            // When we compile we strip the transformation information

            XElement xGroup = new XElement("group");

            FrameOfReference fr = new FrameOfReference();
            if (TransformType != null)
            {
                switch (TransformType)
                {
                    case eTransformType.RotateX:
                        fr = oCurrentFrame.LocalRotateX(TransformValue);
                        break;
                    case eTransformType.RotateY:
                        fr = oCurrentFrame.LocalRotateY(TransformValue);
                        break;
                    case eTransformType.RotateZ:
                        fr = oCurrentFrame.LocalRotateZ(TransformValue);
                        break;
                    case eTransformType.TranslateX:
                        fr = oCurrentFrame.Translate(this.TransformValue, 0, 0);
                        break;
                    case eTransformType.TranslateY:
                        fr = oCurrentFrame.Translate(0, this.TransformValue, 0);
                        break;
                    case eTransformType.TranslateZ:
                        fr = oCurrentFrame.Translate(0, 0, this.TransformValue);
                        break;
                }
            } else {
                    // Create a copy of the current frame of reference to pass down
                    string message = "";
                    XElement cfr = oCurrentFrame.GetProperties();
                    fr.LoadProperties(cfr, out message);
            }
            /*
             * Pass this frame of reference to child groups
             */
            foreach (Group gc in GroupChildrenList)
            {
                XElement nested = gc.CompileLevel(fr);

                // Add the children of the group node to this level. This will flatten out the group struct
                foreach (XElement ch in nested.Elements())
                {
                    xGroup.Add(ch);
                }
            }
            /*
             * Assign this frame of reference to all objects at this level
             */
             foreach (TransformableRoot tr in this.MeshBaseClassList)
             {
                tr.oFrameOfReference = fr;
                xGroup.Add(tr.Compile());
             }
             /*
              * Assign this frame of reference to all templates at this level
              */
              foreach (TemplateBaseClass tb in this.TemplateList)
            {
                tb.oFrameOfReference = fr;
                xGroup.Add(tb.Compile());
            }

            return xGroup;
        }
    }
}
