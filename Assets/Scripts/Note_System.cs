using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Note_System : MonoBehaviour
{
    public string textEnter;
    public GameObject inputField;
    public GameObject textDisplay;

    public void StoreText()
    {
        textEnter = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = textEnter;
    }

}