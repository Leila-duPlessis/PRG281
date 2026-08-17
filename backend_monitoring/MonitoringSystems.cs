using System;
using crud_operations;
using GameServerMatchMaking.Events;

namespace BackendMonitoring
{
    public class MonitoringSystem
    {
        private PlayerTracker tracker = new PlayerTracker();
        private ServerHealthCheck healthCheck = new ServerHealthCheck();
        private SystemStateManager stateManager = new SystemStateManager();
        private CleanupManager cleanup = new CleanupManager();

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

        public void MonitorPlayer(Player player)
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
