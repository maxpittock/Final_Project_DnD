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

    // creating the function to create corridors connecting multiple rooms 
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
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