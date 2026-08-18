using System;
using BLNT_Game_Server;

namespace BLNT_Game_Server
{
    public class PlayerTracker
    {
        private int currentOnlinePlayers;

        public void UpdatePlayerCount(int count)
        {
            currentOnlinePlayers = count;
            Console.WriteLine($"[Monitoring] Current online players: {currentOnlinePlayers}");
        }

        public int GetPlayerCount() => currentOnlinePlayers;
    }
}
