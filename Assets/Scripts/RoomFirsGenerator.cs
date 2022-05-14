using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirsGenerator : SimpleRandomWalkDungeonGenerator
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
        floor = CreateSimpleRooms(roomList);

        //paints the floor positions with the hashet
        tilemapVisualizer.PaintFloorTiles(floor);
        //Generate walls 
        WallGen.Wall_Create(floor, tilemapVisualizer);

    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList)
    {
        //create flor hashet
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        //for each point in our bounds int 
        foreach (var room in roomList)
        {
            //check every horntal point?
            for (int col = offset; col < room.size.x - offset; col++)
            {
                //cheecks vertical points  & apply the offest to room size (i thinkkkk check???)
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    //add a posistion for each point in the room (i think)
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col,row);
                    //add the positions to the floor 
                    floor.Add(position);
                }
            }
        }
        return floor;   
    }

}
