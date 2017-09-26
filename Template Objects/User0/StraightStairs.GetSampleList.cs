using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class StraightStairs : IGetSample
    {
        /// <summary>
        /// Return a list of sample instances
        /// </summary>
        /// <returns></returns>
        public List<SampleInstance> GetSampleList()
        {
            List<SampleInstance> list = new List<SampleInstance>() {
                new SampleInstance()
                {
                     ShortDescription="Basic stairs",
                     LongDescription="A simple set of square stairs",
                      Sample=new  StraightStairs()
                      {
                          StairCount=5,
                      }.GetProperties()
                },
                new SampleInstance()
                {
                     ShortDescription="Longer stairs",
                     LongDescription="Increasing the run length and the horizontal distance makes this longer",
                      Sample=new  StraightStairs()
                      {
                          StairCount=3,
                          Run=20,
                          HorizontalDistance =20
                      }.GetProperties()
                },
                new SampleInstance()
                {
                     ShortDescription="Taller stairs",
                     LongDescription="Increasing the rise length and the vertical distance makes this taller",
                      Sample=new  StraightStairs()
                      {
                          StairCount=3,
                          Rise=20,
                          VerticalDistance=20
                      }.GetProperties()
                },
                new SampleInstance()
                {
                     ShortDescription="Space between stairs",
                     LongDescription="Making the vertical distance more than the rise creates space between the stairs",
                      Sample=new  StraightStairs()
                      {
                          StairCount=5,
                          Rise=5,
                          Run=15,
                          VerticalDistance=10
                      }.GetProperties()
                },
                new SampleInstance()
                {
                     ShortDescription="Lateral distance",
                     LongDescription="Adding a lateral distance makes the stairs go sideways.",
                      Sample=new  StraightStairs()
                      {
                          LateralDistance=5,
                          StairCount=5,
                          Rise=5,
                          Run=15,
                          VerticalDistance=10
                      }.GetProperties()
                },
                new SampleInstance()
                {
                     ShortDescription="Flip lateral and horizontal",
                     LongDescription="Making the horizontal distance 0 and using a lateral distance lets you change which surface is the step of the stair.",
                      Sample=new  StraightStairs()
                      {
                          LateralDistance=10,
                          HorizontalDistance=0,
                          StairCount=5,
                          Rise=5,
                          Run=15,
                          VerticalDistance=10
                      }.GetProperties()
                },
            };


            return list;

        }
    }
}
