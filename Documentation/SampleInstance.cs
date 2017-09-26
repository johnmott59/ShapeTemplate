using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    /// <summary>
    /// The sample instance class is used to provide one or more examples of a shape or template
    /// </summary>
    public class SampleInstance
    {
        /// <summary>
        /// A short description that could be included in a drop-down
        /// </summary>
        public string ShortDescription { get; set; }      
          
        /// <summary>
        /// A longer description that can discuss this example
        /// </summary>
        public string LongDescription { get; set; }

        /// <summary>
        /// The Xelement that contains the example
        /// </summary>
        public XElement Sample { get; set;}
    }

    /// <summary>
    /// Classes that can return sample instances implement this interface
    /// </summary>
    public interface IGetSample
    {
        /// <summary>
        /// Return a list of sample instances for documentation purposes
        /// </summary>
        /// <returns></returns>
        List<SampleInstance> GetSampleList();
    }
}
