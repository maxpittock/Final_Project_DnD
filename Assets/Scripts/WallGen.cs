using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGen
{
    // create method for finding the dungeon walls 
    public static void Wall_Create(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        //finds the walls in the dungeon
        var basicWallPositions = FindWallsInDirection(floorPositions, Direction2D.DirectionList);
        var CornerWall = FindWallsInDirection(floorPositions, Direction2D.Diagonal_DirectionList);
        //creates the basic walls
        CreateBasicWall(tilemapVisualizer, basicWallPositions, floorPositions);
        //responsible for placing corner walls and correcting previous walls if they are incorrect (thats why its called after)
        CreateCornerWall(tilemapVisualizer, CornerWall, floorPositions);
  

    }

    private static HashSet<Vector2Int> FindWallsInDirection(HashSet<Vector2Int> floorPositions, List<Vector2Int> direction_List)
    {
        //Vector for storing wall positions
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            //for each position we call direction
            foreach (var direction in direction_List)
            {
                //For each dirrection we call neighbour pos - Checks position 
                var neighbourPositions = position + direction;
                //If the position is near floor but not on floor tile list
                if (floorPositions.Contains(neighbourPositions) == false)
                    //Add all potential wall tiles to var
                    wallPositions.Add(neighbourPositions);
                
            }
        }
        return wallPositions;
    }

    public static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        //for each element in the wall posistion vector (each individual wall)
        foreach (var position in basicWallPositions)
        {
            //setup the empty neighbour string
            string Binary_Neighbor = "";
            //
            foreach (var direction in Direction2D.DirectionList)
            {
                //checking the neighours of the current tile
                var Neighbourpos = position + direction;
                //if a floor is detected 
                if (floorPositions.Contains(Neighbourpos))
                {
                    //add one to the binary neighbour string
                    Binary_Neighbor += "1";
                }
                else
                {
                    Binary_Neighbor += "0";
                }
            }
            //paint tiles
            tilemapVisualizer.Paintbasicwall(position, Binary_Neighbor);
        }
    }

    public static void CreateCornerWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> CornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        
        //for each element in the wall posistion vector (each individual wall)
        foreach (var position in CornerWallPositions)
        {
            //setup the empty neighbour string
            string Binary_Neighbor = "";
            //
            foreach (var direction in Direction2D.EightDirList)
            {
                //find the neighourborus positions
                var neighbourPos = position + direction;;
                if (floorPositions.Contains(neighbourPos))
                {
                    Binary_Neighbor += "1";
                }
                else
                {
                    Binary_Neighbor += "0";
                }
            }
            //paint tiles
            tilemapVisualizer.Paintcornerwall(position, Binary_Neighbor);
        }
    }

}
