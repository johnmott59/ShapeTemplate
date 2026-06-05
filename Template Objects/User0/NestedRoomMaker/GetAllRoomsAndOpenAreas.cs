using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    /// <summary>
    /// Input to generate a floor layout, containing a tree of open areas and outlines. 
    /// </summary>

    public partial class NestedRoomMaker
    {
        public void GetAllRoomsAndOpenAreas(List<NRMRoom> RoomList, List<NRMOpenArea> OpenAreaList)
        {
            foreach (NestedRoomMaker fli in this.Children)
            {
                fli.GetAllRoomsAndOpenAreas(RoomList, OpenAreaList);
            }

            GetRoomsAndOpenAreasForLevel(RoomList, OpenAreaList);

            return;
        }

        // Collect the data for each level separatley

        public void RoomDataByLevel(List<NRMLevelInfo> LevelInfoList)
        {
            foreach (NestedRoomMaker fli in this.Children)
            {
                fli.RoomDataByLevel(LevelInfoList);
            }

            NRMLevelInfo v = new NRMLevelInfo();
            LevelInfoList.Add(v);

            GetRoomsAndOpenAreasForLevel(v.RoomList, v.OpenAreaList);
        }


    }




}
