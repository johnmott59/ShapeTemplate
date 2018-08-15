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

    public partial class FlatMesh 
    {
     
        public override XElement GetProperties(string PropertyName = "")
        {
            XElement flatmesh = new XElement("flatmesh", new XAttribute("prop", PropertyName));

            // Add the boundary, which can be any boundaryroot derived shape
            flatmesh.Add(Boundary.WrapAndGetProperties("boundary"));

            if (LocalTransform != null)
            {
                flatmesh.Add(LocalTransform.GetProperties("localtransform"));
            }

            if (oFrameOfReference != null)
            {
                flatmesh.Add(oFrameOfReference.GetProperties("frameofreference"));
            }

            if (MeshDisplayProperties != null)
            {
                flatmesh.Add(MeshDisplayProperties.GetProperties("meshdisplayproperties"));
            }

            if (Offset == null)
            {
                // add a default value
                flatmesh.Add(new Point3D(0, 0, 0).GetProperties("offset"));
            } else
            {
                flatmesh.Add(Offset.GetProperties("offset"));
            }

            if (XAxis == null)
            {
                // Add a default value
                flatmesh.Add(new Vector3D(1f, 0f, 0f).GetProperties("xaxis"));
            } else
            {
                flatmesh.Add(XAxis.GetProperties("xaxis"));
            }

            /*
             * Add the list of panel holes
             */
            XElement xHole = new XElement("holelist", new XAttribute("prop","holelist"));
            flatmesh.Add(xHole);
            foreach (Hole h in HoleList)
            {
                xHole.Add(h.GetProperties());
            }

            return flatmesh;
        }


}

}
    
