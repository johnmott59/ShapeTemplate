
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;
using System.Web.Script.Serialization;

namespace ShapeTemplateLib.Templates.User0 
{
    public partial class TestTemplateClass 
    {


     public override void LoadJSONDataTrain(string sDataTrain)
     {
		JavaScriptSerializer ser = new JavaScriptSerializer();
        /*
         * Decode this data train
         */
        JSONDataTrain oDataTrain = ser.Deserialize<JSONDataTrain>(sDataTrain);
        /*
         * Load each of the values from their carriage
         */
        JSONDataCarriage oCarriage;

 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(LSListList)).FirstOrDefault();
            if (oCarriage != null)
            {
                LSListList = ser.Deserialize<LineSegmentList[]>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(ID)).FirstOrDefault();
            if (oCarriage != null)
            {
                ID = ser.Deserialize<string>(oCarriage.fieldvalue);
            }
	
		
	}

	}
}
