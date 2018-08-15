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
        public override bool LoadProperties(XElement ele, out string message)
        {
            if (ele == null)
            {
                message = $"Missing panel element";
                return false;
            }

            XElement xBoundary = Utilities.GetNamedElementWithPropAttribute(ele, "boundary");
            message = "OK";
            BoundaryRoot b;
            if (!BoundaryRoot.UnwrapAndLoadProperties(xBoundary, out b, out message)) return false;
            this.Boundary = b;

            XElement xLocalTransform = Utilities.GetNamedElementWithPropAttribute(ele, "matrix4x4","localtransform");
            if (xLocalTransform != null)
            {
                if (!LocalTransform.LoadProperties(xLocalTransform,out message)) return false;
            }

            XElement xFrameOfReference = Utilities.GetNamedElementWithPropAttribute(ele, "frameofreference");  
            //ele.Element("frameofreference");
            if (xFrameOfReference != null)
            {
                if (!oFrameOfReference.LoadProperties(xFrameOfReference,out message)) return false;
            }

            XElement xDisplayProperties = Utilities.GetNamedElementWithPropAttribute(ele, "meshdisplayproperties");
            if (xDisplayProperties != null)
            {
                if (!MeshDisplayProperties.LoadProperties(xDisplayProperties, out message)) return false;
            }

            XElement xOffset = Utilities.GetNamedElementWithPropAttribute(ele,"point3d", "offset");
            if (xOffset != null)
            {
                if (!Offset.LoadProperties(xOffset, out message)) return false;
            }

            XElement xXAxis = Utilities.GetNamedElementWithPropAttribute(ele, "vector3d", "xaxis");
            if (xXAxis != null)
            {
                if (!XAxis.LoadProperties(xXAxis, out message)) return false;
            }

            XElement xHoleList = Utilities.GetNamedElementWithPropAttribute(ele, "holelist");
            /*
             * If there are holes, add them
             */
            if (xHoleList != null)
            {
                List<Hole> list = new List<Hole>();
                foreach (XElement xh in xHoleList.Elements("hole"))
                {
                    Hole h = new Hole();
                    if (!h.LoadProperties(xh, out message)) return false;
                    list.Add(h);
                }
                HoleList = list.ToArray();
            }

            return true;
        }
}

}
    
