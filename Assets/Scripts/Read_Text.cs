using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class Read_Text : MonoBehaviour
{   
    //where to place the text    
    public Transform contentWindow;
    public GameObject readText;

    // Start is called before the first frame update
    void Start()
    {
        //get the file path - probs gonna need to change this to be some type of user input but this will do for now
        string readFromFilePath = Application.streamingAssetsPath + "/Notes/" + "Page I" + ".txt";
        
        //Reads all content in file and puts it in a list 
        List<string> FileData = File.ReadAllLines(readFromFilePath).ToList();

        //foreach line print to note object
        foreach (string line in FileData)
        {
            Instantiate(readText, contentWindow);

            readText.GetComponent<Text>().text = line;
        }
    }


}

