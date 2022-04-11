using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CorridorFirstGen : SimpleRandomWalkDungeonGenerator
{
    //creating parameters for corridor generation
    [SerializeField]
    private int corridorLength = 14; 
    [SerializeField]
    private int corridorCount = 5;

    //The percent of rooms that are created
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

     
    //Override the inherited class
    protected override void RunProceduralGeneration()
    {
        //call func
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        //create hashset for storing floor positions
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        //create hashset for storing where a room may be created after a corridor
        HashSet<Vector2Int> PotentialRoomPos = new HashSet<Vector2Int>();

        //Uses funtion to create corridors an rooms
        CreateCorridors(floorPositions, PotentialRoomPos);

        //create a hashset to store room positions
        HashSet<Vector2Int> roomPositions = CreateRooms(PotentialRoomPos);      
        //find and store dead ends
        List<Vector2Int> deadEnds = FindDeadEnds(floorPositions);
        //create a room at the dead end (even if the max rooms has been generated)
        CreateRoomsAtDeadEnd(deadEnds, roomPositions);
        //join floor and room positions
        floorPositions.UnionWith(roomPositions);
        //tilemap stuff for corridors (makes sure corridors are painted in the scene using the tilemap provided)
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGen.Wall_Create(floorPositions, tilemapVisualizer);
    }

       private List<Vector2Int> FindDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        //store deadends in list
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        //
        foreach(var position in floorPositions)
        {
            //make counter for finding if there is a neighbour tile
            int neighbourCounter = 0;
            foreach (var direction in Direction2D.DirectionList)
            {
                //check if the floor has neighouring tile
                if (floorPositions.Contains(position + direction))
                {  
                    //add to counter
                    neighbourCounter++;
                }
            }
            //if floor has no neighour 
            if (neighbourCounter == 1)
            {
                //add the deadend to deadend list
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
    {
        foreach (var position in deadEnds)
        {
            //if no room touching = dead end
            if(roomPositions.Contains(position) == false)
            {
                //creates new room on deadend
                var roomFloor = RunRandomWalk(Algorithm_Parameters, position);
                roomPositions.UnionWith(roomFloor);
            }
        }
    }

    //create rooms function
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> PotentialRoomPos)
    {  
        //create hashset to store room pos
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        //decides how many rooms to create
        int RoomCreateCounter = Mathf.RoundToInt(PotentialRoomPos.Count * roomPercent);
        //sorted room pos and got list of those room pos at random
        List<Vector2Int> roomToCreate = PotentialRoomPos.OrderBy(x => System.Guid.NewGuid()).Take(RoomCreateCounter).ToList();

        foreach (var roomPosition in roomToCreate)
        {
            //cycled through each position and create room
            var roomFloor = RunRandomWalk(Algorithm_Parameters, roomPosition);
            //join the rooms and corridors
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;

    }

    //function that creates corridors
    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> PotentialRoomPos)
    {
        //current pos = start pos (inherited from simplerandomwalkdungeongen)
        var currentPosition = startPosition;
        //adds the current pos to the hashset (this will be the start pos)
        PotentialRoomPos.Add(currentPosition);

        for (var i = 0; i < corridorCount; i++ )
        {
            //this will generate our corridor posistions
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            //sets current position to the last position of the last corridor - this makes the corridors connect
            currentPosition = corridor[corridor.Count - 1];
            //add the next position after corriddor has been made
            PotentialRoomPos.Add(currentPosition);
            //this joins the floor pos with the corridors
            floorPositions.UnionWith(corridor);
        }
    }

}   
