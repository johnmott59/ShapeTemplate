
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
    public partial class GearBase 
    {

        public override JSONDataTrain GetJSONDataTrain()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            List<JSONDataCarriage> list = new List<JSONDataCarriage>()
            {

 
			new JSONDataCarriage() {
				fieldname = "Scale",
                fieldvalue = ser.Serialize(Scale )
			},
	 
			new JSONDataCarriage() {
				fieldname = "ToothCount",
                fieldvalue = ser.Serialize(ToothCount )
			},
	 
			new JSONDataCarriage() {
				fieldname = "ToothShape",
                fieldvalue = ser.Serialize(ToothShape )
			},
	
			};

			return new JSONDataTrain() { JSONDataCarriageArray = list.ToArray() };
		}


	}
}
