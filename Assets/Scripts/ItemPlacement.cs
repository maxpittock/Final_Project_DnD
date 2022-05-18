using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPlacement : MonoBehaviour
{
    /*
    //possible placement possibtions for items in the dungeon
    Dictionary<TileType, HashSet<Vector2Int>> tile2tile = new Dictionary<TileType, HashSet<Vector2Int>>();

    HashSet<Vector2Int> RoomArea;

    //constructor
    public ItemPlacement(HashSet<Vector2Int> RoomFloor, HashSet<Vector2Int> RoomArea)
    {
        
        Floor_Neighbour_Graph graph = new Floor_Neighbour_Graph(RoomFloor);
        
        this.RoomArea = RoomArea;

        foreach (var tile in RoomArea)
        {
            int neighbor8dir = graph.Neighbour8Dir(tile).Count;
            //if the tile has less than 8 neigbours its near a wall
            TileType type = neighbor8dir < 8 ? TileType.NearWall :  TileType;

            if (tile2tile.ContainsKey(type) == false)
                tile2tile[type] = new HashSet<Vector2Int>();
            
            if ( type == TileType.NearWall && graph.Neighbour8Dir(tile).Count)
                continue;
            
            tile2tile[type].Add(tile);

        } 
    }


    public enum TileType
    {
        OpenSpace,
        NearWall
    }
*/
}
