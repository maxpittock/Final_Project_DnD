using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creates a menu item in the inspector that allows a user to change the map paramters easily. These can then be saved to create different types of maps
[CreateAssetMenu(fileName = "SimpleRandomWalkParameters",menuName = "ProceduralGeneration/SimpleRandomWalkSaves")]

public class SimpleRandomWalk_Saves : ScriptableObject
{
    // how many times we want to run our algorithm
    public int Iterations = 10;
    //Tells the algorithm the distance it needs to go
    public int WalkLength = 10;
    //Set up bool to randomise intial generation.
    public bool startRandomlyEachIteration = true;


}
