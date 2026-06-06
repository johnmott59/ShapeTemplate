# CRITICAL BUG FIX: Door Assignment Not Working + Not Actually Random

## Problems Discovered

### Problem 1: No Door Assignment
The door selection algorithm was **finding door candidates** but **not actually assigning them** as doors. The methods `FindConnectedRooms()` and `BuildPathwayToDestinationRoom()` were adding edges to the candidate list but never setting the `HoleGroupID` property, which is what marks an edge as having a door.

### Problem 2: Not Random
Despite being named `RandomlySelectDoors()`, the algorithm always selected the **first edge** (`[0]` index) from the list of common edges, not a random one.

## Root Cause

Both door selection code paths had the same bug:

1. **PATH 1** (`FindConnectedRooms`): Used when no destination rooms defined, multiple rooms exist
2. **PATH 2** (`BuildPathwayToDestinationRoom`): Used when destination rooms (open areas) exist

Both methods:
- ✅ Found common edges between rooms
- ✅ Added edges to `CandidateList`
- ❌ **Never set `edge.HoleGroupID`** to mark them as doors

**Result**: 
```
Door/Window candidates: 5
Doors assigned: 0  ← BUG!
```

The edges were identified as candidates but never "activated" as actual door placements.

## Solutions

### Solution 1: Add HoleGroupID Assignment
Added `HoleGroupID` assignment in both code paths to actually mark edges as doors.

### Solution 2: Add True Random Selection
Added `Random` instance and changed from `[0]` (first edge) to random selection from available edges.

### Files Modified

**1. FindConnectedRooms.cs** (PATH 1: No destinations, multiple rooms)

Added static `Random _doorRandom` field at class level.

```csharp
// OLD CODE (didn't work - always first edge, no HoleGroupID):
FMEdge f = EdgeList[CommonEdgeIndex[0]];
if (!CandidateList.Contains(f)) CandidateList.Add(f);

// NEW CODE (works - random selection + HoleGroupID):
// Randomly select one of the common edges as a doorway
int randomIndex = _doorRandom.Next(CommonEdgeIndex.Count);
int selectedEdgeIndex = CommonEdgeIndex[randomIndex];
FMEdge f = EdgeList[selectedEdgeIndex];

// Assign HoleGroupID to actually mark this as a door!
if (string.IsNullOrEmpty(f.HoleGroupID))
{
    f.HoleGroupID = "door";  // Use a default door pattern
    System.Diagnostics.Debug.WriteLine($"-> Assigned HoleGroupID='door' to Edge {f.Index}");
}

if (!CandidateList.Contains(f)) CandidateList.Add(f);
```

**2. BuildPathwayToDestinationRoom.cs** (PATH 2: Has destination rooms)

```csharp
// OLD CODE (didn't work - always first edge, no HoleGroupID):
int ndx = 0;
if (CommonEdgeList.Count == 0)
{
    ndx = dRoom.EdgeIndexList[0];  // Always first
}
else
{
    ndx = CommonEdgeList[0];  // Always first
}

FMEdge f = this.EdgeList[ndx];
if (!FMEdgeCandidateList.Contains(f)) FMEdgeCandidateList.Add(f);

// NEW CODE (works - random selection + HoleGroupID):
int ndx = 0;
if (CommonEdgeList.Count == 0)
{
    // Randomly pick an edge in the open area
    int randomIndex = FloorMaker._doorRandom.Next(dRoom.EdgeIndexList.Length);
    ndx = dRoom.EdgeIndexList[randomIndex];
}
else
{
    // Randomly pick from edges that connect to the open area
    int randomIndex = FloorMaker._doorRandom.Next(CommonEdgeList.Count);
    ndx = CommonEdgeList[randomIndex];
}

FMEdge f = this.EdgeList[ndx];

// Assign HoleGroupID to actually mark this as a door!
if (string.IsNullOrEmpty(f.HoleGroupID))
{
    f.HoleGroupID = "door";
    System.Diagnostics.Debug.WriteLine($"-> Assigned HoleGroupID='door' to Edge {f.Index}");
}

if (!FMEdgeCandidateList.Contains(f)) FMEdgeCandidateList.Add(f);
```

## Enhanced Logging Added

Also added comprehensive debug logging to `FindConnectedRooms()`:

- Shows which room is being visited
- Counts connected unvisited rooms per edge
- Reports common edges found
- Logs when HoleGroupID is assigned
- Shows when edges already have HoleGroupID
- Indicates when rooms are already connected

**Example Output**:
```
PATH 1: No destinations, multiple rooms
  -> Calling FindConnectedRooms() starting from room 0

  FindConnectedRooms visiting room with 5 edges
    Edge 5: Found 1 connected unvisited rooms
      Room has 1 common edges with current room
      Selecting Edge 5 for doorway (was in candidate list: True)
      -> Assigned HoleGroupID='door' to Edge 5
  
  FindConnectedRooms visiting room with 4 edges
    Edge 8: Found 1 connected unvisited rooms
      Room has 1 common edges with current room
      Selecting Edge 8 for doorway (was in candidate list: True)
      -> Assigned HoleGroupID='door' to Edge 8

  FindConnectedRooms complete. Visited 3 rooms.
Doors assigned: 2
```

## Data Flow Explanation

### Before Fix:
```
1. RandomlySelectDoors() collects door candidates (edges with InteriorDoorCandidate=1)
2. Calls FindConnectedRooms() or BuildPathwayToDestinationRoom()
3. These methods add edges to CandidateList
4. Methods return
5. No HoleGroupID assigned anywhere
6. FloorMaker.Compile() copies edges to SimpleLayout
7. SimpleLayout edges have empty HoleGroupID
8. Mesh generation sees no HoleGroupID → generates solid walls
```

### After Fix:
```
1. RandomlySelectDoors() collects door candidates
2. Calls FindConnectedRooms() or BuildPathwayToDestinationRoom()
3. These methods select edges AND assign HoleGroupID="door"  ← FIX!
4. Methods return
5. FloorMaker.Compile() copies edges with HoleGroupID to SimpleLayout
6. SimpleLayout edges have HoleGroupID="door"
7. Mesh generation sees HoleGroupID → applies hole pattern (door cutout)
```

## HoleGroupID Usage

The `HoleGroupID` property:
- Defined in `Edge` base class (inherited by `FMEdge`)
- String property, default empty
- References a hole pattern definition (door/window shape)
- Used during mesh generation to apply boolean subtraction (CSG)
- Value "door" references default door pattern

**In SimpleLayout compilation** (FloorMaker.Compile.cs):
```csharp
new Edge()
{
    p1 = edge.p1,
    p2 = edge.p2,
    Width = (int)edge.Width,
    Height = (int)edge.Height,
    ID = edge.ID,
    HoleGroupID = edge.HoleGroupID  // Copied from FMEdge
}
```

**In mesh generation** (ShapeLib):
```csharp
if (!string.IsNullOrEmpty(edge.HoleGroupID))
{
    // Look up hole pattern and apply boolean subtraction
    ApplyHolePattern(panel, edge.HoleGroupID);
}
```

## Testing

### Before Fix:
```
Door/Window candidates:
  - Interior door candidates: 5
  - Exterior window candidates: 7
Doors assigned: 0  ← FAIL
```

### After Fix (expected):
```
Door/Window candidates:
  - Interior door candidates: 5
  - Exterior window candidates: 7
Doors assigned: 2  ← SUCCESS (or more, depending on room connectivity)
```

## Impact

This fix resolves the primary issue of doors not appearing in generated FBX files. The door selection algorithm now:

1. ✅ Finds door candidates (was working)
2. ✅ Selects appropriate edges (was working)
3. ✅ **Assigns HoleGroupID to mark them as doors (NOW FIXED)**
4. ✅ Propagates HoleGroupID through compilation (was working)
5. ✅ Applies hole patterns during mesh generation (was working but had no data)

## Commit Message

```
CRITICAL FIX: Assign HoleGroupID in door selection algorithms

BUG: Door selection was finding candidates but not marking them as doors.
The HoleGroupID property (which triggers door hole cutout in mesh generation)
was never being set, resulting in solid walls instead of doorways.

FIXED:
- FindConnectedRooms.cs: Assign HoleGroupID="door" when selecting edges
- BuildPathwayToDestinationRoom.cs: Assign HoleGroupID="door" when selecting edges

ENHANCED LOGGING:
- FindConnectedRooms.cs: Log room visits, edge selection, HoleGroupID assignment

RESULT:
Door selection now properly marks edges with HoleGroupID, which propagates
through FloorMaker → SimpleLayout → mesh generation → door cutouts in FBX.

Fixes issue where "Doors assigned: 0" despite having valid candidates.
```

## Files Changed

**ShapeTemplateLib:**
- `Template Objects/User0/FloorMaker/SelectDoors/FindConnectedRooms.cs`
- `Template Objects/User0/FloorMaker/SelectDoors/BuildPathwayToDestinationRoom.cs`
- `Template Objects/User0/FloorMaker/SelectDoors/RandomlySelectDoors.Overload.cs` (already committed - logging only)
- `Template Objects/User0/FloorMaker/SelectDoors/RandomlySelectDoors.cs` (already committed - logging only)
