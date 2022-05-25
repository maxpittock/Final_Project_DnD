const functions = require('firebase-functions');
const admin = require('firebase-admin');
admin.initializeApp(functions.config().firebase);

var database = admin.database();

//this function gets execute everytime theres a new node that eneters the matchmaking section of the database.
exports.matchmaker = functions.database.ref('DungeonMasters/matchmaking/{playerId}')
    .onCreate((snap, context) => {

        //generates game id
        var gameId = generateGameId();
        //get all matchmaking players
        database.ref('DungeonMasters/matchmaking').once('value').then(players => {
            //set second player null
            var secondPlayer = null;
            //foreach player check for "placeholder" - checking to see if a player is searching still (and is not the current player)
            players.forEach(player => {

                if (player.val() === "placeholder" && player.key !== context.params.playerId) {
                    //set the second player
                    secondPlayer = player;
                }
            });
            
            //if there is no second player retunr null
            if (secondPlayer === null) return null;

            
            database.ref("DungeonMasters/matchmaking").transaction(function (matchmaking) {

                // If any of the players gets into another game during the transaction, abort the operation
                if (matchmaking === null || matchmaking[context.params.playerId] !== "placeholder" || matchmaking[secondPlayer.key] !== "placeholder") return matchmaking;
                
                //set the new player IDs
                matchmaking[context.params.playerId] = gameId;
                matchmaking[secondPlayer.key] = gameId;
                return matchmaking;

            }).then(result => {

                if (result.snapshot.child(context.params.playerId).val() !== gameId) return;
                //setup game variable
                var game = {
                    //info about the game
                    gameInfo: {
                        //store gameID
                        gameId: gameId,
                        //all the players (only 2 at the moment)
                        playersIds: [context.params.playerId, secondPlayer.key]
                    },
                    //keep track of whos turn it is
                    turn: context.params.playerId
                }
                //all the data that was created (GameID and player IDs) is pushed into the created game
                database.ref("DungeonMasters/games/" + gameId).set(game).then(snapshot => {
                    //
                    database.ref("DungeonMasters/" + context.params.PlayerId).set(gameId)
                    //Log to the console that the game has been created successfully 
                    console.log("Game created successfully!")
                    return null;
                }).catch(error => {
                    console.log(error);
                });

                return null;

            }).catch(error => {
                console.log(error);
            });

            return null;
        }).catch(error => {
            console.log(error);
        });
    });

//Function generates the match id
function generateGameId() {
    //Provide the possible characters to use when creating an ID
    var possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    //sets the current game id as  nothing
    var gameId = "";
    // randomy pick 20 characters from the possible characters list and add that to the gamID string to create a randomly generated ID
    for (var j = 0; j < 20; j++) gameId += possibleChars.charAt(Math.floor(Math.random() * possibleChars.length));
    return gameId;
}
