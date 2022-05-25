using UnityEngine;
using Gamedata;

namespace Managers
{
    public class MainManager : MonoBehaviour
    {
        //singleton which means that it can be called from anywhere within the game
        public static MainManager Instance;

        //reference to the other files
        public MatchmakingManager matchmakingManager;
        public GameManager gameManager;

        public string currentLocalPlayerId; // You can use Firebase Auth to turn this into a userId. Just using the player name for a player id as an example for now!

        private void Awake() => Instance = this;

        private void Start()
        {
            Debug.Log("Start managers");

            matchmakingManager = GetComponent<MatchmakingManager>();
            gameManager = GetComponent<GameManager>();
            
        }
    }
}