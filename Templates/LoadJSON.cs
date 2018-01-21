
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
    public partial class xxGearBase 
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

 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(Scale)).FirstOrDefault();
            if (oCarriage != null)
            {
                Scale = ser.Deserialize<float>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(ToothCount)).FirstOrDefault();
            if (oCarriage != null)
            {
                ToothCount = ser.Deserialize<int>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(ToothShape)).FirstOrDefault();
            if (oCarriage != null)
            {
                ToothShape = ser.Deserialize<Point2DContainer>(oCarriage.fieldvalue);
            }
	
		
	}

	}
}