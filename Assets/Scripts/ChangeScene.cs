using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //Function for changing the scene
    public void LoadScene(string sceneName)
    {
        //Change the scene to the specficed one
        SceneManager.LoadScene(sceneName);
    }
}
