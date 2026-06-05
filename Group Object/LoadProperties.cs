using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib
{
    /// <summary>
    /// The group is a container class for sets of objects.
    /// In the simplest case a group is just a list of meshes that have their own specifications and the group is kind of like
    /// and html div; just a container. 
    /// 
    /// The group object contains a frame of reference so the next level of complexity is that a set of objects are oriented a certain way
    /// via a single frame of reference.
    /// 
    /// A group object can hold other group objects, so the next level of complexity is that a tree structure of related objects that have 
    /// a spatial relationship. A group node can have multiple items at a particular frame of reference and a number of group children, 
    /// each with a different frame of reference, and each with their own set of meshes.
    /// 
    /// The API will process a group object by doing a depth first search, accruing the different frames of reference as it descends, so that
    /// it can provide the leaf mesh (or template) with its final frame of reference.
    /// The internal group struct will maintain the frameofference, because it will be instantiating
    
    /// maybe compilation will be placing the final frame of reference values in the objects and templates.
    /// we will have a list of meshes and a list of templates.
    /// the advantage of a compile would be that final frames of reference would go into the API
    /// but the group structure would go in as well the compile would have stripped the tranformations?
    /// 
    /// somehow it seems useful to be able to use compile() as a way of computing the final frames of reference.
    /// when the API is called it will call compile again anyway, but it might be useful to get those transformation
    /// outside calling the API.
    /// 
    /// if compile is called the nested objects will get the frames of reference and the group nodes will be cleared.
    /// if getproperties is called the group objects will retain the transformations
    /// 
    /// the group node itself doens't have a frameofrefernce object. The compilation process will build and maintain one as it
    /// descends the object tree for the purposes of setting them in the node.
    /// 
    /// aha, because there is no rule that you cannot build your own orientation and use groups to organize.
    /// if a group node has no transformation then the mesh values are not overridden
    /// 
    /// if group node has transformations then a frame of reference is built and assigned to the mesh, overwriting anything that's there.
    /// 
    /// 
    /// </summary>
    public partial class Group : CompilableRoot
    {
        public override bool LoadProperties(XElement ele, out string message)
        {
            // Load the group transform properties
            message = "OK";

            XAttribute xform = ele.Attributes().Where(m => m.Name == "transform").FirstOrDefault();
            XAttribute xValue = ele.Attributes().Where(m => m.Name == "value").FirstOrDefault();
            if (xform != null)
            {
                switch (xform.Value)
                {
                    case "rx":
                        TransformType = eTransformType.RotateX;
                        break;
                    case "ry":
                        TransformType = eTransformType.RotateY;
                        break;
                    case "rz":
                        TransformType = eTransformType.RotateZ;
                        break;
                    case "tx":
                        TransformType = eTransformType.TranslateX;
                        break;
                    case "ty":
                        TransformType = eTransformType.TranslateY;
                        break;
                    case "tz":
                        TransformType = eTransformType.TranslateZ;
                        break;
                    default:
                        message = "Invalid value for transform type";
                        return false;
                }

                double d = 0;
                if (!double.TryParse(xValue.Value,out d))
                {
                    message = "Invalid value for group transform value";
                    return false;
                }
                TransformValue = (float) d;
               
            }
            /*
             * Load the objects at this level, recursing when needed
             */
             foreach (XElement child in ele.Elements())
            {
                /*
                 * Load a group node and recurse
                 */
                 if (child.Name.LocalName == "group")
                {
                    Group g = new Group();
                    if (!g.LoadProperties(child,out message))
                    {
                        return false;
                    }
                    this.GroupChildrenList.Add(g);
                    continue;
                }

                if (child.Name.LocalName == "flatmesh") {
                    FlatMesh fm = new FlatMesh();

                    if (!fm.LoadProperties(child,out message))
                    {
                        return false;
                    }
                    this.MeshBaseClassList.Add(fm);
                }

                if (child.Name.LocalName == "panel")
                {
                    Panel p = new Panel();
                    if (!p.LoadProperties(child,out message))
                    {
                        return false;
                    }
                    this.MeshBaseClassList.Add(p);
                }
            }


            return true;
        
        }
    }
}
