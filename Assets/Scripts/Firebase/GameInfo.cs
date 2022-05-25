using System;

namespace Gamedata
{
    [Serializable]
    public class GameInfo
    {
        public string gameId;
        public string[] playersIds;
        public string localPlayerId;
    }
}