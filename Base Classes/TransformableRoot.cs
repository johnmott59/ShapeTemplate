using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib
{
    /*
     * The transformable root provides mechanisms to allow this object to have local transforms and frames of reference.
     */
    public class TransformableRoot : CompilableRoot
    {
        /// <summary>
        /// The frame of reference is the orientation of this object in 3D space, its origin and its local X,Y and Z axis
        /// The difference between a local transform and a frame of reference is that the localtransform and a frame of reference
        /// is that the frame of reference is used to 'set' the object in 3D space by giving it a new X,Y and Z and origin.
        /// The frames of reference are used to chain objects together in a spatial relationship. imagine an arm object that has 
        /// an upper arm, and a forearm that exists at a specific angle to the upper arm. The forearm object is generated according
        /// to its local X,Y,Z. Its X,Y,Z axis are then re-oriented towards the new frame of reference. While it is technically possible
        /// to describe all operations in terms of transformation matrices it can get messy when you think about the local generation
        /// of an object and any local transformations and then combine those in the correct order to place them in space. It is more
        /// intuitive to think about the rearrangement of the XYZ axis and the origin as a way of dropping an object into space.
        /// </summary>
        [HelpProperty]
        public FrameOfReference oFrameOfReference { get; set; } = new FrameOfReference();

        /// <summary>
        /// Objects are generally rendred around the normal global x,y,z azis at the origin, although some, like
        /// the flatmesh, can be positioned directly in 3D space. However, its very useful for the creation of complex
        /// scenes to allow an object to be locally transformed after its generated. This local transformation is relative
        /// to the work x,y, and z and can be used to translate, rotate or scale the object.
        /// </summary>
        /// [HelpProperty]
        public Matrix4x4 LocalTransform { get; set; } = new Matrix4x4();


   
    }

}
