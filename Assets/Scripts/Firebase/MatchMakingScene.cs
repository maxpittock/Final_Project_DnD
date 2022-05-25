using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using APIs;
using Managers;
using UnityEngine.SceneManagement;

namespace Handlers
{
    
    public class MatchMakingScene : MonoBehaviour
    {
        public GameObject searchingPanel;
        public GameObject foundPanel;

        private bool gameFound;
        private bool readyingUp;
        private string gameId;

        private void Start() => JoinQueue();

        private void JoinQueue() => MainManager.Instance.matchmakingManager.JoinQueue(MainManager.Instance.currentLocalPlayerId, gameId =>
        {
            // This code gets executed once the game is found!
            this.gameId = gameId;
            //set gamefound to true which executes the gamefound func
            gameFound = true;
        }, Debug.Log);

        private void Update()
        {
            //if game is found
            if (!gameFound || readyingUp) return;
            //set readyup to true
            readyingUp = true;
            //run func
            GameFound();
        }

        private void GameFound()
        {
            //get the current game info
            MainManager.Instance.gameManager.GetCurrentGameInfo(gameId, MainManager.Instance.currentLocalPlayerId,
                gameInfo =>
                {
                    searchingPanel.SetActive(false);
                    foundPanel.SetActive(true);
                    Debug.Log("Game found. Ready-up!");
                    gameFound = true;
                    //listen to see if all players have readyed up
                    MainManager.Instance.gameManager.ListenForAllPlayersReady(gameInfo.playersIds,
                        //once all of the players are ready
                        playerId => Debug.Log(playerId + " is ready!"), () =>
                        {   
                            //load the next scene
                            Debug.Log("All players are ready!");
                            //loads game scene
                            SceneManager.LoadScene("MainMenu");
                        }, Debug.Log);
                }, Debug.Log);

            //changes the panels to inform the user a game has been found
            searchingPanel.SetActive(false);
            foundPanel.SetActive(true);
        }

        public void LeaveQueue()
        {
            if (gameFound) MainManager.Instance.gameManager.StopListeningForAllPlayersReady();
            else
                MainManager.Instance.matchmakingManager.LeaveQueue(MainManager.Instance.currentLocalPlayerId,
                    () => Debug.Log("Left queue successfully"), Debug.Log);
            SceneManager.LoadScene("MenuMenu");
        }

        public void Ready() =>
            MainManager.Instance.gameManager.SetLocalPlayerReady(() => Debug.Log("You are now ready!"), Debug.Log);

    }

}