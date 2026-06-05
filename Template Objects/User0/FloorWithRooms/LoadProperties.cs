
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
        public bool LoadProperties(XElement xTemplateNode, out string message)
        {
            float fTmp;
            int iTmp;
            string sTmp;

            message = "OK";
            XElement xNode;
            if (!LoadRoomEdgeGroupList(xTemplateNode, out message)) return false;

            return true;
        }
        public bool LoadRoomEdgeGroupList(XElement Xele, out string message)
        {
            message = "OK";

            XElement XList = Utilities.GetListElement(Xele, "roomedgegrouplist");

            // ok if there are no items
            if (XList == null) return true;

            List<RoomEdgeGroup> list = new List<RoomEdgeGroup>();
            foreach (XElement x in XList.Elements("roomedgegroup"))
            {
                RoomEdgeGroup o = new RoomEdgeGroup();
                if (!o.LoadProperties(x, out message)) return false;
                list.Add(o);
            }

            RoomEdgeGroupList = list.ToArray();

            return true;
        }
    }
}
