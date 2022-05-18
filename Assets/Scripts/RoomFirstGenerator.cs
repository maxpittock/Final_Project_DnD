using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstGenerator : SimpleRandomWalkDungeonGenerator
{
    //Create parameters for generation
    [SerializeField]
    private int minRoomWidth = 4;
    [SerializeField]
    private int minRoomHeight = 4;
    [SerializeField]
    private int DungeonWidth = 20;
    [SerializeField]
    private int DungeonHeight = 20;

    //Create offset to create floors connecting seperate rooms - ensures we will have walls between rooms
    [SerializeField]
    [Range(0,10)]
    private int offset = 1;
    //bool for checkign which algorithm we want to use to create our rooms
    [SerializeField]
    private bool randomWalkRooms = false;
 
    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        //Call binary space partioning algoirthm and define the parameters it requires
        var roomList = ProceduralGenerationAlgorithms.BinarySpacePartioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(DungeonWidth, DungeonHeight, 0)), minRoomWidth, minRoomHeight);
        //create a hashet for storing floor locations
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        //call method

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomList);
        }
        else
        {
            floor = CreateSimpleRooms(roomList);
        }

        //create a list for storing the rooms center positions
        List<Vector2Int> RoomCenter = new List<Vector2Int>();
        //foreach room in the roomlist
        foreach (var room in roomList)
        {
            //adds the center posistion of each room to the room centre list - from this we need to connect the closest centers to one another (corridors)
            RoomCenter.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        //create hashset vector for storing corrdior positions
        HashSet<Vector2Int> corridors = RoomConnect(RoomCenter);
        //join the floor and the corrdiors together
        floor.UnionWith(corridors);

        //paints the floor positions with the hashet
        tilemapVisualizer.PaintFloorTiles(floor);
        //Generate walls 
        WallGen.Wall_Create(floor, tilemapVisualizer);

    }

    
    private HashSet<Vector2Int> RoomConnect(List<Vector2Int> RoomCenter)
    {
        //create corridor hashet vector for storing corridor positions
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        //create variable to select and store a random rooms center position
        var currentCenter = RoomCenter[Random.Range(0, RoomCenter.Count)];
        //after storing in anotherr variable remove from orginal vector so it isnt used again
        RoomCenter.Remove(currentCenter);
        //While not every room has been cycled through and removed 
        while (RoomCenter.Count > 0)
        {
            //find the closest room to the currently selected one and store it in a temp variable
            Vector2Int closestRoom = FindClosestPointTo(currentCenter, RoomCenter);
            //remove closest room from the orginal vector so it isnt used again
            RoomCenter.Remove(closestRoom);
            //creates a new hashet to store and create the corrdiors positions between the two rooms
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentCenter, closestRoom);
            //
            currentCenter = closestRoom;
            //join the corrdors hashsets
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }   

    //create corridor function
    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentCenter, Vector2Int closestRoom)
    {
        //create hashet for sotring corridor positions 
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        //set the posotion to be equal to the current center vector
        var position = currentCenter;
        //add the current position to the corridor vector
        corridor.Add(position);
        //while the current pos and deistinations y is not the same
        while (position.y != closestRoom.y)
        {
            //if its above the pos move up
            if (closestRoom.y > position.y)
            {
                position += Vector2Int.up;
            }
            //if below move down
            else if (closestRoom.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
         //while the current pos and deistinations x is not the same
        while (position.x != closestRoom.x)
        {
            //if its right of the pos move right
            if (closestRoom.x > position.x)
            {
                position += Vector2Int.right;
            }
             //if its left of the pos move left
            else if (closestRoom.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;

    }

    //find closest room func
    private Vector2Int FindClosestPointTo(Vector2Int currentCenter, List<Vector2Int> RoomCenter)
    {
        //create vector for storing closest room
        Vector2Int closestRoom = Vector2Int.zero;
        //temp variable
        float length = float.MaxValue;
        //loop through each room centre
        foreach(var position in RoomCenter)
        {
            //gets the distance as a float between the current pos and the other posisitions in the room centre vector
            float distance = Vector2.Distance(position, currentCenter);
            //if the distance to the next room is lower than the max length
            if (distance < length)
            {
                //set the distance as the new length (this will find us the lowest one out of the list)
                distance = length;
                //set the cloest room to be equal to the room in the list thats closest
                closestRoom = position;
            }
        }
        return closestRoom;
    }



    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList)
    {
        //create floor hashset vector
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        //for each room in our rooms list (each bounds int)
        foreach (var room in roomList)
        {
            //Checks the x axis
            for (int col = offset; col < room.size.x - offset; col++)
            {
                //cheecks vertical points  & apply the offest to room size
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    //add a posistion for each point in the room (i think)
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col,row);
                    //add the positions to the floor 
                    floor.Add(position);
                }
            }
        }
        //return data within the floor variable
        return floor;   
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        //create a hashset for storing the floor positions
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        //loop through all rooms in the list
        for (int i = 0; i < roomsList.Count; i++)
        {
            //create a temo variable for the current element in the array
            var roomBounds = roomsList[i];
            //get the center of the room
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            ////run the random walk alogirthm from the center point of the room ( this will make the room randomly generated instead of just a rectangle)
            var roomFloor = RunRandomWalk(Algorithm_Parameters, roomCenter);
            //foreach room in the list
            foreach (var room in roomFloor)
            {
                //applying the offset - makes sure rooms arent to close together
                if(room.x >= (roomBounds.xMin + offset) && room.x <= (roomBounds.xMax - offset) && room.y >= (roomBounds.yMin - offset) && room.y <= (roomBounds.yMax - offset))
                {
                    //add to the floor hashset
                    floor.Add(room);
                }
            }

        }
        //return the flor hashset
        return floor;
    }

}

