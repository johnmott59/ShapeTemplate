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
    public partial class SingleRoomBuilding
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
                    fieldname = "Thickness",
                    fieldvalue = ser.Serialize(Thickness )
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

            return new JSONDataTrain() { JSONDataCarriageArray = list.ToArray() };

        }

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
            JSONDataCarriage oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(VerticalScale)).FirstOrDefault();
            if (oCarriage != null)
            {
                VerticalScale = ser.Deserialize<float>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(HorizontalScale)).FirstOrDefault();
            if (oCarriage != null)
            {
                HorizontalScale = ser.Deserialize<float>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(Width)).FirstOrDefault();
            if (oCarriage != null)
            {
                Width = ser.Deserialize<float>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(Thickness)).FirstOrDefault();
            if (oCarriage != null)
            {
                Thickness = ser.Deserialize<float>(oCarriage.fieldvalue);
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

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(RoofOffset)).FirstOrDefault();
            if (oCarriage != null)
            {
                RoofOffset = ser.Deserialize<float>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(Door)).FirstOrDefault();
            if (oCarriage != null)
            {
                 Door = Hole.LoadFromJSON(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(FrontWindow)).FirstOrDefault();
            if (oCarriage != null)
            {
                FrontWindow = Hole.LoadFromJSON(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(LeftWindow)).FirstOrDefault();
            if (oCarriage != null)
            {
                LeftWindow = Hole.LoadFromJSON(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(RearWindow)).FirstOrDefault();
            if (oCarriage != null)
            {
                RearWindow = Hole.LoadFromJSON(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(RightWindow)).FirstOrDefault();
            if (oCarriage != null)
            {
                RightWindow = Hole.LoadFromJSON(oCarriage.fieldvalue);
            }
        }

    }
}
