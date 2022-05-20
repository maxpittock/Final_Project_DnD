using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase_Authentication;

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
    [Header("Character_Details")]
    public TMP_InputField PlayerName;
    public TMP_InputField Class_Level;
    public TMP_InputField Race;
    public TMP_InputField Alignment;
    public TMP_InputField Background;
    public TMP_InputField Exp;

    [Header("Character Background")]
    public TMP_InputField Traits;
    public TMP_InputField Ideals;
    public TMP_InputField Bonds;
    public TMP_InputField Flaws;


    [Header("Character Health")]
    public TMP_InputField Armour_Class;
    public TMP_InputField Initiative;
    public TMP_InputField Speed;
    public TMP_InputField HitPointMax;
    public TMP_InputField CurrentHitPoints;
    public TMP_InputField TempHitPoints;
    public TMP_InputField TotalHitDie;
    public TMP_InputField Hitdie;

    [Header("Money & Equipment")]
    public TMP_InputField Equipment;
    public TMP_InputField Copper;
    public TMP_InputField Silver;
    public TMP_InputField Gold;
    public TMP_InputField Platinum;
    public TMP_InputField Extra_Money;

    [Header("Inventory")]
    public TMP_InputField Inventory;

    [Header("Character Stats")]
    public TMP_InputField Strength;
    public TMP_InputField Dexterity;
    public TMP_InputField Constitution;
    public TMP_InputField Intelligence;
    public TMP_InputField Wisdom;
    public TMP_InputField Charisma;

    [Header("Character Skills")]
    public TMP_InputField PassivePerception;
    public TMP_InputField Acrobatics;
    public TMP_InputField AnimalHandling;
    public TMP_InputField Arcana;
    public TMP_InputField Athletics;
    public TMP_InputField Deception;
    public TMP_InputField History;
    public TMP_InputField Insight;
    public TMP_InputField Intimidation;
    public TMP_InputField Investigation;
    public TMP_InputField Medicine;
    public TMP_InputField Nature;
    public TMP_InputField Perception;
    public TMP_InputField Performance;
    public TMP_InputField Persuasion;
    public TMP_InputField Religion;
    public TMP_InputField SleightOfHand;
    public TMP_InputField Stealth;
    public TMP_InputField Survival;

    [Header("Character Saving Throws")]
    public TMP_InputField StrengthSavingThrow;
    public TMP_InputField DexteritySavingThrow;
    public TMP_InputField ConstitutionSavingThrow;
    public TMP_InputField IntelligenceSavingThrow;
    public TMP_InputField WisdomSavingThrow;
    public TMP_InputField CharismaSavingThrow;

    [Header("Character Proficiencys")]
    public TMP_InputField StrengthProf;
    public TMP_InputField DexterityProf;
    public TMP_InputField ConstitutionProf;
    public TMP_InputField IntelligenceProf;
    public TMP_InputField WisdomProf;
    public TMP_InputField CharismaProf;

    [Header("Character bonues")]
    public TMP_InputField Inspriation;
    public TMP_InputField ProfBonus;

    [Header("Languages & Profs")]
    public TMP_InputField LanguagesandProfs;

    [Header("Attack and spellcasting")]
    public TMP_InputField Weapon1;
    public TMP_InputField Weapon2;
    public TMP_InputField Weapon3;
    public TMP_InputField DamageType1;
    public TMP_InputField DamageType2;
    public TMP_InputField DamageType3;
    public TMP_InputField Damage1;
    public TMP_InputField Damage2;
    public TMP_InputField Damange3;
    public TMP_InputField Extra_Attacks;
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
        Debug.Log("Setting up autentication for firebase");
        //set the authentication instance object
        //auth = FirebaseAuth.DefaultInstance.Curre;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = FirebaseAuth.DefaultInstance.CurrentUser;
    }

    public void userTest()
    {
        Debug.Log(User.DisplayName + "Testing");
    }

    //function for saving the users data
    public void SaveDataButton()
    {
        //update the data for the text fields
        StartCoroutine(UpdateUsernameAuth(PlayerName.text));
        StartCoroutine(UpdateCharDetails(PlayerName.text, Class_Level.text, Race.text, Alignment.text, Background.text, int.Parse(Exp.text)));
        StartCoroutine(UpdateCharBackground(Traits.text, Ideals.text, Bonds.text, Flaws.text));
        StartCoroutine(UpdateCharHealth(TempHitPoints.text, CurrentHitPoints.text, HitPointMax.text, Hitdie.text, TotalHitDie.text, Initiative.text, Speed.text, Armour_Class.text));
        //StartCoroutine(UpdatePlayerName(PlayerName.text));
       // StartCoroutine(UpdateEXP(int.Parse(Exp.text)));
        //StartCoroutine(UpdateClass_Level(Class_Level.text));
        //StartCoroutine(UpdateRace(Race.text));
       // StartCoroutine(UpdateAllignment(Alignment.text));
        //StartCoroutine(UpdateBackground(Background.text));

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

    private IEnumerator UpdateCharDetails(string PlayerName, string Class_Level, string Race, string Alignment, string Background, int EXP)
    {
        //Set the currently logged in user username in the database
        //main line to update the database
        var DBTask1 = DBreference.Child("users").Child(User.UserId).Child("CharacterDetails").Child("PlayerName").SetValueAsync(PlayerName);
        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted);
        var DBTask2 = DBreference.Child("users").Child(User.UserId).Child("CharacterDetails").Child("Class & Level").SetValueAsync(Class_Level);
        yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);
        var DBTask3 = DBreference.Child("users").Child(User.UserId).Child("CharacterDetails").Child("Race").SetValueAsync(Race);
        yield return new WaitUntil(predicate: () => DBTask3.IsCompleted);
        var DBTask4 = DBreference.Child("users").Child(User.UserId).Child("CharacterDetails").Child("Allignment").SetValueAsync(Alignment);
        yield return new WaitUntil(predicate: () => DBTask4.IsCompleted);
        var DBTask5 = DBreference.Child("users").Child(User.UserId).Child("CharacterDetails").Child("Background").SetValueAsync(Background);
        yield return new WaitUntil(predicate: () => DBTask5.IsCompleted);
        var DBTask6 = DBreference.Child("users").Child(User.UserId).Child("CharacterDetails").Child("EXP").SetValueAsync(EXP);
        yield return new WaitUntil(predicate: () => DBTask6.IsCompleted);
        //wait until the task is completed
       
    }

    private IEnumerator UpdateCharBackground(string Traits, string Ideals, string Bonds, string Flaws)
    {
        //Set the currently logged in user username in the database
        //main line to update the database
        var DBTask7 = DBreference.Child("users").Child(User.UserId).Child("CharacterBackground").Child("Traits").SetValueAsync(Traits);
        yield return new WaitUntil(predicate: () => DBTask7.IsCompleted);
        var DBTask8 = DBreference.Child("users").Child(User.UserId).Child("CharacterBackground").Child("Ideals").SetValueAsync(Ideals);
        yield return new WaitUntil(predicate: () => DBTask8.IsCompleted);
        var DBTask9 = DBreference.Child("users").Child(User.UserId).Child("CharacterBackground").Child("Bonds").SetValueAsync(Bonds);
        yield return new WaitUntil(predicate: () => DBTask9.IsCompleted);
        var DBTask10 = DBreference.Child("users").Child(User.UserId).Child("CharacterBackground").Child("Flaws").SetValueAsync(Flaws);
        yield return new WaitUntil(predicate: () => DBTask10.IsCompleted);

       
    }
    private IEnumerator UpdateCharHealth(string TempHitPoints, string CurrentHitPoints, string HitPointMax, string Hitdie, string MaxHitdie, string Intiative, string Speed, string Armour_Class )
    {
        //Set the currently logged in user username in the database
        //main line to update the database
        var DBTask11 = DBreference.Child("users").Child(User.UserId).Child("CharacterHealth").Child("Temporary HP").SetValueAsync(TempHitPoints);
        yield return new WaitUntil(predicate: () => DBTask11.IsCompleted);
        var DBTask12 = DBreference.Child("users").Child(User.UserId).Child("CharacterHealth").Child("Current HP").SetValueAsync(CurrentHitPoints);
        yield return new WaitUntil(predicate: () => DBTask12.IsCompleted);
        var DBTask13 = DBreference.Child("users").Child(User.UserId).Child("CharacterHealth").Child("HP Max").SetValueAsync(HitPointMax);
        yield return new WaitUntil(predicate: () => DBTask13.IsCompleted);
        var DBTask14 = DBreference.Child("users").Child(User.UserId).Child("CharacterHealth").Child("Hit Die").SetValueAsync(Hitdie);
        yield return new WaitUntil(predicate: () => DBTask14.IsCompleted);
        var DBTask15 = DBreference.Child("users").Child(User.UserId).Child("CharacterHealth").Child("Max Hit Die").SetValueAsync(MaxHitdie);
        yield return new WaitUntil(predicate: () => DBTask15.IsCompleted);
        var DBTask16 = DBreference.Child("users").Child(User.UserId).Child("CharacterHealth").Child("Iniative").SetValueAsync(Intiative);
        yield return new WaitUntil(predicate: () => DBTask16.IsCompleted);
        var DBTask17 = DBreference.Child("users").Child(User.UserId).Child("CharacterHealth").Child("Speed").SetValueAsync(Speed);
        yield return new WaitUntil(predicate: () => DBTask17.IsCompleted);
        var DBTask18 = DBreference.Child("users").Child(User.UserId).Child("CharacterHealth").Child("Armour Class").SetValueAsync(Armour_Class);
        yield return new WaitUntil(predicate: () => DBTask18.IsCompleted);
    }

   

    
}

