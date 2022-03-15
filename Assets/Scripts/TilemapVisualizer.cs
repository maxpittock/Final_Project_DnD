using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//script will be responsible for putting tiles on the tilemap depending on positions
public class TilemapVisualizer : MonoBehaviour
{
    //Serializefield allows me to see the variables that i give it to in the inspector.
    [SerializeField]
    //reference to our tilemap 
    private Tilemap floorTilemap;
    [SerializeField]
    private Tilemap Wallmap;
    [SerializeField]
    // tell which tiles to paint on the tilemap
    private TileBase floorTile;
    [SerializeField]
    private TileBase wallTop;


    //create function to paint tiles on tile map
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        //call painttiles function
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        // look through each position in positions list
        foreach (var position in positions)
        {
            //call paintsingletile function (create multiple methods so that it will; be easier to use when we cal the visulizer)
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        //get the position on the tilemap
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        //"SetTile" puts a tile on a place in the tilemap.
        tilemap.SetTile(tilePosition, tile);
    }

    public void Paintbasicwall(Vector2Int position)
    {
        //passes the paramaters so that the tilemap can be used to paint the walls in correct positions
        PaintSingleTile(Wallmap, wallTop, position);
    }
    //method to clear the generated map
    public void Clear()
    {              
        Wallmap.ClearAllTiles();
        floorTilemap.ClearAllTiles();
    }
}
