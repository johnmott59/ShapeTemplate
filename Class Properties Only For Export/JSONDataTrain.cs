using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib
{
    /*
     * The JSON data train is used by templates to export and import data from the template in JSON format
     */
    public partial class JSONDataTrain
    {
        public JSONDataCarriage[] JSONDataCarriageArray { get; set; }

        public JSONDataTrain()
        {
            JSONDataCarriageArray = new JSONDataCarriage[0];
        }
       
    }
}
