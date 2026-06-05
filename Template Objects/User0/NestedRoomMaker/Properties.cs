using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using ShapeTemplateLib.Templates.User0;

namespace ShapeTemplateLib.Templates.User0
{
    public enum eFLInputNodeMessage
    {
        OK,
        MustResultInSinglePolygon,
        NoShapesDefined,
    }

    public class FLInputNodeStatus
    {
        public eFLInputNodeMessage eStatus { get; set; } = eFLInputNodeMessage.OK;
        public string AdditionalInformation { get; set; } = "";
    }

    // this class is used during a process to store variables as the tree is navigate. 
    // the use of these working variables lets us process the tree over and over

    class WorkBench 
    {


        // these are the outlines created at the current level
        public List<NRMEdgeList> CurrentLevelInputPolygons { get; set; }

        // These are the wall segments that will be processed for this level if this is an outline
        // they will be clipped against the outline polygons and any open areas internally

        public NRMEdgeList InnerWallSegments { get; set; } = new NRMEdgeList();
              
        public List<RoomEdgeGroup> RoomEdgeGroupList { get; set; } = new List<RoomEdgeGroup>();
        
        // If this an outline level this will be the rooms in this level. Each level will maintain its own list

        public List<NRMRoom> RoomList = new List<NRMRoom>();

        // This will hold the rooms that are found in this level

        public List<NRMRoomWorkBench> rwslist { get; set; } = new List<NRMRoomWorkBench>();

        // This will hold the room definitions found at this level. This will only be filled in if this is an outline. 

        public List<FloorMaker> RoomLayoutList { get; set; } = new List<FloorMaker>();

    }
    /// <summary>
    /// Input to generate a floor layout, containing a tree of open areas and outlines. 
    /// </summary>
    public partial class NestedRoomMaker : CompilableRoot
    {
        public enum eFLInput3OutsideType {
            Outline,
            OpenArea
        }

        // this is a holding area for variables used during the creation of the input
        WorkBench oWorkBench = new WorkBench();

        // We'll mark the edges that result from the processing with this value so they can be retrieved
        public string IDForEdges { get; set; }

        // The center point of this collection
       // public Point2D CenterPoint { get; set; } = new Point2D() { X = 0, Y = 0 };

        // This defines what type the 'outside' element of this group is, used for clipping 
        public eFLInput3OutsideType OutlineType { get; set; } = eFLInput3OutsideType.Outline;

        // This is the set of polygons that form the HoleGroup
        public SingleHoleGroup HoleGroup { get; set; } = new SingleHoleGroup();

        // There can be interior wall segments associated with this input node if this is an outline
        public LineSegment[] InteriorWallSegmentArray { get; set; } = new LineSegment[0];

        // This group can have children, either open areas or outlines
        public List<NestedRoomMaker> Children { get; set; } = new List<NestedRoomMaker>();

    }

  
}
