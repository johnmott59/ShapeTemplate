
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

        public override XElement GetProperties(string PropertyName = "")
        {
            XElement root = new XElement("template",
          new XAttribute("prop", PropertyName),
          new XAttribute("user", "User0"),
          new XAttribute("name", "FloorWithRooms".ToLower()));

            XElement XRoomEdgeGroupList = new XElement("list", new XAttribute("name", nameof(RoomEdgeGroupList).ToLower()));
            root.Add(XRoomEdgeGroupList);
            XRoomEdgeGroupList.Add(GetRoomEdgeGroupList().Elements());

            return root;
        }

        //--------------------------------
        protected XElement GetRoomEdgeGroupList()
        {
            // Add the vertices and the edges
            XElement XList = new XElement("roomedgegrouplist");

            foreach (RoomEdgeGroup r in RoomEdgeGroupList)
            {
                XList.Add(r.GetProperties());
            }

            return XList;
        }

    }
}
