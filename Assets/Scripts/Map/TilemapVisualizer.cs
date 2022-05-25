using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using static Wall_types;

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
    private TileBase TopWall;
    [SerializeField]
    private TileBase WallsideRight;
    [SerializeField]
    private TileBase WallsideLeft;
    [SerializeField]
    private TileBase Walldown;
    [SerializeField]
    private TileBase FullWall;
    [SerializeField]
    private TileBase WallInnerDownLeft, WallInnerDownRight;
    [SerializeField]
    private TileBase WallDiagonalDownRight, WallDiagonalDownLeft, WallDiagonalUpRight, WallDiagonalUpLeft;


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

    public void Paintbasicwall(Vector2Int position, string binaryType)
    {
        //Displays the binary number tpye to the coresponding space to the console
        //Debug.Log(position + "type; " + binaryType);
        
        //convert the binary strings to and int (this is how the binary values for the walls are defined)
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        //Debug.Log(typeAsInt + "Int");
        //create tilebase
        TileBase tile = null;
        //Debug.Log(tile + "tile");

        //if statements use the binary string to figure out which sprite is needed for each location and is then applied acordingly 
        // if the function in wall types contains a int the same
        if (Wall_types.wallTop.Contains(typeAsInt))
        {
            //set the top wall sprite
           tile = TopWall;
        }
        else if (Wall_types.wallSideRight.Contains(typeAsInt))
        {
            tile = WallsideRight;
        }
        else if (Wall_types.wallSideLeft.Contains(typeAsInt))
        {
            tile = WallsideLeft;
        }
        else if (Wall_types.wallBottom.Contains(typeAsInt))
        {
            tile = Walldown;
        }
        else if (Wall_types.wallFull.Contains(typeAsInt))
        {
            tile = FullWall;
        }
        if (tile!=null)
            PaintSingleTile(Wallmap, tile, position);

    }

    public void Paintcornerwall(Vector2Int position, string binaryType)
    {
        //convert the binary strings to and int (this is how the binary values for the walls are defined)
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        //create tilebase
        TileBase tile = null;

        if (Wall_types.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
           tile = WallInnerDownLeft;
        }
        else if (Wall_types.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = WallInnerDownRight;
        }
        else if (Wall_types.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = WallDiagonalDownLeft;
        }
        else if (Wall_types.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = WallDiagonalDownRight;
        }
        else if (Wall_types.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = WallDiagonalUpRight;
        }
        else if (Wall_types.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = WallDiagonalUpLeft;
        }
        else if (Wall_types.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = FullWall;
        }
        else if (Wall_types.wallBottomEightDirections.Contains(typeAsInt))
        {
            tile = Walldown;
        }
        if (tile!=null)
            PaintSingleTile(Wallmap, tile, position);

    }

    //method to clear the generated map
    public void Clear()
    {              
        Wallmap.ClearAllTiles();
        floorTilemap.ClearAllTiles();
    }
}
