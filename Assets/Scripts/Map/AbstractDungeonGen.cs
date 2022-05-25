using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGen : MonoBehaviour
{
    [SerializeField]
    //tilemap visualizer reference
    protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    //storing start position as vector2int (this means you wont need to do it in every child class)
    protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        //Clear the tilemap 
        tilemapVisualizer.Clear();
        //Function call
        
        
        
        RunProceduralGeneration();

        
        
    }

    public void ClearButton()
    {
        //Clear the tilemap 
        tilemapVisualizer.Clear();
    }
    // This will allow me to generate my tilemap according to the algoirthm i want to use
    protected abstract void RunProceduralGeneration();
   

}
