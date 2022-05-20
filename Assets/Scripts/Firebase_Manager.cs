using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Firebase_Authentication
{

    public class Firebase_Manager : MonoBehaviour
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

        public static bool IsAuthenticated;

        //Login variables
        [Header("Login")]
        public TMP_InputField emailLoginField;
        public TMP_InputField passwordLoginField;
        public TMP_Text warningLoginText;
        public TMP_Text confirmLoginText;

        //Register variables
        [Header("Register")]
        public TMP_InputField usernameRegisterField;
        public TMP_InputField emailRegisterField;
        public TMP_InputField passwordRegisterField;
        public TMP_InputField passwordRegisterVerifyField;
        public TMP_Text warningRegisterText;
        public TMP_Text confirmRegisterText;
        
        [Header("Player_data")]
        public TMP_InputField PlayerName;


        private GameObject LoginUI;
        private GameObject RegisterUI;
        private GameObject MenuUI;

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
            auth = FirebaseAuth.DefaultInstance;
            DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        }


        public void ClearLoginInput()
        {
            //sets the input fields to no longer contain any data when returning to the login page
            emailLoginField.text = "";
            passwordLoginField.text = "";
        }
        public void ClearRegisterInput()
        {
            //sets the input fields to no longer contain any data when returning to the register page
            usernameRegisterField.text = "";
            emailRegisterField.text = "";
            passwordRegisterField.text = "";
            passwordRegisterVerifyField.text = "";
        }

        public void LoginButton()
        {
            //Call the login coroutine passing the email and password
            //when you click the login button it will start the login function
            Debug.Log("email" + emailLoginField.text);
            Debug.Log("password" + passwordLoginField.text);
            StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
        
        }
        public void RegisterButton()
        {
            //Call the login coroutine passing the email and password
            //when you click the register button it will start the register function
            StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
        }

        //Signing the user out with a button
        public void SignOutButton()
        {
            //sign the user out of their firebase account
            auth.SignOut();
            //load the login 
            IsAuthenticated = false;
            UIManager.instance.LoginScreen();
            //runs the clear inputbox functions
            ClearRegisterInput();
            ClearLoginInput();
        }


        //function for saving the users data
        public void SaveDataButton()
        {
            //update the data for the text fields
            StartCoroutine(UpdateUsernameAuth(PlayerName.text));
            StartCoroutine(UpdateUsernameDatabase(PlayerName.text));
        
        }
        //Login function
        public IEnumerator Login(string email, string password)
        {
            //calls the firebase authentication sign in function to pass the email and password
            var LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
            //this waits until the login task has been completed
            yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);
            //check if some type of exception occurs
            if(LoginTask.Exception != null)
            {
                //print error message to the console
                Debug.LogWarning(message: $"failed to register task with {LoginTask.Exception}");
                //checks the type of exception
                FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
                //gets the error code for the exception type
                AuthError errorcode = (AuthError)firebaseEx.ErrorCode;

                //set message variable
                string message = "Login failed :( ";
                //create switch statement using the error code to display what the user has inputted incorrectly
                switch (errorcode)
                {
                    //if email input missing
                    case AuthError.MissingEmail:
                    //display missing email messgae
                        message = "Missing email";
                        break;
                        //if password missing
                    case AuthError.MissingPassword:
                        message = "Missing password";
                        break;
                        //if password incorrect
                    case AuthError.WrongPassword:
                        message = "Inavlid password";
                        break;
                        //if email invalid
                    case AuthError.InvalidEmail:
                        message = "Wrong email";
                        break;
                    case AuthError.UserNotFound:
                        message = "User not found, please register an account";
                        break;

                }
                //display the warning message in the warning message textbox
                warningLoginText.text = message;
                IsAuthenticated = false;
            }
            //if nothing went wrong
            else
            {
                
                //set the login result under the user variable
                User = LoginTask.Result;
                //log a success message and welcome the user by their username when they log in in console
                Debug.LogFormat("User Signed in successfully: Welcome ", User.DisplayName, User.Email);
                //set the warning message as empty
                warningLoginText.text = "";
                //set the success message as logged in
                confirmLoginText.text = "Logged in!";

                //Wait for 2 seconds
                yield return new WaitForSeconds(2);
                //change text colour so is visible to user
                
                //welcome the player ith theirname (dynamic -will change depending on user)
                PlayerName.text = "Welcome " + User.DisplayName;
                //Change the scene to the specficed one
                UIManager.instance.MainMenu();
                //After 2 seconds set the confirm text to be empty
                confirmLoginText.text = "";
                ClearLoginInput();
                ClearRegisterInput();
                IsAuthenticated = true;
            }
        }

        private IEnumerator Register(string email, string password, string username)
        {
            //if username is blank
            if (username == "")
            {
                //set the warning text as missing username
                warningRegisterText.text = "Missing username!";
            }
            //if the two password boxes dont match
            else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
            {
                //inform the user with an error message
                warningRegisterText.text = "Passwords do not match!";
            }

            else
            {
            //calls the firebase authentication sign in function to pass the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
            //this waits until the register task has been completed
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);
            //check if some type of exception occurs
            if(RegisterTask.Exception != null)
            {
                //print error message to the console
                Debug.LogWarning(message: $"failed to register task with {RegisterTask.Exception}");
                //checks the type of exception
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                //gets the error code for the exception type
                AuthError errorcode = (AuthError)firebaseEx.ErrorCode;

                //set message variable
                string message = "Register failed";
                //create switch statement using the error code to display what the user has inputted incorrectly
                switch (errorcode)
                {
                    //if email input missing
                    case AuthError.MissingEmail:
                    //display missing email messgae
                        message = "Missing email";
                        break;
                        //if password missing
                    case AuthError.MissingPassword:
                        message = "Missing password";
                        break;
                        //if password incorrect
                    case AuthError.WeakPassword:
                        message = "Weak password";
                        break;
                        //if email invalid
                    case AuthError.EmailAlreadyInUse:
                        message = "Email already in use";
                        break;

                }
                //display the warning message in the warning message textbox
                warningLoginText.text = message;
                
            }
            else
            {
                //create variable for storing the result of the register task when succesful as a user
                User = RegisterTask.Result;
                //if the user is not null
                if (User != null)
                {
                    //Create a new user profile and set the username 
                    UserProfile Profile = new UserProfile {DisplayName = username};
                    //call the firebase auth update te user profile function passing the profile with the username (setting the username)
                    var ProfileTask = User.UpdateUserProfileAsync(Profile);
                    //waits until the profile task has been completed
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);
                    //if an exceptions occur
                    if (ProfileTask.Exception != null)
                    {
                        //Log debug message to console
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        //checks the type of exception
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        //gets the error code for the exception type
                        AuthError errorcode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Failed to set username";
                    }
                    else
                    {
                        //username is now set
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                        ClearLoginInput();
                        ClearRegisterInput();
                    }
                }
            }

            }

        }
        //updating database data

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


        

        

    }
}
