using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerationAlgorithms 
{
    //hashset method (hashset and vector2int allows us to remove duplicates - ideal for random walk) 
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosistion, int walkLength)
    {
        //stores path data
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        //add start posittion to path
        path.Add(startPosistion);
        //
        var previousPosition = startPosistion;

        //
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
}

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

    //method to get random direction
    public static Vector2Int GetRandomDirection()
    {
        return DirectionList[Random.Range(0, DirectionList.Count)];
    }

}