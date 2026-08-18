using BLNT_Game_Server;
using System;

namespace BLNT_Game_Server
{
    public class MonitoringSystem
    {
        private PlayerTracker tracker = new PlayerTracker();
        private ServerHealthCheck healthCheck = new ServerHealthCheck();
        private SystemStateManager stateManager = new SystemStateManager();
        private CleanupManager cleanup = new CleanupManager();

        // Subscribe to the in-file MatchmakingEventManager (defined below)
        public void SubscribeToEvents(MatchMakingEvents.MatchmakingEventManager eventManager)
        {
            eventManager.OnMatchFound += HandleMatchFound;
            eventManager.OnNotificationReceived += HandleNotification;
        }

        private void HandleMatchFound(object sender, MatchMakingEvents.MatchFoundEvent e)
        {
            Console.WriteLine($"[Monitoring] Match found: {e.MatchId} on {e.ServerIp} with {e.PlayerCount} players.");
            stateManager.SaveState($"Match {e.MatchId} active with {e.PlayerCount} players.");
        }

        private void HandleNotification(object sender, MatchMakingEvents.NotificationEvent e)
        {
            Console.WriteLine($"[Monitoring] Notification: {e.Message} at {e.Timestamp}");
        }

        // Original method signature that used the external DTO
        public void MonitorPlayer(crud_operations.Player player)
        {
            tracker.UpdatePlayerCount(1);
            Console.WriteLine($"[Monitoring] Player tracked: {player.playerName} (Level {player.playerLvl})");
        }

        public void RunHealthCheck()
        {
            bool healthy = healthCheck.IsServerHealthy();
            Console.WriteLine($"[Monitoring] Server health: {(healthy ? "OK" : "Issue detected")}");
        }

        public void PerformCleanup()
        {
            cleanup.PerformCleanup();
        }
    }
}

// Minimal in-file DTO namespace to avoid adding files.
// This is intentionally minimal and only supports what MonitoringSystem needs.
namespace crud_operations
{
    public class Player
    {
        public string playerName { get; set; }
        public int playerLvl { get; set; }

        public Player() { }
        public Player(string name, int lvl)
        {
            playerName = name;
            playerLvl = lvl;
        }
    }
}

// Minimal matchmaking events so SubscribeToEvents compiles.
// Keep these here so you do not need separate files.
namespace MatchMakingEvents
{
    public class MatchFoundEvent : EventArgs
    {
        public string MatchId { get; set; }
        public string ServerIp { get; set; }
        public int PlayerCount { get; set; }
    }

    public class NotificationEvent : EventArgs
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MatchmakingEventManager
    {
        public event EventHandler<MatchFoundEvent> OnMatchFound;
        public event EventHandler<NotificationEvent> OnNotificationReceived;

        // Helpers to raise events in tests/demo
        public void RaiseMatchFound(string matchId, string serverIp, int count)
            => OnMatchFound?.Invoke(this, new MatchFoundEvent { MatchId = matchId, ServerIp = serverIp, PlayerCount = count });

        public void RaiseNotification(string message)
            => OnNotificationReceived?.Invoke(this, new NotificationEvent { Message = message, Timestamp = DateTime.UtcNow });
    }
}