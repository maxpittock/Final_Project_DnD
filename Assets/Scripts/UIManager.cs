using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject LoginUI;
    public GameObject RegisterUI;
    // Start is called before the first frame update
    void Awake()
    {
        //if instance doesnt exist 
        if (instance == null)
        {
            //create it
            instance = this;
        }
        //if it doe 
        else if (instance != null)
        {
            //log message
            Debug.Log("Instance already exists, destorying object");
            //destroys object
            Destroy(this);
        }
    }

    //functions to change what the user is viewing (login or register)
    public void LoginScreen()
    {
        //set the login gameobject active
        LoginUI.SetActive(true);
        //set the regiter one false
        RegisterUI.SetActive(false);
    }
    public void RegisterScreen()
    {
        //set the login screen inactive
        LoginUI.SetActive(false);
        //set the register screen on
        RegisterUI.SetActive(true);
    }

}
