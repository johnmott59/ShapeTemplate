using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace ShapeTemplateLib
{
    /// <summary>
    /// 
    /// The BoundaryRoot is the base class of BoundaryRectangle, BoundaryEllipse and BoundaryLineSegment
    /// The Panel and Hole classes use Boundaries to define their outlines, but will instantiate the type of 
    /// boundary that makes sense, an ellipise, rectangle or line segment.
    /// 
    /// Points for boundaries are defined in the XY plane with left (-x), right (+x), up (+y) and down (-y) with +z facing
    /// outward towards the viewer.
    /// 
    /// Points for boundaries should be wound counter-clockwise. This happens automatically for rectangle
    /// and ellipse shapes. 
    /// An example triangle that is wound correctly would be (0,0) -> (50,0) -> (25,25)
    /// 
    /// Inheritance and challenges in I/O
    /// 
    /// Classes that have and use inheritance present a special challenge in two circumstances.
    /// 
    /// SAVING AND READING FROM XML
    /// 
    /// For a class like boundaryroot, it has a subclass like BoundaryRectangle or BoundaryEllipse. 
    /// 
    /// When we read and write XML there are two situations we will encounter when these classes are used as properties in 
    /// other classes to represent a boundary
    /// 
    ///     a. When we declare a property of type BoundaryRoot
    ///        We do this when we want the boundary to be flexible; it can be any subclass of BoundaryRoot, like rectangle
    ///        or ellispe or polygon. We want the XML generated and loaded to be a value BoundaryRoot child type and have that
    ///        type figured out at load time or save time.
    ///        
    ///     b. When we declare a specific subclass of BoundaryRoot as a property, like a BoundaryRectangle. In this case we know 
    ///        specifically what type we want, because we declared it that way, and we don't want any additional information 
    ///        associated with it. It should be an error for there to be different XML than what we declared. We said we wanted
    ///        a rectangle, that's what we want.
    ///        
    /// To handle these two situations the BoundaryRoot object has additional methods WrapAndGetProperties and UnwrapAndLoadProperties. 
    /// These two methods are used by a class that declared BoundaryRoot as an instance type. It knows its a BoundaryRoot and
    /// therefore may contain any valid subclass, and it needs to have the XML reflect that. The resulting XML has a 
    /// <boundary> </boundary> wrapper around the specific child class. The class that declares the BoundaryRoot property
    /// will not call GetProperties or LoadProperties, they will call WrapAndGetProperties and UnwrapAndLoadProperties; they
    /// will know to do that becuase its their property, they declared it and they know what they want done with it.
    /// 
    /// A class the defines an instance of BoundaryRectangle will continue to call GetProperties and LoadProperties, just as before.
    /// The resulting XML will not be contained in a wrapper.
    /// 
    /// SAVING AND READING INTO AND FROM THE JSON DATA TRAIN
    /// 
    /// If a property is declared as a BoundaryRectangle or other subclass of a BoundaryRoot then nothing else need be done, 
    /// serialization and deserialization work just as they do with all types.
    /// 
    /// The data train presents a special challenge for properties declared as BoundaryRoot because both javascript and .net need 
    /// to know the correct type to deserialize from, and type information
    /// isn't saved through JSON serialization and deserilization. However, the BoundaryRoot class has a property, 'BoundaryType',
    /// that all subclass constructors initialize as 'rectangle' or whatever, so we always have a handle on the intended type.
    /// 
    /// From .NET, serializing an object of either BoundaryRoot or a child works the same; it will pick up all properties. If you
    /// serialize a Boundaryroot object that was assigned to a BoundaryRectangle it looks like this:
    /// 
    /// {"Width":100,"Height":100,"ZDepth":0,"BoundaryType":"rectangle"}
    /// 
    /// Inside Javascript when the datatrain
    /// is being unloaded, JS can check the property, 'BoundaryType', and use that to reload the object as an instance of 
    /// the correct type via the CopyFrom routine for the appropriate type. Then it will have the full object 
    /// of the correct type. It can serialize this object back into the data train when there is a 'save' operation.
    /// 
    /// on the deserialize side back in .NET, unloading a property declared as boundaryroot can use this logic:
    /// 
    /// BoundaryRoot jbr = ser.Deserialize<BoundaryRoot>(json);  // This will only pull the 'BoundaryType' property
    /// switch (jbr.BoundaryType)
    /// {
    ///     case "rectangle":
    ///         BoundaryRectangle orect = ser.Deserialize<BoundaryRectangle>(json);
    ///         break;
    ///     case (other subclasses)
    /// }

    /// </summary>
    [HelpItem(eItemFlavor.Data,"boundary")]
    public partial class BoundaryRoot : ILoadAndSaveProperties
    {
        // This value is set by the subclass
        public string BoundaryType { get; set; } = "";

        /// <summary>
      
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        public XElement WrapAndGetProperties(string PropertyName="")
        {
            XElement ele = new XElement("boundary", new XAttribute("prop", PropertyName));
            
            // Get the overridden properties and place them inside the boundary
            XElement oChild = GetProperties("");
            ele.Add(oChild);

            return ele;
        }
        /*
         * This is a static method because it needs to return a new instance of a boundary based class
         */
        public static bool UnwrapAndLoadProperties(XElement ele, out BoundaryRoot bound, out string message)
        {
            message = "OK";
            // this element is a boundary element. Its child is the one we want
            // Based on the child, build and return an object

            XElement child = (XElement)ele.FirstNode;

            switch (child.Name.LocalName)
            {
                case "boundary":
                    bound = new BoundaryRoot();
                    return true;
                case "boundaryrectangle":
                    bound = new BoundaryRectangle();
                    return bound.LoadProperties(child, out message);
                case "boundaryellipse":
                    bound = new BoundaryEllipse();
                    return bound.LoadProperties(child, out message);
                case "boundarypolygon":
                    bound = new BoundaryPolygon();
                    return bound.LoadProperties(child, out message);
                default:
                    message = $"Expected boundary value boundaryrectangle, boundaryellipse or boundarypolygon, got {child.Name.LocalName}";
                    bound = null;
                    return false;
            }
            
        }

        public virtual XElement GetProperties(string PropertyName="")
        {
            return new XElement("boundary",new XAttribute("prop",PropertyName));
        }

        public virtual bool LoadProperties(XElement ele,out string message)
        {
            message = "OK";
            return true;
        }
#if false
        public static bool LoadXElement(XElement ele,out BoundaryRoot bound, out string message)
        {
            if (ele == null)
            {
                message = $"Missing boundary element";
                bound = null;
                return false;
            }

            message = "OK";

            // Based on the child, build and return an object
            XElement child = (XElement) ele.FirstNode;

            switch (child.Name.LocalName)
            {
                case "rectangle":
                    bound = new BoundaryRectangle();
                    return bound.LoadProperties(child, out message);
                case "ellipse":
                    bound = new BoundaryEllipse();
                    return bound.LoadProperties(child, out message);
                case "linesegment":
                    bound = new BoundaryPolygon();
                    return bound.LoadProperties(child, out message);
                default:
                    message = $"Expected boundary value rectangle, ellipse or line, got {child.Name.LocalName}";
                    bound = null;
                    return false;
            }

        }
#endif

    }


}