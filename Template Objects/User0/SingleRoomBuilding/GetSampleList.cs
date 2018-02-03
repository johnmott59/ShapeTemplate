using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShapeTemplateLib.BasicShapes;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class SingleRoomBuilding : IGetSample
    {
        public List<SampleInstance> GetSampleList()
        {
            SingleRoomBuilding srb = new SingleRoomBuilding();
            srb.HorizontalScale = 1;
            srb.VerticalScale = 1;
            srb.RoofOffset = -10;
            srb.Thickness = 3;

            List<SampleInstance> list = new List<SampleInstance>() {
            new SampleInstance()
                {
                    ShortDescription = "Building",
                    LongDescription = "Building with a door and a front window",
                    Sample = srb.GetProperties()
                },
            };

            return list;
        }
    }
}
