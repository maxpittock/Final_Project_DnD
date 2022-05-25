using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase_Authentication;

public class Save_Notes : MonoBehaviour
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
    [Header("Notepad")]
    public TMP_InputField Notes;
    public DataSnapshot snapshot;
    public TMP_Text Warning;

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
        //auth = FirebaseAuth.DefaultInstance.Curre;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = FirebaseAuth.DefaultInstance.CurrentUser;
    }

        //function for saving the users data
    public void SaveDataButton()
    {
        StartCoroutine(UpdateNotes(Notes.text));
    
    }
    
    public void LoadDataButton()
    {
        //update the data for the text fields
        StartCoroutine(LoadData());

    }

    private IEnumerator UpdateNotes(string Notes)
    {
        //main line to update the database
        var DBTask1 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Notepad").Child("Page1").SetValueAsync(Notes);
        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted);
  
    }

    private IEnumerator LoadData()
    {
        var DBTask = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning("Load Failed "+ DBTask.Exception);
        }
        //if there is no data to be loaded
        else if (DBTask.Result.Value == null)
        {
            Notes.text = "0";
        }
        else
        {
            snapshot = DBTask.Result;

            Debug.Log(Notes.text + "test");
            
            try
            {
                Notes.text = snapshot.Child("Notepad").Child("Page1").Value.ToString();
            }
            catch 
            {
                Debug.Log("Cant load");
                Warning.text = "Please save some data before trying to load anything";
            }
        }

}
}   
