using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemPlacement : MonoBehaviour
{
    
    //possible placement possibtions for items in the dungeon
    Dictionary<TileType, HashSet<Vector2Int>> tile2tile = new Dictionary<TileType, HashSet<Vector2Int>>();
    //create roomarea hashet
    HashSet<Vector2Int> RoomArea;

    //constructor to figure out if the tile is near a wall or not
    public ItemPlacement(HashSet<Vector2Int> RoomFloor, HashSet<Vector2Int> RoomArea)
    {
        //import graph from the floor neighour file
        Floor_Neighbour_Graph graph = new Floor_Neighbour_Graph(RoomFloor);
        //setup Roomarea
        this.RoomArea = RoomArea;
        //for each tile in the room
        foreach (var tile in RoomArea)
        {
            //count the neighbour amount in 8 directions
            int neighbour8dir = graph.Neighbour8Dir(tile).Count;
            int neighbour4dir =  graph.Neighbour4Dir(tile).Count;    
            //if the tile has less than 8 neigbours its near a wall if it has 8 neighbours its in open space
            TileType type = neighbour8dir < 8 ? TileType.NearWall: TileType.OpenSpace;
            //If the tile is in open space
            if (tile2tile.ContainsKey(type) == false)
                //create a new hashset vector to store the open space
                tile2tile[type] = new HashSet<Vector2Int>();

            //if the tile is near a wall 
            if (type == TileType.NearWall && neighbour4dir <= 1)
                continue;
            //add the tile to the Dictionary
            tile2tile[type].Add(tile);

        } 
    }
    
    public Vector2? GetItemPlacement(TileType type, int MaxIterations, Vector2Int roomsize, bool addOffset)
    {
        //gets the area where items can be placed,,
        int itemArea = roomsize.x * roomsize.y;
        //if the count of the current tile is < item area
        if (tile2tile[type].Count < itemArea)
            //do nothing`
            return null;

        //setup min iteations
        int iteration = 0;

        //while iterations is smaller than max iterations - this is so to many prefabs arent spawned
        while (iteration < MaxIterations)
        {
            //add to iterations
            iteration++;
            //select a random range between 0 and the current tile type count
            int index = Random.Range(0, tile2tile[type].Count);
            //get a vector2Int of the position of the current tile
            Vector2Int position = tile2tile[type].ElementAt(index);
            
            //if item area is greater than 1
            if (itemArea > 1)
            {
                //create variable for placing a large item 
                var (result, placementPositions) = PlaceBigItem(position, roomsize, addOffset);
                
                //if result is false
                if (result == false)
                    // continue
                    continue;

                //find the open space positions
                tile2tile[type].ExceptWith(placementPositions);
                //find the near wall positions
                tile2tile[TileType.NearWall].ExceptWith(placementPositions);
            }
            else
            {
                //remove it from the dictionary - so that multiple things arent generated in same location
                tile2tile[type].Remove(position);
            }
            //return position for later use
            return position;
        }
        return null;
    }

    //for placing bigger itemss that spread across more than one square
    private (bool, List<Vector2Int>) PlaceBigItem(Vector2Int Startposition, Vector2Int size, bool addOffset)
    {
        //create postions vecto list
        List<Vector2Int> positions = new List<Vector2Int>() 
        {
            Startposition
        };
        //Creating offset so if the neighbour is still the prefab it extends the range 
        int Xmax = addOffset ? size.x + 1 : size.x;
        int Ymax = addOffset ? size.y + 1 : size.y;
        int Xmin = addOffset ? -1 : 0;
        int Ymin = addOffset ? -1 : 0;
        
        //checking if the item in x direction is bigger than one tile
        for (int row = Xmin; row <= Xmax; row++)
        {
            //checking y
            for (int col = Ymin; col <= Ymax; col++)
            {
                //if there are no addtional spaces needed for the item
                if (col == 0 && row == 0)
                //contiue
                    continue;
                //create a vector for storing the 2nd tiles pos
                Vector2Int ChecknewPos = new Vector2Int(Startposition.x + row, Startposition.y + col);
                //If the item only cotains one tile
                if (RoomArea.Contains(ChecknewPos) == false)
                    //set the boolean as false and return positions 
                    return (false, positions);
                // add the position to the new position check vector
                positions.Add(ChecknewPos); 
            }
        }
        //if the xmin > xmax then return true which will add to the offset
        return (true, positions);

        //loops this till it find the correct direction 
   }

    public enum TileType
    {
        OpenSpace,
        NearWall
    }

}
