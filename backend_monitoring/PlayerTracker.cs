using System;
using crud_operations;

namespace BackendMonitoring
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
