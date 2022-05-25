using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralGenerationAlgorithms 
{
    //hashset method (hashset and vector2int allows us to remove duplicates - ideal for random walk) 
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosistion, int walkLength)
    {
        //stores path data
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        //add start posittion to path
        path.Add(startPosistion);
        //sets the previous posisiton as the currrent start position - so the alogirithm doesnt go back on itself
        var previousPosition = startPosistion;

        //while i is smaller than the defined walk length
        for (int i = 0; i < walkLength; i++)
        {
            //Move 1 step from previous position to random direction 
            var newPosition = previousPosition + Direction2D.GetRandomDirection();
            // add new movement to the path
            path.Add(newPosition);
            //make previous posistion equal to new posistion for the loop
            previousPosition = newPosition;

        }
        //return path created (runs the random walk algorithm)
        return path;
    }

    // creating the function to create corridors connecting multiple rooms 
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        

        //You can save it as a Map or as an array and then convert it to a HashSet on the client.

        
        //use list instead of vector 2 int since we need the last position for the next start position
        List<Vector2Int> corridor = new List<Vector2Int>();
        //Create direction varianble to select a direction
        var direction = Direction2D.GetRandomDirection();
        // add current position variable
        var currentPosition = startPosition;
        //Adds current pos as the start pos
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {

            currentPosition += direction;
            //Adds current pos as the start pos
            corridor.Add(currentPosition);
        }
        //return variable
        return corridor;

    }

    //Binary space partioning algorithm 
    //Boundsint is used as it allows the representationg of a box with interger values, this allows me to split the rooms
    public static List<BoundsInt> BinarySpacePartioning(BoundsInt SplitSpace, int minWidth, int minHeight)
    {
        //use queue to split the rooms since it doesnt matter how they are split.
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        //Create the paramter to save the results
        List<BoundsInt> roomsCreated = new List<BoundsInt>();
        //Enqueue the split space 
        roomsQueue.Enqueue(SplitSpace);
        //while we have rooms to split
        while (roomsQueue.Count > 0)
        {
            //Creates variable of  room to split
            var room = roomsQueue.Dequeue();
            //if statment checks if the room is able to be split into multiple rooms and how that room can be split (horizontally or vertically)
            if(room.size.y >= minHeight && room.size.x >= minWidth)
            {
                //random way to split the room - if random number lower that .5 split horizontally
                if (Random.value < 0.5f)    
                {   
                    //checks if we can split horizontally
                    if (room.size.y >= minHeight * 2)
                    {
                        //call split horizontally method
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }   
                    //if cant split horizontally then check if we can split vertically
                    else if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    //if the room cannot be split either way but still contains a room
                    else if(room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        //add the room to the list
                        roomsCreated.Add(room);
                    }
                }
            
                //if above .5 split vertically
                else
                {
                    //if cant split horizontally then check if we can split vertically
                    if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    //checks if we can split horizontally
                    else if (room.size.y >= minHeight * 2)
                    {
                        //call split horizontally method
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    //if the room cannot be split either way but still contains a room
                    else if(room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        //add the room to the list
                        roomsCreated.Add(room);
                    }
                }
     
            }
         
        }
        return roomsCreated;
    }
     


    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
       
        //defines the random split of the room
        var SplitY = Random.Range(1, room.size.y);
        //Defines room a start postion and creates the room parameter
        BoundsInt roomA = new BoundsInt(room.min, new Vector3Int(room.size.x, SplitY, room.size.z));
        //Defines room b start postion and creates the room parameter
        BoundsInt roomB = new BoundsInt(new Vector3Int(room.min.x, room.min.y + SplitY, room.min.z),
            //Create the size of the roomsQueue
            new Vector3Int(room.size.x, room.size.y - SplitY, room.size.z));

        //Adds the created rooms to the queue
        roomsQueue.Enqueue(roomA);
        roomsQueue.Enqueue(roomB);
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        //defines the split of the room
        var splitX = Random.Range(1, room.size.x); 
        //Defines room a start postion and creates the room parameter
        BoundsInt roomA = new BoundsInt(room.min, new Vector3Int(splitX, room.size.y, room.size.z));
        //Defines room b start postion and creates the room parameter
        BoundsInt roomB = new BoundsInt(new Vector3Int(room.min.x + splitX, room.min.y, room.min.z),
            //Create the size of the roomsQueue
            new Vector3Int(room.size.x - splitX, room.size.y, room.size.z));

        //Adds the created rooms to the queue
        roomsQueue.Enqueue(roomA);
        roomsQueue.Enqueue(roomB);

    }
}
//end of binary partioning algorithm

public static class Direction2D
{
    //create a list of directions 
    public static List<Vector2Int> DirectionList = new List<Vector2Int>
    {
        //directions for the new position
        new Vector2Int(0,1), // up
        new Vector2Int(1,0), //right
        new Vector2Int(0,-1), //down
        new Vector2Int(-1,0) //left
    };

    //create a list of diagonal directions for wall placement directions 
    public static List<Vector2Int> Diagonal_DirectionList = new List<Vector2Int>
    {
        //directions for the new position
        new Vector2Int(1,1), // up-right
        new Vector2Int(1,-1), //right-down
        new Vector2Int(-1,-1), //down-left
        new Vector2Int(-1,1) //left-up
    };

        //create a list of directions 
    public static List<Vector2Int> EightDirList = new List<Vector2Int>
    {      
        //directions for the new position
        new Vector2Int(0,1), // up
        new Vector2Int(1,1), // up-right
        new Vector2Int(1,0), //right
        new Vector2Int(1,-1), //right-down
        new Vector2Int(0,-1), //down
        new Vector2Int(-1,-1), //down-left
        new Vector2Int(-1,0), //left
        new Vector2Int(-1,1) //left-up
    };

    //method to get random direction
    public static Vector2Int GetRandomDirection()
    {
        return DirectionList[Random.Range(0, DirectionList.Count)];
    }

}
