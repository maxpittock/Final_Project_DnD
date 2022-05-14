using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameHandler.Start");

        PlayerData player1 = new PlayerData();
        player1.ClassLevel = "Wizard, Level 1";
        player1.Background = "Street Kid";
        player1.PlayerName = "Sam";
        player1.Race = "Black";
        player1.Alignment = "Naught Naughty"; 
        player1.EXP = 50000;


        string json = JsonUtility.ToJson(player1);
        Debug.Log(json);
    }

    private class PlayerData
    {
        public string ClassLevel;
        public string Background;
        public string PlayerName;
        public string Race;
        public string Alignment;
        public int EXP;
    }
}
