
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

        public override JSONDataTrain GetJSONDataTrain()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            List<JSONDataCarriage> list = new List<JSONDataCarriage>()
            {


            new JSONDataCarriage() {
                fieldname = "VerticalDistance",
                fieldvalue = ser.Serialize(VerticalDistance )
            },

            new JSONDataCarriage() {
                fieldname = "HorizontalDistance",
                fieldvalue = ser.Serialize(HorizontalDistance )
            },

            new JSONDataCarriage() {
                fieldname = "LateralDistance",
                fieldvalue = ser.Serialize(LateralDistance )
            },

            new JSONDataCarriage() {
                fieldname = "StairCount",
                fieldvalue = ser.Serialize(StairCount )
            },

            new JSONDataCarriage() {
                fieldname = "Width",
                fieldvalue = ser.Serialize(Width )
            },

            new JSONDataCarriage() {
                fieldname = "Rise",
                fieldvalue = ser.Serialize(Rise )
            },

            new JSONDataCarriage() {
                fieldname = "Run",
                fieldvalue = ser.Serialize(Run )
            },

            new JSONDataCarriage() {
                fieldname = "LeftSideTexture",
                fieldvalue = ser.Serialize(LeftSideTexture )
            },

            new JSONDataCarriage() {
                fieldname = "RightSideTexture",
                fieldvalue = ser.Serialize(RightSideTexture )
            },

            new JSONDataCarriage() {
                fieldname = "StairTexture",
                fieldvalue = ser.Serialize(StairTexture )
            },

            };

            return new JSONDataTrain() { JSONDataCarriageArray = list.ToArray() };
        }


    }
}
