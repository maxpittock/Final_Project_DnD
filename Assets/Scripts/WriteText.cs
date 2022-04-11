using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WriteText : MonoBehaviour
{
    //create declaration of input field
    public InputField NoteTaker;

    public void Start()
    {
        //creates a folder for storing notes
        Directory.CreateDirectory(Application.streamingAssetsPath + "/Notes/"); 

        CreateTextFile();
    }

    public void OnClick()
    {
        
    }

    public void CreateTextFile()
    {
        //if no data is enetered then restart function
        if (NoteTaker.text == "")
        {
            return; 
        }


        //Creates the text file containing the inputted data
        string txtDocumentName = Application.streamingAssetsPath + "/Notes/" + "Page I" + ".txt";
        //check if the file already exists in the folder
        if (!File.Exists(txtDocumentName))
        {
            File.WriteAllText(txtDocumentName, "NOTES \n \n");
        }
        //add the input field data to the text file - essentially saving the file
        File.AppendAllText(txtDocumentName, NoteTaker.text + "\n");

    }

}
