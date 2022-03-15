using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGen
{
    // will make private variables visible in the inspector 
    [SerializeField]
    // how many times we want to run our algorithm
    private SimpleRandomWalk_Saves Algorithm_Parameters;


    //function for running the procedural generation
    protected override void RunProceduralGeneration()
    {
        //Create hashset to store the and run floor positions  
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        //Clears the map when button is pressed (to remove previous maps and load another)
        tilemapVisualizer.Clear();
        //Call the public method from tilemap visualizer and hand it the floor positions so it can place the tiles on correct locations.
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGen.Wall_Create(floorPositions,tilemapVisualizer);
        foreach (var position in floorPositions)
        {
            //output data to console
            Debug.Log(position);
        }
        
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        // set currentposition as start position
        var currentPosition = startPosition;
        //stores floor positions
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < Algorithm_Parameters.Iterations; i++)
        {
            //path references SimpleRandomWalk( from ProceduralGenerationAlgorithms script handing it the start posistion and walk length
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, Algorithm_Parameters.WalkLength);
            // add path to floor positions using union with (this is possible because we are using hashset - makes sure theres no duplicates)
            floorPositions.UnionWith(path);
            //if the bool is on
            if (Algorithm_Parameters.startRandomlyEachIteration)
                //select random posisition from floor posostion hashset - allows us to start from any position thats connected to a floor posisition  
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
               
        }
        //return this as each path is intergrated in "floorPositions" because of "union with"
        return floorPositions;
    }
}
 