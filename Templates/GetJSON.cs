
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

        public override JSONDataTrain GetJSONDataTrain()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            List<JSONDataCarriage> list = new List<JSONDataCarriage>()
            {

 
			new JSONDataCarriage() {
				fieldname = "HorizontalScale",
                fieldvalue = ser.Serialize(HorizontalScale )
			},
	 
			new JSONDataCarriage() {
				fieldname = "VerticalScale",
                fieldvalue = ser.Serialize(VerticalScale )
			},
	 
			new JSONDataCarriage() {
				fieldname = "Width",
                fieldvalue = ser.Serialize(Width )
			},
	 
			new JSONDataCarriage() {
				fieldname = "Length",
                fieldvalue = ser.Serialize(Length )
			},
	 
			new JSONDataCarriage() {
				fieldname = "Height",
                fieldvalue = ser.Serialize(Height )
			},
	 
			new JSONDataCarriage() {
				fieldname = "Thickness",
                fieldvalue = ser.Serialize(Thickness )
			},
	 
			new JSONDataCarriage() {
				fieldname = "RoofOffset",
                fieldvalue = ser.Serialize(RoofOffset )
			},
	 
			new JSONDataCarriage() {
				fieldname = "Door",
                fieldvalue = ser.Serialize(Door )
			},
	 
			new JSONDataCarriage() {
				fieldname = "FrontWindow",
                fieldvalue = ser.Serialize(FrontWindow )
			},
	 
			new JSONDataCarriage() {
				fieldname = "LeftWindow",
                fieldvalue = ser.Serialize(LeftWindow )
			},
	 
			new JSONDataCarriage() {
				fieldname = "RearWindow",
                fieldvalue = ser.Serialize(RearWindow )
			},
	 
			new JSONDataCarriage() {
				fieldname = "RightWindow",
                fieldvalue = ser.Serialize(RightWindow )
			},
	
			};
		}


	}
}
