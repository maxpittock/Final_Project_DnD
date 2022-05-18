using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;

public class Save_Data : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    //authentication variable
    public FirebaseAuth auth;  
    //user  
    public FirebaseUser User;
    //reference to the database
    public DatabaseReference DBreference;



    //Variables for character information
    [Header("Player_data")]
    public TMP_InputField PlayerName;
    public TMP_InputField Class_Level;
    public TMP_InputField Race;
    public TMP_InputField Alignment;
    public TMP_InputField Background;
    public TMP_InputField Exp;
    //public GameObject scoreElement;
    //public Transform scoreboardContent; 


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
            Debug.Log("Setting up database for firebase");
            //set the authentication instance object
            //auth = FirebaseAuth.DefaultInstance;
            //intialise the database
            DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }


    //function for saving the users data
    public void SaveDataButton()
    {
        //update the data for the text fields
        StartCoroutine(UpdateUsernameAuth(PlayerName.text));
        StartCoroutine(UpdateUsernameDatabase(PlayerName.text));
        //StartCoroutine(UpdatePlayerName(PlayerName.text));
        StartCoroutine(UpdateEXP(int.Parse(Exp.text)));
        StartCoroutine(UpdateClass_Level(Class_Level.text));
        StartCoroutine(UpdateRace(Race.text));
        StartCoroutine(UpdateAllignment(Alignment.text));
        StartCoroutine(UpdateBackground(Background.text));

    }

    private IEnumerator UpdateUsernameAuth(string username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = username };

        //Call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = User.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Auth username is now updated
        }        
    }

    private IEnumerator UpdateUsernameDatabase(string username)
    {
        //Set the currently logged in user username in the database
        //main line to update the database
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(username);

        //wait until the task is completed
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator UpdatePlayerName(string PlayerName)
    {
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("PlayerName").SetValueAsync(PlayerName);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

    private IEnumerator UpdateClass_Level(string Class_Level)
    {
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Class&Level").SetValueAsync(Class_Level);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

    private IEnumerator UpdateRace(string Race)
    {
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Race").SetValueAsync(Race);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

    private IEnumerator UpdateAllignment(string Alignment)
    {
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Allignment").SetValueAsync(Alignment);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

    private IEnumerator UpdateBackground(string Background)
    {
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("Background").SetValueAsync(Background);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

        private IEnumerator UpdateEXP(int EXP)
    {
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("EXP").SetValueAsync(EXP);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

    
}

