using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase_Authentication;

namespace Firebase_Authentication
{

    public class Save_Character_Data : MonoBehaviour
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
        public TMP_InputField Etherium;

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
        public TMP_InputField Atheletics;
        public TMP_InputField Deception;
        public TMP_InputField History;
        public TMP_InputField Insight;
        public TMP_InputField Intimidation;
        public TMP_InputField Investigation;
        public TMP_InputField Medicine;
        public TMP_InputField Nature;
        public TMP_InputField Perception;
        public TMP_InputField Performance;
        public TMP_InputField Persuassion;
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
        public TMP_InputField Damage3;
        public TMP_InputField Extra_Attacks;
        //public GameObject scoreElement;
        //public Transform scoreboardContent; 

        public TMP_Text DatabaseWarning;
        
        

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
            //StartCoroutine(UpdateUsernameAuth(PlayerName.text));

            StartCoroutine(UpdateCharDetails(PlayerName.text, Class_Level.text, Race.text, Alignment.text, Background.text, Exp.text));
            
            StartCoroutine(UpdateCharBackground(Traits.text, Ideals.text, Bonds.text, Flaws.text));

            StartCoroutine(UpdateCharHealth(int.Parse(TempHitPoints.text), int.Parse(CurrentHitPoints.text), int.Parse(HitPointMax.text), int.Parse(Hitdie.text), int.Parse(TotalHitDie.text), int.Parse(Initiative.text), 
            int.Parse(Speed.text), int.Parse(Armour_Class.text)));

            StartCoroutine(UpdateCharEquipment(Equipment.text, int.Parse(Copper.text),int.Parse(Silver.text),int.Parse(Gold.text),int.Parse(Platinum.text),int.Parse(Etherium.text)));

            StartCoroutine(UpdateInventory(Inventory.text));

        //if i change these to strings it wont beak i dont think

            StartCoroutine(UpdateCharStats(int.Parse(Strength.text) , int.Parse(Dexterity.text) , int.Parse(Constitution.text), int.Parse(Intelligence.text), int.Parse(Wisdom.text), int.Parse(Charisma.text),
            int.Parse(StrengthProf.text), int.Parse(DexterityProf.text), int.Parse(ConstitutionProf.text), int.Parse(IntelligenceProf.text), int.Parse(WisdomProf.text), int.Parse(CharismaProf.text), 
            int.Parse(StrengthSavingThrow.text), int.Parse(DexteritySavingThrow.text), int.Parse(ConstitutionSavingThrow.text), int.Parse(IntelligenceSavingThrow.text), int.Parse(WisdomSavingThrow.text),
            int.Parse(CharismaSavingThrow.text), int.Parse(Inspriation.text), int.Parse(ProfBonus.text), int.Parse(PassivePerception.text),int.Parse(Acrobatics.text),int.Parse(AnimalHandling.text),int.Parse(Arcana.text),
            int.Parse(Atheletics.text),int.Parse(Deception.text), int.Parse(History.text), int.Parse(Insight.text), int.Parse(Intimidation.text), int.Parse(Investigation.text), int.Parse(Medicine.text), int.Parse(Nature.text),
            int.Parse(Perception.text),int.Parse(Persuassion.text), int.Parse(Religion.text), int.Parse(SleightOfHand.text), int.Parse(Stealth.text), int.Parse(Survival.text)));

            StartCoroutine(UpdateCharWeapons(Extra_Attacks.text, Weapon1.text, Weapon2.text, Weapon3.text, Damage1.text, Damage2.text, Damage3.text, DamageType1.text, DamageType2.text, DamageType3.text));   

        }

        public void LoadDataButton()
        {
            //update the data for the text fields
            StartCoroutine(LoadDetails());
            StartCoroutine(LoadBackground());
            StartCoroutine(LoadHealth());
            StartCoroutine(LoadInventory());
            StartCoroutine(LoadStats());
            StartCoroutine(LoadLanguagesProfs());
            StartCoroutine(LoadWeapons());
        
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

        private IEnumerator UpdateCharDetails(string PlayerName, string Class_Level, string Race, string Alignment, string Background, string EXP)
        {
            //main line to update the database

            var DBTask1 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Details").Child("PlayerName").SetValueAsync(PlayerName);
            yield return new WaitUntil(predicate: () => DBTask1.IsCompleted);
            var DBTask2 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Details").Child("ClassLevel").SetValueAsync(Class_Level);
            yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);
            var DBTask3 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Details").Child("Race").SetValueAsync(Race);
            yield return new WaitUntil(predicate: () => DBTask3.IsCompleted);
            var DBTask4 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Details").Child("Alignment").SetValueAsync(Alignment);
            yield return new WaitUntil(predicate: () => DBTask4.IsCompleted);
            var DBTask5 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Details").Child("Background").SetValueAsync(Background);
            yield return new WaitUntil(predicate: () => DBTask5.IsCompleted);
            var DBTask6 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Details").Child("EXP").SetValueAsync(EXP);
            yield return new WaitUntil(predicate: () => DBTask6.IsCompleted);
            //wait until the task is completed
        
        }

        private IEnumerator UpdateCharBackground(string Traits, string Ideals, string Bonds, string Flaws)
        {
            //main line to update the database
            var DBTask7 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Background").Child("Traits").SetValueAsync(Traits);
            yield return new WaitUntil(predicate: () => DBTask7.IsCompleted);
            var DBTask8 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Background").Child("Ideals").SetValueAsync(Ideals);
            yield return new WaitUntil(predicate: () => DBTask8.IsCompleted);
            var DBTask9 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Background").Child("Bonds").SetValueAsync(Bonds);
            yield return new WaitUntil(predicate: () => DBTask9.IsCompleted);
            var DBTask10 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Background").Child("Flaws").SetValueAsync(Flaws);
            yield return new WaitUntil(predicate: () => DBTask10.IsCompleted);

        
        }
        private IEnumerator UpdateCharHealth(int TempHitPoints, int CurrentHitPoints, int HitPointMax, int Hitdie, int MaxHitdie, int Intiative, int Speed, int Armour_Class )
        {
            
            //main line to update the database
            var DBTask11 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Health").Child("TemporaryHP").SetValueAsync(TempHitPoints);
            yield return new WaitUntil(predicate: () => DBTask11.IsCompleted);
            var DBTask12 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Health").Child("CurrentHP").SetValueAsync(CurrentHitPoints);
            yield return new WaitUntil(predicate: () => DBTask12.IsCompleted);
            var DBTask13 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Health").Child("HP Max").SetValueAsync(HitPointMax);
            yield return new WaitUntil(predicate: () => DBTask13.IsCompleted);
            var DBTask14 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Health").Child("HitDie").SetValueAsync(Hitdie);
            yield return new WaitUntil(predicate: () => DBTask14.IsCompleted);
            var DBTask15 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Health").Child("MaxHitDie").SetValueAsync(MaxHitdie);
            yield return new WaitUntil(predicate: () => DBTask15.IsCompleted);
            var DBTask16 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Health").Child("Initiative").SetValueAsync(Intiative);
            yield return new WaitUntil(predicate: () => DBTask16.IsCompleted);
            var DBTask17 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Health").Child("Speed").SetValueAsync(Speed);
            yield return new WaitUntil(predicate: () => DBTask17.IsCompleted);
            var DBTask18 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Health").Child("ArmourClass").SetValueAsync(Armour_Class);
            yield return new WaitUntil(predicate: () => DBTask18.IsCompleted);
        }

        private IEnumerator UpdateCharEquipment(string Equipment, int Copper, int Silver, int Gold, int Platinum, int Etherium)
        {
            //main line to update the database
            var DBTask19 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Equipment").Child("Equipment").SetValueAsync(Equipment);
            yield return new WaitUntil(predicate: () => DBTask19.IsCompleted);
            var DBTask20 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Equipment").Child("Copper").SetValueAsync(Copper);
            yield return new WaitUntil(predicate: () => DBTask20.IsCompleted);
            var DBTask21 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Equipment").Child("Silver").SetValueAsync(Silver);
            yield return new WaitUntil(predicate: () => DBTask21.IsCompleted);
            var DBTask22 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Equipment").Child("Gold").SetValueAsync(Gold);
            yield return new WaitUntil(predicate: () => DBTask22.IsCompleted);
            var DBTask23 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Equipment").Child("Platinum").SetValueAsync(Platinum);
            yield return new WaitUntil(predicate: () => DBTask23.IsCompleted);
            var DBTask24 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Equipment").Child("Etherium").SetValueAsync(Etherium);
            yield return new WaitUntil(predicate: () => DBTask24.IsCompleted);
            //wait until the task is completed
        
        }

        private IEnumerator UpdateInventory(string Inventory)
        {
            //main line to update the database
            var DBTask25 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Inventory").Child("Inventory").SetValueAsync(Inventory);
            yield return new WaitUntil(predicate: () => DBTask25.IsCompleted);
            
        }

        private IEnumerator UpdateCharStats(int Strength, int Dexterity, int Constitution, int Intelligence, int Wisdom, int Charisma, int StrengthProf, int DexterityProf, int ConstitutionProf, int IntelligenceProf, 
        int WisdomProf, int CharismaProf, int StrengthSavingThrow, int DexteritySavingThrow, int ConstitutionSavingThrow, int IntelligenceSavingThrow, int WisdomSavingThrow, int CharismaSavingThrow, int Inspiration,
        int Prof_Bonus, int PassivePerception, int Acrobatics, int Animal_Handling, int Arcana, int Atheletics, int Deception, int History, int Insight, int Intimidatiom, int Investigation, int Medicine, int Nature, 
        int Perception, int Persuassion, int Religion, int Sleight_Of_Hand, int Stealth, int Survival)
        {
            //main line to update the database

            //Main stats
            var DBTask26 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Strength").SetValueAsync(Strength);
            yield return new WaitUntil(predicate: () => DBTask26.IsCompleted);
            var DBTask27 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Dexterity").SetValueAsync(Dexterity);
            yield return new WaitUntil(predicate: () => DBTask27.IsCompleted);
            var DBTask28 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Constitution").SetValueAsync(Constitution);
            yield return new WaitUntil(predicate: () => DBTask28.IsCompleted);
            var DBTask29 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Intelligence").SetValueAsync(Intelligence);
            yield return new WaitUntil(predicate: () => DBTask29.IsCompleted);
            var DBTask30 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Wisdom").SetValueAsync(Wisdom);
            yield return new WaitUntil(predicate: () => DBTask30.IsCompleted);
            var DBTask31 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Charisma").SetValueAsync(Charisma);
            yield return new WaitUntil(predicate: () => DBTask31.IsCompleted);

            //proficiencies
            var DBTask32 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("StrengthProf").SetValueAsync(StrengthProf);
            yield return new WaitUntil(predicate: () => DBTask32.IsCompleted);
            var DBTask33 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("DexterityProf").SetValueAsync(DexterityProf);
            yield return new WaitUntil(predicate: () => DBTask33.IsCompleted);
            var DBTask34 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("ConstitutionProf").SetValueAsync(ConstitutionProf);
            yield return new WaitUntil(predicate: () => DBTask34.IsCompleted);
            var DBTask35 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("IntelligenceProf").SetValueAsync(IntelligenceProf);
            yield return new WaitUntil(predicate: () => DBTask35.IsCompleted);
            var DBTask36 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("WisdomProf").SetValueAsync(WisdomProf);
            yield return new WaitUntil(predicate: () => DBTask36.IsCompleted);
            var DBTask37 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("CharismaProf").SetValueAsync(CharismaProf);
            yield return new WaitUntil(predicate: () => DBTask37.IsCompleted);

            //saving throws
            var DBTask38 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("StrengthSavingThrows").SetValueAsync(StrengthSavingThrow);
            yield return new WaitUntil(predicate: () => DBTask38.IsCompleted);
            var DBTask39 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("DexteritySavingThrows").SetValueAsync(DexteritySavingThrow);
            yield return new WaitUntil(predicate: () => DBTask39.IsCompleted);
            var DBTask40 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("ConstitutionSavingThrows").SetValueAsync(ConstitutionSavingThrow);
            yield return new WaitUntil(predicate: () => DBTask40.IsCompleted);
            var DBTask41 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("IntelligenceSavingThrows").SetValueAsync(IntelligenceSavingThrow);
            yield return new WaitUntil(predicate: () => DBTask41.IsCompleted);
            var DBTask42 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("WisdomSavingThrows").SetValueAsync(WisdomSavingThrow);
            yield return new WaitUntil(predicate: () => DBTask42.IsCompleted);
            var DBTask43 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("CharismaSavingThrows").SetValueAsync(CharismaSavingThrow);
            yield return new WaitUntil(predicate: () => DBTask43.IsCompleted);

            //bonus stats
            var DBTask44 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Inspiration").SetValueAsync(Inspiration);
            yield return new WaitUntil(predicate: () => DBTask44.IsCompleted);
            var DBTask45 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("ProfBonus").SetValueAsync(Prof_Bonus);
            yield return new WaitUntil(predicate: () => DBTask45.IsCompleted);
            var DBTask46 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("PassivePerception").SetValueAsync(PassivePerception);
            yield return new WaitUntil(predicate: () => DBTask46.IsCompleted);

            //Skills
            var DBTask47 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Acrobatics").SetValueAsync(Acrobatics);
            yield return new WaitUntil(predicate: () => DBTask47.IsCompleted);
            var DBTask48 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("AnimalHandling").SetValueAsync(Animal_Handling);
            yield return new WaitUntil(predicate: () => DBTask48.IsCompleted);
            var DBTask49 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Arcana").SetValueAsync(Arcana);
            yield return new WaitUntil(predicate: () => DBTask49.IsCompleted);
            var DBTask50 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Atheletics").SetValueAsync(Atheletics);
            yield return new WaitUntil(predicate: () => DBTask50.IsCompleted);
            var DBTask51 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Deception").SetValueAsync(Deception);
            yield return new WaitUntil(predicate: () => DBTask51.IsCompleted);
            var DBTask52 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("History").SetValueAsync(History);
            yield return new WaitUntil(predicate: () => DBTask52.IsCompleted);
            var DBTask53 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Insight").SetValueAsync(Insight);
            yield return new WaitUntil(predicate: () => DBTask53.IsCompleted);
            var DBTask54 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Intimidatiom").SetValueAsync(Intimidatiom);
            yield return new WaitUntil(predicate: () => DBTask54.IsCompleted);
            var DBTask55 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Investigation").SetValueAsync(Investigation);
            yield return new WaitUntil(predicate: () => DBTask55.IsCompleted);
            var DBTask56 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Medicine").SetValueAsync(Medicine);
            yield return new WaitUntil(predicate: () => DBTask56.IsCompleted);
            var DBTask57 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Nature").SetValueAsync(Nature);
            yield return new WaitUntil(predicate: () => DBTask57.IsCompleted);
            var DBTask58 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Perception").SetValueAsync(Perception);
            yield return new WaitUntil(predicate: () => DBTask58.IsCompleted);
            var DBTask59 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Persuassion").SetValueAsync(Persuassion);
            yield return new WaitUntil(predicate: () => DBTask59.IsCompleted);
            var DBTask60 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Religion").SetValueAsync(Religion);
            yield return new WaitUntil(predicate: () => DBTask60.IsCompleted);
            var DBTask61 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("SleightOfHand").SetValueAsync(Sleight_Of_Hand);
            yield return new WaitUntil(predicate: () => DBTask61.IsCompleted);
            var DBTask62 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Stealth").SetValueAsync(Stealth);
            yield return new WaitUntil(predicate: () => DBTask62.IsCompleted);
            var DBTask63 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Stats").Child("Survival").SetValueAsync(Survival);
            yield return new WaitUntil(predicate: () => DBTask63.IsCompleted);
        }

        private IEnumerator UpdateCharLanguagesProfs(string LanguagesandProfs)
        {
            var DBTask63 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Character Languages and profs").Child("Languages and Profs").SetValueAsync(LanguagesandProfs);
            yield return new WaitUntil(predicate: () => DBTask63.IsCompleted);
        }

        private IEnumerator UpdateCharWeapons(string ExtraAttacks, string Weapon1, string Weapon2, string Weapon3, string Damage1, string Damage2, string Damage3, string DamageType1, string DamageType2, string DamageType3)
        {
            var DBTask64 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Weapons").Child("ExtraAttacks").SetValueAsync(ExtraAttacks);
            yield return new WaitUntil(predicate: () => DBTask64.IsCompleted);
            var DBTask65 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Weapons").Child("Weapon1").SetValueAsync(Weapon1);
            yield return new WaitUntil(predicate: () => DBTask65.IsCompleted);
            var DBTask66 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Weapons").Child("Weapon2").SetValueAsync(Weapon2);
            yield return new WaitUntil(predicate: () => DBTask66.IsCompleted);
            var DBTask67 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Weapons").Child("Weapon3").SetValueAsync(Weapon3);
            yield return new WaitUntil(predicate: () => DBTask67.IsCompleted);
            var DBTask68 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Weapons").Child("Damage1").SetValueAsync(Damage1);
            yield return new WaitUntil(predicate: () => DBTask68.IsCompleted);
            var DBTask69 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Weapons").Child("Damage2").SetValueAsync(Damage2);
            yield return new WaitUntil(predicate: () => DBTask69.IsCompleted);
            var DBTask70 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Weapons").Child("Damage3").SetValueAsync(Damage3);
            yield return new WaitUntil(predicate: () => DBTask70.IsCompleted);
            var DBTask71 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Weapons").Child("DamageType1").SetValueAsync(DamageType1);
            yield return new WaitUntil(predicate: () => DBTask71.IsCompleted);
            var DBTask72 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Weapons").Child("DamageType2").SetValueAsync(DamageType2);
            yield return new WaitUntil(predicate: () => DBTask72.IsCompleted);
            var DBTask73 = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).Child("Weapons").Child("DamageType3").SetValueAsync(DamageType3);
            yield return new WaitUntil(predicate: () => DBTask73.IsCompleted);
        }

        public IEnumerator LoadDetails()
        {
            var DBTask = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning("Load Details Failed "+ DBTask.Exception);
                DatabaseWarning.text = "Database contains no Character Details - Please save some";
            }  
            else
            {
                DataSnapshot snapshot = DBTask.Result;
                try
                {
                    PlayerName.text = snapshot.Child("Details").Child("PlayerName").Value.ToString();
                    Class_Level.text = snapshot.Child("Details").Child("ClassLevel").Value.ToString();
                    Race.text = snapshot.Child("Details").Child("Race").Value.ToString();
                    Alignment.text = snapshot.Child("Details").Child("Alignment").Value.ToString();
                    Background.text = snapshot.Child("Details").Child("Background").Value.ToString();
                    Exp.text = snapshot.Child("Details").Child("PlayerName").Value.ToString();
                }
                catch
                {
                    Debug.Log("Database does not contain all Character Details");
                    PlayerName.text = "null";
                    Class_Level.text = "null";
                    Race.text = "null";
                    Alignment.text = "null";
                    Background.text = "null";
                    Exp.text = "null";
                }   
            }
        }

        public IEnumerator LoadBackground()
        {
            var DBTask = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).GetValueAsync();
            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning("Load Details Failed "+ DBTask.Exception);
                DatabaseWarning.text = "Database contains no Character background Details - Please save some";
            }
            else
            {
                DataSnapshot snapshot = DBTask.Result;
                try
                {
                    Traits.text = snapshot.Child("Background").Child("Traits").Value.ToString();
                    Ideals.text = snapshot.Child("Background").Child("Ideals").Value.ToString();
                    Bonds.text = snapshot.Child("Background").Child("Bonds").Value.ToString();
                    Flaws.text = snapshot.Child("Background").Child("Flaws").Value.ToString();
                }
                catch
                {
                    Debug.Log("Database does not contain all Character Background information");
                    Traits.text = "null";
                    Ideals.text = "null";
                    Bonds.text = "null";
                    Flaws.text = "null";
                }
            }
        }
        
        public IEnumerator LoadHealth()
        {
            var DBTask = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).GetValueAsync();
            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning("Load Details Failed "+ DBTask.Exception);
                DatabaseWarning.text = "Database contains no Character Health Details - Please save some";
            }
            //if there is no data to be loaded
            else if (DBTask.Result.Value == null)
            {
                //Set the data of all feilds to null orr 0
                DatabaseWarning.text = "Not all health fields in database have value";
                Armour_Class.text = "0";
                Initiative.text = "0";
                Speed.text = "0";
                HitPointMax.text = "0";
                CurrentHitPoints.text = "0";
                TempHitPoints.text = "0";
                TotalHitDie.text = "0";
                Hitdie.text = "0";

            }
            else
            {
                DataSnapshot snapshot = DBTask.Result;
                try
                {
                    Armour_Class.text = snapshot.Child("Health").Child("ArmourClass").Value.ToString();
                    Initiative.text = snapshot.Child("Health").Child("Initiative").Value.ToString();
                    Speed.text = snapshot.Child("Health").Child("Speed").Value.ToString();
                    HitPointMax.text = snapshot.Child("Health").Child("HPMax").Value.ToString();
                    CurrentHitPoints.text = snapshot.Child("Health").Child("CurrentHP").Value.ToString();
                    TempHitPoints.text = snapshot.Child("Health").Child("TemporaryHP").Value.ToString();
                    TotalHitDie.text = snapshot.Child("Health").Child("MaxHitDie").Value.ToString();
                    Hitdie.text = snapshot.Child("Health").Child("HitDie").Value.ToString();
                }
                catch
                {
                    Debug.Log("Database does not contain all Character Health information");
                }   
            }
        }
        
        public IEnumerator LoadEquipment()
        {
            var DBTask = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).GetValueAsync();
            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning("Load Details Failed "+ DBTask.Exception);
                DatabaseWarning.text = "Database contains no Character Equipment - Please save some";
            }
            //if there is no data to be loaded
            else if (DBTask.Result.Value == null)
            {
                //Set the data of all feilds to null orr 0
                DatabaseWarning.text = "Not all fields in database have value";
                Equipment.text = "null";
                Copper.text = "0";
                Silver.text = "0";
                Gold.text = "0";
                Platinum.text = "0";
                Etherium.text = "0";
            }
            else
            {
                DataSnapshot snapshot = DBTask.Result;                
                try
                {
                    Equipment.text = snapshot.Child("Equipment").Child("Equipment").Value.ToString();
                    Copper.text = snapshot.Child("Equipment").Child("Copper").Value.ToString();
                    Silver.text = snapshot.Child("Equipment").Child("Silver").Value.ToString();
                    Gold.text = snapshot.Child("Equipment").Child("Gold").Value.ToString();
                    Platinum.text = snapshot.Child("Equipment").Child("Platinum").Value.ToString();
                    Etherium.text = snapshot.Child("Equipment").Child("Etherium").Value.ToString();
                }
                catch
                {
                    Debug.Log("Database does not contain all Character Equipment");
                }   
            }
        }
        
        public IEnumerator LoadInventory()
        {
            var DBTask = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).GetValueAsync();
            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning("Load Details Failed "+ DBTask.Exception);
                DatabaseWarning.text = "Database contains no Inventory Details - Please save some";
            }
            //if there is no data to be loaded
            else if (DBTask.Result.Value == null)
            {
                //Set the data of all feilds to null orr 0
                DatabaseWarning.text = "Not all fields in database have value";
                Inventory.text = "0";
            }
            else
            {
                DataSnapshot snapshot = DBTask.Result;
                try
                {
                    Inventory.text = snapshot.Child("Inventory").Child("Inventory").Value.ToString();
                }
                catch
                {
                    Debug.Log("Database does not contain any inventory Data");
                    
                }   
            }
        }
        
        public IEnumerator LoadStats()
        {
            var DBTask = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).GetValueAsync();
            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning("Load Details Failed "+ DBTask.Exception);
                DatabaseWarning.text = "Database contains no Character Stats Details - Please save some";
            }
            //if there is no data to be loaded
            else if (DBTask.Result.Value == null)
            {
                //Set the data of all feilds to null orr 0
                DatabaseWarning.text = "Not all fields in database have value";
                Strength.text = "0";
                Dexterity.text = "0";
                Constitution.text = "0";
                Intelligence.text = "0";
                Wisdom.text = "0";
                Charisma.text = "0";
                PassivePerception.text = "0";
                Acrobatics.text = "0";
                AnimalHandling.text = "0";
                Arcana.text = "0";
                Atheletics.text = "0";
                Deception.text = "0";
                History.text = "0";
                Insight.text = "0";
                Intimidation.text = "0";
                Investigation.text = "0";
                Medicine.text = "0";
                Nature.text = "0";
                Perception.text = "0";
                Performance.text = "0";
                Persuassion.text = "0";
                Religion.text = "0";
                SleightOfHand.text = "0";
                Stealth.text = "0";
                Survival.text = "0";
                StrengthSavingThrow.text = "0";
                DexteritySavingThrow.text = "0";
                ConstitutionSavingThrow.text = "0";
                IntelligenceSavingThrow.text = "0";
                WisdomSavingThrow.text = "0";
                CharismaSavingThrow.text = "0";
                StrengthProf.text = "0";
                DexterityProf.text = "0";
                ConstitutionProf.text = "0";
                IntelligenceProf.text = "0";
                WisdomProf.text = "0";
                CharismaProf.text = "0";
                Inspriation.text = "0";
                ProfBonus.text = "0";

            }
            else
            {
                DataSnapshot snapshot = DBTask.Result;
                try{

                    Strength.text = snapshot.Child("Stats").Child("Strength").Value.ToString();
                    Dexterity.text = snapshot.Child("Stats").Child("Dexterity").Value.ToString();
                    Constitution.text = snapshot.Child("Stats").Child("Constitution").Value.ToString();
                    Intelligence.text = snapshot.Child("Stats").Child("Intelligence").Value.ToString();
                    Wisdom.text = snapshot.Child("Stats").Child("Wisdom").Value.ToString();
                    Charisma.text = snapshot.Child("Stats").Child("Charisma").Value.ToString();

                    PassivePerception.text = snapshot.Child("Stats").Child("PassivePerception").Value.ToString();
                    Acrobatics.text = snapshot.Child("Stats").Child("Acrobatics").Value.ToString();
                    AnimalHandling.text = snapshot.Child("Stats").Child("AnimalHandling").Value.ToString();
                    Arcana.text = snapshot.Child("Stats").Child("Arcana").Value.ToString();
                    Atheletics.text = snapshot.Child("Stats").Child("Atheletics").Value.ToString();
                    Deception.text = snapshot.Child("Stats").Child("Deception").Value.ToString();
                    History.text = snapshot.Child("Stats").Child("History").Value.ToString();
                    Insight.text = snapshot.Child("Stats").Child("Insight").Value.ToString();
                    Intimidation.text = snapshot.Child("Stats").Child("Intimidation").Value.ToString();
                    Investigation.text = snapshot.Child("Stats").Child("Investigation").Value.ToString();
                    Medicine.text = snapshot.Child("Stats").Child("Medicine").Value.ToString();
                    Nature.text = snapshot.Child("Stats").Child("Nature").Value.ToString();
                    Perception.text = snapshot.Child("Stats").Child("Perception").Value.ToString();
                    Performance.text = snapshot.Child("Stats").Child("Performance").Value.ToString();
                    Persuassion.text = snapshot.Child("Stats").Child("Persuassion").Value.ToString();
                    Religion.text = snapshot.Child("Stats").Child("Relgion").Value.ToString();
                    SleightOfHand.text = snapshot.Child("Stats").Child("SleightOfHand").Value.ToString();
                    Stealth.text = snapshot.Child("Stats").Child("Stealth").Value.ToString();
                    Survival.text = snapshot.Child("Stats").Child("Survival").Value.ToString();

                    StrengthSavingThrow.text = snapshot.Child("Stats").Child("StrengthSavingThrows").Value.ToString();
                    DexteritySavingThrow.text = snapshot.Child("Stats").Child("DexteritySavingThrows").Value.ToString();
                    ConstitutionSavingThrow.text = snapshot.Child("Stats").Child("ConstitutionSavingThrows").Value.ToString();
                    IntelligenceSavingThrow.text = snapshot.Child("Stats").Child("IntelligenceSavingThrows").Value.ToString();
                    WisdomSavingThrow.text = snapshot.Child("Stats").Child("WisdomSavingThrows").Value.ToString();
                    CharismaSavingThrow.text = snapshot.Child("Stats").Child("CharismaSavingThrows").Value.ToString();

                    StrengthProf.text = snapshot.Child("Stats").Child("StrengthProf").Value.ToString();
                    DexterityProf.text = snapshot.Child("Stats").Child("DexterityProf").Value.ToString();
                    ConstitutionProf.text = snapshot.Child("Stats").Child("ConstitutionProf").Value.ToString();
                    IntelligenceProf.text = snapshot.Child("Stats").Child("IntelligenceProf").Value.ToString();
                    WisdomProf.text = snapshot.Child("Stats").Child("WisdomProf").Value.ToString();
                    CharismaProf.text = snapshot.Child("Stats").Child("CharismaProf").Value.ToString();

                    Inspriation.text = snapshot.Child("Stats").Child("Inspriation").Value.ToString();
                    ProfBonus.text = snapshot.Child("Stats").Child("ProfBonus").Value.ToString();
                
                }
                catch
                {
                    Debug.Log("Database does not contain all Character Stats");
                }   
            }
        }
        
        public IEnumerator LoadLanguagesProfs()
        {
            var DBTask = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).GetValueAsync();
            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning("Load Details Failed "+ DBTask.Exception);
                DatabaseWarning.text = "Database contains no Character Langauge or prof Details - Please save some";
            }
            //if there is no data to be loaded
            else if (DBTask.Result.Value == null)
            {
                //Set the data of all feilds to null orr 0
                DatabaseWarning.text = "Not all fields in database have value";
                LanguagesandProfs.text = "null";
               

            }
            else
            {
                DataSnapshot snapshot = DBTask.Result;
                try{
                    LanguagesandProfs.text = snapshot.Child("Languagesandprofs").Child("CharLanguages&Profs").Value.ToString();
                }
                catch
                {
                    Debug.Log("Database does not contain all Languages and prof Details");
                }
            }
        }
        
        public IEnumerator LoadWeapons()
        {
            var DBTask = DBreference.Child("DungeonMasters").Child("users").Child(User.UserId).GetValueAsync();
            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning("Load Details Failed "+ DBTask.Exception);
                DatabaseWarning.text = "Database contains no Character Weapon Details - Please save some";
            }
            //if there is no data to be loaded
            else if (DBTask.Result.Value == null)
            {
                //Set the data of all feilds to null orr 0
                DatabaseWarning.text = "Not all fields in database have value";
                Weapon1.text = "null";
                Weapon2.text = "null";
                Weapon3.text = "null";
                DamageType1.text = "null";
                DamageType2.text = "null";
                DamageType3.text = "null";
                Damage1.text = "null";
                Damage2.text = "null";
                Damage3.text = "null";
                Extra_Attacks.text = "null";
            }
            else
            {
                DataSnapshot snapshot = DBTask.Result;
                try{

                    Weapon1.text = snapshot.Child("Weapons").Child("Weapon1").Value.ToString();
                    Weapon2.text = snapshot.Child("Weapons").Child("Weapon2").Value.ToString();
                    Weapon3.text = snapshot.Child("Weapons").Child("Weapon3").Value.ToString();
                    DamageType1.text = snapshot.Child("Weapons").Child("Damage1").Value.ToString();
                    DamageType2.text = snapshot.Child("Weapons").Child("Damage2").Value.ToString();
                    DamageType3.text = snapshot.Child("Weapons").Child("Damage3").Value.ToString();
                    Damage1.text = snapshot.Child("Weapons").Child("DamageType1").Value.ToString();
                    Damage2.text = snapshot.Child("Weapons").Child("DamageType2").Value.ToString();
                    Damage3.text = snapshot.Child("Weapons").Child("DamageType3").Value.ToString();
                    Extra_Attacks.text = snapshot.Child("Weapons").Child("ExtraAttacks").Value.ToString();   
                }
                catch
                {
                    Debug.Log("Missing weapon data");
                }
            } 
        }

    }
}

