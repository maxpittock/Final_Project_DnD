using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Neighbour_Graph : MonoBehaviour
{
    /*
    //create a list of directions 
    public static List<Vector2Int> DirectionList = new List<Vector2Int>
    {
        //directions for the new position
        new Vector2Int(0,1), // up
        new Vector2Int(1,0), //right
        new Vector2Int(0,-1), //down
        new Vector2Int(-1,0) //left
    };

    //create a list of directions 
    public static List<Vector2Int> EightDirList = new List<Vector2Int>
    {      
        //directions for the new position
        new Vector2Int(0,1), // up
        new Vector2Int(1,1), // up-right
        new Vector2Int(1,0), //right
        new Vector2Int(1,-1), //right-down
        new Vector2Int(0,-1), //down
        new Vector2Int(-1,-1), //down-left
        new Vector2Int(-1,0), //left
        new Vector2Int(-1,1) //left-up
    };

    //setup graph variable for neighbours
    List<Vector2Int> graph;

    //constructor for intialisin the graph list
    public Floor_Neighbour_Graph(IEnumerable<Vector2Int> Vertices)
    {
        graph = new List<Vector2Int>(vertices);
    }
   
    //Up down left right checks
    public List<Vector2Int> Neighbour4Dir(Vector2Int startPosition)
    {
        return GetNeigbours(startPosition, NeighbourDirFour);
    }
    //other directional checks for neighbours
    public List<Vector2Int> Neighbour8Dir(Vector2Int startPosition)
    {
        return GetNeigbours(startPosition, NeighbourDirEight);
    }

    //get neighbour function 
    private List<Vector2Int> GetNeigbours(Vector2Int startPosition, List<Vector2Int> NeighbourList )
    {
        //vector list for storing neighbours
        List<Vector2Int> neighbours = new List<Vector2Int>();
        //foreach direction in the offset lists made
        foreach (var neighbourDir in NeighbourList )
        {
            //store each neighbour in a vector 
            Vector2Int potentialNeighbour = startPosition + neighbourDir;
            //
            if (graph.Contains(potentialNeighbour))
                //Add the potential neighbour to the neighbour vector
                neighbours.Add(potentialNeighbour);
        }
        return neighbours;
    }
*/
}
