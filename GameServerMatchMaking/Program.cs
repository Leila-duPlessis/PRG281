using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameServerMatchMaking.Events.MatchMakingEvents;
using crud_operations;
using GameServerMatchMaking.Events;
using BackendMonitoring;

class Program
{
    static void Main(string[] args)
    {
        var eventManager = new MatchMakingEvents.MatchmakingEventManager();
        var monitor = new MonitoringSystem();

        monitor.SubscribeToEvents(eventManager);

        // Example player from CRUD
        var player = new Player("P001", "Leila", 10, Manage_Players.PlayerStatus.Online);
        monitor.MonitorPlayer(player);
        monitor.RunHealthCheck();

        // Trigger events
        eventManager.MatchFound("M123", "192.168.1.10", 10);
        eventManager.Notification("Server running smoothly.");

        // Cleanup
        monitor.PerformCleanup();
    }
}


/*namespace GameServerMatchMaking
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //
            //===========Example use of the events, it works!!!!!! ========

            // 1.  Create an instance
            MatchmakingEventManager eventManager = new MatchmakingEventManager();

            // 2. Subscribe to event
            eventManager.OnMatchFound += (sender, e) =>
            {
                Console.WriteLine($"[MATCH CREATED] ID: {e.MatchId} on Server {e.ServerIp} with {e.PlayerCount} players!");
            };

            // 3. Fire the event!
            eventManager.MatchFound("MATCH_901", "10.0.2.1", 8);// 1. Creates instance
            MatchmakingEventManager EventManager1 = new MatchmakingEventManager();

            // 2. Subscribe to your event using '+='
            EventManager1.OnMatchFound += (sender, e) =>
            {
                Console.WriteLine($"[MATCH CREATED] ID: {e.MatchId} on Server {e.ServerIp} with {e.PlayerCount} players!");
            };

            // 3. Fire the event!
            eventManager.MatchFound("MATCH_901", "10.0.2.1", 8);

            Console.ReadLine();
        }
    }
}
*/