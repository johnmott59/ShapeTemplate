
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
    public partial class FloorWithRooms 
    {

        public override JSONDataTrain GetJSONDataTrain()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            List<JSONDataCarriage> list = new List<JSONDataCarriage>()
            {


			};

			return new JSONDataTrain() { JSONDataCarriageArray = list.ToArray() };
		}


	}
}
