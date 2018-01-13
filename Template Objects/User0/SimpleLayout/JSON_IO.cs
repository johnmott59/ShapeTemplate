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
    public partial class SimpleLayout 
    {

        public override JSONDataTrain GetJSONDataTrain()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            List<JSONDataCarriage> list = new List<JSONDataCarriage>()
            {
                 new JSONDataCarriage()
                 {
                      fieldname="VerticalScale",
                      fieldvalue=ser.Serialize(VerticalScale)
                 },
                 new JSONDataCarriage()
                 {
                     fieldname="HorizontalScale",
                     fieldvalue =ser.Serialize(HorizontalScale)
                 },
                 new JSONDataCarriage()
                 {
                     fieldname="EdgeList",
                     fieldvalue=ser.Serialize(EdgeList)
                 },
                 new JSONDataCarriage()
                 {
                     fieldname="VertexList",
                     fieldvalue=ser.Serialize(VertexList)
                 },
                 new JSONDataCarriage()
                 {
                     fieldname="HoleGroupList",
                     fieldvalue=ser.Serialize(HoleGroupList)
                 },
                 new JSONDataCarriage()
                 {
                     fieldname="BoundaryRectangleList",
                     fieldvalue=ser.Serialize(BoundaryRectangleList)
                 },
                 new JSONDataCarriage()
                 {
                     fieldname="BoundaryEllipseList",
                     fieldvalue=ser.Serialize(BoundaryEllipseList)
                 },
                  new JSONDataCarriage()
                 {
                     fieldname="BoundaryPolygonList",
                     fieldvalue=ser.Serialize(BoundaryPolygonList)
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

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(EdgeList)).FirstOrDefault();
            if (oCarriage != null)
            {
                EdgeList = ser.Deserialize<List<Edge>>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(VertexList)).FirstOrDefault();
            if (oCarriage != null)
            {
                VertexList = ser.Deserialize<List<Vertex>>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(HoleGroupList)).FirstOrDefault();
            if (oCarriage != null)
            {
                HoleGroupList = ser.Deserialize<List<HoleGroup>>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(BoundaryRectangleList)).FirstOrDefault();
            if (oCarriage != null)
            {
                BoundaryRectangleList = ser.Deserialize<List<BoundaryRectangle>>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(BoundaryEllipseList)).FirstOrDefault();
            if (oCarriage != null)
            {
                BoundaryEllipseList = ser.Deserialize<List<BoundaryEllipse>>(oCarriage.fieldvalue);
            }

            oCarriage = oDataTrain.JSONDataCarriageArray.Where(m => m.fieldname == nameof(BoundaryPolygonList)).FirstOrDefault();
            if (oCarriage != null)
            {
                BoundaryPolygonList = ser.Deserialize<List<BoundaryPolygon>>(oCarriage.fieldvalue);
            }
        }
    }
}
