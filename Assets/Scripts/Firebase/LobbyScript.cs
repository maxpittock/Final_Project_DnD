using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Handlers
{
    public class LobbyScript : MonoBehaviour
    {
        public TMP_InputField PlayerID;
        public TMP_Text warning;

        public void Play()
        {
            Debug.Log("Button pressed");
            warning.text = PlayerID.text;
            Debug.Log(PlayerID.text);
            //stores the input field into a variable
            MainManager.Instance.currentLocalPlayerId = PlayerID.text;
            //continues onto the next scene
            SceneManager.LoadScene("Matchmaking");
        }
    }
}