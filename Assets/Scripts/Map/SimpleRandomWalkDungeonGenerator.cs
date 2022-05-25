using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase_Authentication;

//using Firebase.Firestore;


//[System.Serializable]
public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGen
{
    public TMP_Text DatabaseWarning;

    public DependencyStatus dependencyStatus;
    //authentication variable
    public FirebaseAuth auth;  
    //user  
    public FirebaseUser User;
    //reference to the database
    public DatabaseReference DBreference;
    //public FirebaseFirestore FirestoreDB;

    // will make private variables visible in the inspector 
    [SerializeField]
    // how many times we want to run our algorithm
    protected SimpleRandomWalk_Saves Algorithm_Parameters;

    //create array 
    private Vector2Int[] HashSet2Array;

    
    //list of ints to store vecto pos to send to firebase
    List<int> posistionStore = new List<int>();

    //loading data list
    private IList LoadedMapData;
    private Vector2Int[] List2Vector;
    //int Yint;
    //public List<string> test;
    private HashSet<Vector2Int> floorPositions;

    public int[,] temp;
    
    void Awake()
    {
        //checks all dependencies that are needed to run firebase are present within the project
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => 
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //if the dependencies are avliable - intialise firebase
                IntializeFirebase();
            }
            else
            {
                //throws error if not all dependencies required are found
                Debug.LogError("Error - cannot find all firebase dependencies");
            }
            
        });
    }

    private void IntializeFirebase()
    {
        //send message to console.
        Debug.Log("Setting up autentication for firebase");
        //set the authentication instance object
        //auth = FirebaseAuth.DefaultInstance.CurrentUser;
        User = FirebaseAuth.DefaultInstance.CurrentUser;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        //FirestoreDB = FirebaseFirestore.DefaultInstance;
       
    }

    //function for running the procedural generation
    protected override void RunProceduralGeneration()
    {

        //Create hashset to store the and run floor positions - hashsets make sure that the content of the vector is unqiue
        floorPositions = RunRandomWalk(Algorithm_Parameters, startPosition);
        //Clears the map when button is pressed (to remove previous maps and load another)
        tilemapVisualizer.Clear();
        //Call the public method from tilemap visualizer and hand it the floor positions so it can place the tiles on correct locations.
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGen.Wall_Create(floorPositions,tilemapVisualizer);

        //foreach (var position in floorPositions)
        //{
            //output data to console
        //    Debug.Log(position);
        //}
        
    } 

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalk_Saves parameters, Vector2Int position)
    {
        // set currentposition as start position
        var currentPosition = position;
        //stores floor positions
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        //Debug.Log(floorPositions + " Floor positions");

        for (int i = 0; i < parameters.Iterations; i++)
        {
            //path references SimpleRandomWalk( from ProceduralGenerationAlgorithms script handing it the start posistion and walk length
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.WalkLength);
            // add path to floor positions using union with (this is possible because we are using hashset - makes sure theres no duplicates)
            floorPositions.UnionWith(path);
            //if the bool is on
            if (parameters.startRandomlyEachIteration)
                //select random posisition from floor posostion hashset - allows us to start from any position thats connected to a floor posisition  
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
               
        }

            // to convert hashet to array try either
            // - .ToArray() - with system.Linq
            // - or hashset<>.CopyTo
            // when stringSet is the hashset
                //String[] stringArray = new String[stringSet.Count];
                //stringSet.CopyTo(stringArray);

        //Creates a vector2 array for sotring converted vector2int data
        HashSet2Array = new Vector2Int[floorPositions.Count];
        //copy the vector2int array to the vector 
        floorPositions.CopyTo(HashSet2Array);
    
        for (int i = 0; i < HashSet2Array.Length; i++)
        {
        
            //Debug.Log(HashSet2Array[i].x + " x");
            //Debug.Log(HashSet2Array[i].y + " y");
            //Debug.Log(i + " i");
            
            //Adds the x to the list 
            posistionStore.Add(HashSet2Array[i].x);
            //add the y to the list
            posistionStore.Add(HashSet2Array[i].y);

            //Codde for debugging and figuring out issues
           // Debug.Log(posistionStore.Count + " size of floor array");
           // Debug.Log(posistionStore + " testing fo shane");
 
        }
        //prints each list pos
        foreach(int key in posistionStore)
        {
            Debug.Log(key + " da list");
        }
  
 
        return floorPositions;
        
    }
    

    public void SaveDataButton()
    {
        
        StartCoroutine(SaveMaps(posistionStore));

    }
    
    private IEnumerator SaveMaps(List<int> savedmap)
    {
        var DBTask = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Maps").Child("Map1").SetValueAsync(savedmap);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
    }

    public void LoadDataButton()
    {
        //update the data for the text fields
        
        StartCoroutine(LoadMaps());
       
    }


    private IEnumerator LoadMaps()
    {

        //string temp = new string();

        var DBTask1 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Maps").GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted);

        if (DBTask1.Exception != null)
        {
            Debug.LogWarning("Load Details Failed "+ DBTask1.Exception);
            DatabaseWarning.text = "No map data currently saved please save some";
        }
        else
        {
            DataSnapshot snapshot = DBTask1.Result;             
        try
        {
            foreach (DataSnapshot s in snapshot.Children)
            {
                //IDictionary result = snapshot.Child("Map1")s.Value;
                Debug.Log(s.Value+ " s");
                
                //Create a list to sttore the loaded map data
                LoadedMapData = (IList)s.Value;
                //int Conversion = LoadedMapData.Convert.ToInt32();
                Debug.Log(LoadedMapData + " List values");
                
                for (int x = 0; x < LoadedMapData.Count; x++)
                {
                    for (int y = 1; y < LoadedMapData.Count; y++)
                    {
                      
                        if (x % 2 == 0 )
                        {
                            if (y % 3 == 1)
                            {
                               //temp = new int[LoadedMapData[x], LoadedMapData[y]];
                            }
                           
                        }
                        //Debug.Log(x + " x");
                        //Debug.Log(y + "y");

                    }
                }
                Debug.Log(temp);

                //loop through contentto check it has been filled.
                foreach(var y in LoadedMapData)
                {
                    //int[] temp = new int[(int)y];
                    //Debug.Log(temp[0]);
                    
                    //List2Vector = new Vector2Int[LoadedMapData];
                    //int conversion = (int)s.Value;
                   
                   // temp = new int;

                    Debug.Log(y);
                }
               // 

                
            }  

        }
        catch
        {
            Debug.Log("Cannot load and convert to list");
             
        }

        
    }

}

}
