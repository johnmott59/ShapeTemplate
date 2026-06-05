using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeTemplateLib;

namespace ShapeTemplateLib.Templates.User0
{
    public partial class NRMRoomEdge : PolygonEdge
    {
        public int IsExteriorEdge { get; set; }
        public int IsOpenSpaceEdge { get; set; }

        public bool InteriorDoorCandidate { get; set; } = false;
        public bool DoorPresent { get; set; } = false;
        public bool ExteriorWindowCandidate { get; set; } = false;

        public List<NRMOpenArea> ConnectedOpenAreas { get; set; } = new List<NRMOpenArea>();

        // This flag is used when exporting the connection information. The list of open areas is great for us 
        // but we don't need a list of open areas, we only need to know that the edges do connect and to what
        // each open area will be represented by a flag value

        public long ConnectedOpenAreaFlag { get; set; }

        // If this edge is part of a room record what open areas this edge connects to, 
        // either directly or indirectly through contact with a neighboring room

        public NRMRoomEdge(PointF from, PointF to) : base(from,to)
        {

        }

        public NRMRoomEdge(PolygonEdge pe) :base(pe)
        {

        }

        public NRMRoomEdge(LineSegment ls,string ID) : base(ls)
        {
            this.ID = ID;
        }

        public static void SplitEdgesAtIntersection(List<NRMRoomEdge> PEOpenArea, List<NRMRoomEdge> PEOutline)
        {
            // catch one or both lists null or empty
            if (PEOpenArea == null || PEOpenArea.Count == 0 || PEOutline == null || PEOutline.Count == 0) return;

            // Set a tolerence of the thinnest edge. 

            bool ThereAreSplits = true;

            while (ThereAreSplits)
            {
                ThereAreSplits = false;

                for (int i = 0; i < PEOpenArea.Count && ThereAreSplits == false; i++)
                {
                    NRMRoomEdge S1 = PEOpenArea[i];
                    for (int j = 0; j < PEOutline.Count && ThereAreSplits == false; j++)
                    {
                        NRMRoomEdge S2 = PEOutline[j];

                        PointF? p = PolygonEdge.FindInsideIntersectionPoint(S1, S2);

                        if (p != null)
                        {
                            // TBD make sure its not too close to an existing point

                            // Create two new segments with the intersection as the 'from' point. 
                            // We want each new section to have the appropriate id so we know where they came from

                            NRMRoomEdge pe1 = new NRMRoomEdge(p.Value, S1.To);
                            pe1.Width = S1.Width;
                            pe1.Height = S1.Height;
                            pe1.ID = S1.ID;

                            pe1.InteriorDoorCandidate = S1.InteriorDoorCandidate;
                            pe1.ExteriorWindowCandidate = S1.ExteriorWindowCandidate;
                            pe1.IsExteriorEdge = S1.IsExteriorEdge;
                            pe1.IsOpenSpaceEdge = S1.IsOpenSpaceEdge;
                            pe1.IsInteriorWallSection = S1.IsInteriorWallSection;
                            
                            PEOpenArea.Insert(i + 1, pe1);
                            //PolygonEdgeList.Add(pe1);

                            NRMRoomEdge pe2 = new NRMRoomEdge(p.Value, S2.To);
                            pe2.Width = S1.Width;
                            pe2.Height = S1.Height;
                            pe2.ID = S2.ID;

                            pe2.InteriorDoorCandidate = S2.InteriorDoorCandidate;
                            pe2.ExteriorWindowCandidate = S2.ExteriorWindowCandidate;
                            pe2.IsExteriorEdge = S2.IsExteriorEdge;
                            pe2.IsOpenSpaceEdge = S2.IsOpenSpaceEdge;
                            pe2.IsInteriorWallSection = S2.IsInteriorWallSection;

                            PEOutline.Insert(j + 1, pe2);
                            //PolygonEdgeList.Add(pe2);

                            // Truncate these segments

                            S1.To = p.Value;
                            S2.To = p.Value;

                            // Indicate that there are splits so that the list can be reprocessed

                            ThereAreSplits = true;

                        }

                    }
                }

            }
        }


        // This algorithm 
        public static bool IsPointInsidePolygon(PointF v, List<NRMRoomEdge> RoomEdgeList)
        {
            List<PointF> PointList = new List<PointF>();
            float[] polyX = new float[RoomEdgeList.Count];
            float[] polyY = new float[RoomEdgeList.Count];

            int ndx = 0;
            foreach (NRMRoomEdge pe in RoomEdgeList)
            {
                polyX[ndx] = pe.From.X;
                polyY[ndx] = pe.From.Y;
                ndx++;
                PointList.Add(pe.To);
            }

            float x = v.X;
            float y = v.Y;


            // first test is to see if this point is also one of the vertices of the edge list.
            // if this is an overlapping vertex its considered inside for this purpose. 

            for (int i=0; i < PointList.Count; i++)
            {
                if (polyX[i] == v.X && polyY[i] == v.Y) return true;
            }

   
            bool oddNodes = false;
            int j = PointList.Count - 1;

            for (int i = 0; i < PointList.Count; i++)
            {
                if (polyY[i] < y && polyY[j] >= y
                || polyY[j] < y && polyY[i] >= y)
                {
                    if (polyX[i] + (y - polyY[i]) / (polyY[j] - polyY[i]) * (polyX[j] - polyX[i]) < x)
                    {
                        oddNodes = !oddNodes;
                    }
                }
                j = i;
            }

            return oddNodes;

        }
    }
}
