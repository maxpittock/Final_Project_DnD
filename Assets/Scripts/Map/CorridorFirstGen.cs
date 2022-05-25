using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

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

    //PCG data - dictionary for storing the procedural data
    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomDict = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
    //Create hashset for storing floor and corridor positions
    private HashSet<Vector2Int> floorPositions, corridorPosition;

    //Override the inherited class
    protected override void RunProceduralGeneration()
    {
        //call new func
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        //You can save it as a Map or as an array and then convert it to a HashSet on the client.
        //convert the data to either

        //do corridor stuff first and then room stufflast cause the bounding boxes may be hard to do 
    
        //will need to use firestore i think   
        // collection is set to maps
            // dococuments are the different map data that is saved
        

        //create hashset for storing floor positions
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        //create hashset for storing where a room may be created after a corridor
        HashSet<Vector2Int> PotentialRoomPos = new HashSet<Vector2Int>();

        //Uses funtion to create corridors and rooms
        CreateCorridors(floorPositions, PotentialRoomPos);

        //create a hashset to store room positions from function
        HashSet<Vector2Int> roomPositions = CreateRooms(PotentialRoomPos);      
        ///create a vector to store deadends from function - identidying dead ends allows me to add an extra room so i looks natural
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
        //Create a list for storing dead ends
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        //for each item in floor position vector
        foreach(var position in floorPositions)
        {
            //make counter for finding if there is a neighbour tile
            int neighbourCounter = 0;
            //checks all sides of the tile
            foreach (var direction in Direction2D.DirectionList)
            {
                //if the tile does have a neighouring one
                if (floorPositions.Contains(position + direction))
                {  
                    //add to neighbour counter
                    neighbourCounter++;
                }
            }
            //if floor has no neighour 
            if (neighbourCounter == 1)
            {
                //add the deadend to deadend list - this is how the algorithm knows where the dead ends are.
                deadEnds.Add(position);
            }
        }
        //return the list of deadends
        return deadEnds;
    }

    //function for creating a room at a dead end
    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
    {
        //for each element in the deadends vector (returned from the previous function)
        foreach (var position in deadEnds)
        {
            //if not room is already there
            if(roomPositions.Contains(position) == false)
            {
                //Run the algoirthm and create a new room on the deadend
                var roomFloor = RunRandomWalk(Algorithm_Parameters, position);
                //join the new room with the rest of the rooms
                roomPositions.UnionWith(roomFloor);
            }
        }
    }

    //create rooms function
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> PotentialRoomPos)
    {  
        //create hashset vector to store room pos
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        //Uses set parameters to decide how many rooms to create
        int RoomCreateCounter = Mathf.RoundToInt(PotentialRoomPos.Count * roomPercent);
        //sorted room posistion and got list of those room posistions at random
        List<Vector2Int> roomToCreate = PotentialRoomPos.OrderBy(x => System.Guid.NewGuid()).Take(RoomCreateCounter).ToList();

        //for each element in the roomtocreate vector
        foreach (var roomPosition in roomToCreate)
        {
            //cycle through the vector and create a room with the algorithm
            var roomFloor = RunRandomWalk(Algorithm_Parameters, roomPosition);

            //Save the room space in the dictionary (collecting data)
            roomDict[roomPosition] = roomFloor;
            //add random colours to the rooms
            //roomColor.Add(Random.ColorHSV());
            //join the rooms and corridors
            roomPositions.UnionWith(roomFloor);
        }
        //return room position vector
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
        //save the corrdior data into a hashset so that i can use this data elsewhere (collecting data)
        corridorPosition = new HashSet<Vector2Int>(floorPositions);
    }

}   
