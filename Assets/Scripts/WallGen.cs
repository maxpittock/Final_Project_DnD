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
        //Create foreach loop for painting tiles onto the cnvas
        foreach (var position in basicWallPositions)
        {
            //paint tiles
            tilemapVisualizer.Paintbasicwall(position);
        }

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

}
