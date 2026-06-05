
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

        public override JSONDataTrain GetJSONDataTrain()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            List<JSONDataCarriage> list = new List<JSONDataCarriage>()
            {

 
			new JSONDataCarriage() {
				fieldname = "LSListList",
                fieldvalue = ser.Serialize(LSListList )
			},
	 
			new JSONDataCarriage() {
				fieldname = "ID",
                fieldvalue = ser.Serialize(ID )
			},
	
			};

			return new JSONDataTrain() { JSONDataCarriageArray = list.ToArray() };
		}


	}
}
