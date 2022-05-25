using System;
using System.Collections.Generic;
using APIs;
using Firebase.Database;
using Firebase.Auth;
using Firebase;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{

    public class MatchmakingManager : MonoBehaviour
    {
        public FirebaseUser User;
        //reference to the database
        public DatabaseReference DBreference;

        //creates a listener to listen to see if player has joined
        private KeyValuePair<DatabaseReference, EventHandler<ValueChangedEventArgs>> queueListener;

        //Call back - gets executed once a game is found
        public void JoinQueue(string playerId, Action<string> onGameFound, Action<AggregateException> fallback) =>
            DatabaseAPI.PostObject($"DungeonMasters/matchmaking/{playerId}", "placeholder",
                // We listen for the placeholder value to change -called when data no the node is changed (one the match is found)
                () => queueListener = DatabaseAPI.ListenForValueChanged($"DungeonMasters/matchmaking/{playerId}",
                    args =>
                    {
                        // This code gets once the placeholder value is changed
                        var gameId =
                            StringSerializationAPI.Deserialize(typeof(string), args.Snapshot.GetRawJsonValue()) as
                                string;
                        if (gameId == "placeholder") return;
                        //when the game has been found leave the queue
                        LeaveQueue(playerId, () => onGameFound(
                            gameId), fallback);
                    }, fallback), fallback);


        public void LeaveQueue(string playerId, Action callback, Action<AggregateException> fallback)
        {
            DatabaseAPI.StopListeningForValueChanged(queueListener);
            DatabaseAPI.PostJSON($"DungeonMasters/matchmaking/{playerId}", "null", callback, fallback);
            SceneManager.LoadScene("Matchmaking");
        }
    }
}