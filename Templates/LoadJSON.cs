
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
    public partial class GearBaseTemplate 
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

 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(HorizontalScale)).FirstOrDefault();
            if (oCarriage != null)
            {
                HorizontalScale = ser.Deserialize<float>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(VerticalScale)).FirstOrDefault();
            if (oCarriage != null)
            {
                VerticalScale = ser.Deserialize<float>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(Width)).FirstOrDefault();
            if (oCarriage != null)
            {
                Width = ser.Deserialize<float>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(Length)).FirstOrDefault();
            if (oCarriage != null)
            {
                Length = ser.Deserialize<float>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(Height)).FirstOrDefault();
            if (oCarriage != null)
            {
                Height = ser.Deserialize<float>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(Thickness)).FirstOrDefault();
            if (oCarriage != null)
            {
                Thickness = ser.Deserialize<float>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(RoofOffset)).FirstOrDefault();
            if (oCarriage != null)
            {
                RoofOffset = ser.Deserialize<float>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(Door)).FirstOrDefault();
            if (oCarriage != null)
            {
                Door = ser.Deserialize<SRBRectHole>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(FrontWindow)).FirstOrDefault();
            if (oCarriage != null)
            {
                FrontWindow = ser.Deserialize<SRBRectHole>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(LeftWindow)).FirstOrDefault();
            if (oCarriage != null)
            {
                LeftWindow = ser.Deserialize<SRBRectHole>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(RearWindow)).FirstOrDefault();
            if (oCarriage != null)
            {
                RearWindow = ser.Deserialize<SRBRectHole>(oCarriage.fieldvalue);
            }
	 
	        oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(RightWindow)).FirstOrDefault();
            if (oCarriage != null)
            {
                RightWindow = ser.Deserialize<SRBRectHole>(oCarriage.fieldvalue);
            }
	
	}

	}
}
