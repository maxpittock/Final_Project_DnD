using System;
using System.Collections.Generic;
using System.Linq;
using APIs;
using Firebase.Database;
using Gamedata;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        //game infor stored
        public GameInfo currentGameInfo;

        //create a bool to store weather the player is ready or not
        private Dictionary<string, bool> readyPlayers;
        //create listeners for players
        private KeyValuePair<DatabaseReference, EventHandler<ChildChangedEventArgs>> readyListener;
        private KeyValuePair<DatabaseReference, EventHandler<ValueChangedEventArgs>> localPlayerTurnListener;
        private KeyValuePair<DatabaseReference, EventHandler<ValueChangedEventArgs>> currentGameInfoListener;

        private readonly Dictionary<string, KeyValuePair<DatabaseReference, EventHandler<ChildChangedEventArgs>>>
            moveListeners =
                new Dictionary<string, KeyValuePair<DatabaseReference, EventHandler<ChildChangedEventArgs>>>();

        //getting the information set by the cloud function (index.js)
        public void GetCurrentGameInfo(string gameId, string localPlayerId, Action<GameInfo> callback,
            Action<AggregateException> fallback)
        {
            currentGameInfoListener =
                DatabaseAPI.ListenForValueChanged($"DungeonMasters/games/{gameId}/gameInfo", args =>
                {
                    if (!args.Snapshot.Exists) return;

                    var gameInfo =
                        StringSerializationAPI.Deserialize(typeof(GameInfo), args.Snapshot.GetRawJsonValue()) as
                            GameInfo;
                    currentGameInfo = gameInfo;
                    currentGameInfo.localPlayerId = localPlayerId;
                    DatabaseAPI.StopListeningForValueChanged(currentGameInfoListener);
                    callback(currentGameInfo);
                }, fallback);
        }

        //ready up for local player
        public void SetLocalPlayerReady(Action callback, Action<AggregateException> fallback)
        {
            DatabaseAPI.PostObject($"DungeonMasters/games/{currentGameInfo.gameId}/ready/{currentGameInfo.localPlayerId}", true,
                callback,
                fallback);
        }

        //function to listen to see if all players have readyed up and are ready to play
        public void ListenForAllPlayersReady(IEnumerable<string> playersId, Action<string> onNewPlayerReady,
            Action onAllPlayersReady,
            Action<AggregateException> fallback)
        {
            //dictionary to store ready players
            readyPlayers = playersId.ToDictionary(playerId => playerId, playerId => false);
            readyListener = DatabaseAPI.ListenForChildAdded( path: $"DungeonMasters/games/{currentGameInfo.gameId}/ready/", onChildAdded: args =>
            {
                //Set the player as ready in the dictionary
                readyPlayers[args.Snapshot.Key] = true;
                //
                onNewPlayerReady(args.Snapshot.Key);
                //go through whole dictionary and check that if all players are ready
                if (!readyPlayers.All(readyPlayer => readyPlayer.Value)) return;
                //if all players are ready - stop listening for ready players
                StopListeningForAllPlayersReady();
                //start the on all players ready function
                onAllPlayersReady();
            }, fallback);
        }

        public void StopListeningForAllPlayersReady() => DatabaseAPI.StopListeningForChildAdded(readyListener);

        /*public void SendMove(Move move, Action callback, Action<AggregateException> fallback)
        {
            DatabaseAPI.PushObject($"games/{currentGameInfo.gameId}/{currentGameInfo.localPlayerId}/moves/", move,
                () =>
                {
                    Debug.Log("Moved sent successfully!");
                    callback();
                }, fallback);
        }*/

        public void ListenForLocalPlayerTurn(Action onLocalPlayerTurn, Action<AggregateException> fallback)
        {
            localPlayerTurnListener =
                DatabaseAPI.ListenForValueChanged($"DungeonMasters/games/{currentGameInfo.gameId}/turn", args =>
                {
                    var turn =
                        StringSerializationAPI.Deserialize(typeof(string), args.Snapshot.GetRawJsonValue()) as string;
                    if (turn == currentGameInfo.localPlayerId) onLocalPlayerTurn();
                }, fallback);
        }

        public void StopListeningForLocalPlayerTurn() =>
            DatabaseAPI.StopListeningForValueChanged(localPlayerTurnListener);

        /*public void ListenForMoves(string playerId, Action<Move> onNewMove, Action<AggregateException> fallback)
        {
            moveListeners.Add(playerId, DatabaseAPI.ListenForChildAdded(
                $"games/{currentGameInfo.gameId}/{playerId}/moves/",
                args => onNewMove(
                    StringSerializationAPI.Deserialize(typeof(Move), args.Snapshot.GetRawJsonValue()) as Move),
                fallback));
        }

        public void StopListeningForMoves(string playerId)
        {
            DatabaseAPI.StopListeningForChildAdded(moveListeners[playerId]);
            moveListeners.Remove(playerId);
        }*/

        public void SetTurnToOtherPlayer(string currentPlayerId, Action callback, Action<AggregateException> fallback)
        {
            var otherPlayerId = currentGameInfo.playersIds.First(p => p != currentPlayerId);
            DatabaseAPI.PostObject(
                $"DungeonMasters/games/{currentGameInfo.gameId}/turn", otherPlayerId, callback, fallback);
        }
    }
}