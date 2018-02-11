
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
    public partial class StraightStairs
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


            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(VerticalDistance)).FirstOrDefault();
            if (oCarriage != null)
            {
                VerticalDistance = ser.Deserialize<int>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(HorizontalDistance)).FirstOrDefault();
            if (oCarriage != null)
            {
                HorizontalDistance = ser.Deserialize<int>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(LateralDistance)).FirstOrDefault();
            if (oCarriage != null)
            {
                LateralDistance = ser.Deserialize<int>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(StairCount)).FirstOrDefault();
            if (oCarriage != null)
            {
                StairCount = ser.Deserialize<int>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(Width)).FirstOrDefault();
            if (oCarriage != null)
            {
                Width = ser.Deserialize<int>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(Rise)).FirstOrDefault();
            if (oCarriage != null)
            {
                Rise = ser.Deserialize<int>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(Run)).FirstOrDefault();
            if (oCarriage != null)
            {
                Run = ser.Deserialize<int>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(LeftSideTexture)).FirstOrDefault();
            if (oCarriage != null)
            {
                LeftSideTexture = ser.Deserialize<string>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(RightSideTexture)).FirstOrDefault();
            if (oCarriage != null)
            {
                RightSideTexture = ser.Deserialize<string>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(StairTexture)).FirstOrDefault();
            if (oCarriage != null)
            {
                StairTexture = ser.Deserialize<string>(oCarriage.fieldvalue);
            }


        }

    }
}
