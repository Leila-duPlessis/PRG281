using BLNT_Game_Server;
using MatchMakingEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLNT_Game_Server
{
    enum menuMain { playerManagement = 1, matchManagement = 2, monitoringManagement = 3, exitMainMenu = 4 }
    enum menuPlayer { createPlayer = 1, viewPlayers = 2, searchPlayer = 3, updatePlayer = 4, deletePlayer = 5, exitMenu = 6 }
    enum menuMatch { joinQueue = 1, leaveQueue = 2, viewQueue = 3, createMatch = 4, exitMenu = 5 }
    enum menuMonitor { runHealthCheck = 1, trackPlayers = 2, cleanupSystem = 3, systemStatus = 4, exitMenu = 5 }
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continueMainMenu = true;

            b_Manage_Players manager = new b_Manage_Players();

            t_Matchmaking matchmaking = new t_Matchmaking();

            // Monitorin/events
            MatchmakingEventManager eventManager = new MatchmakingEventManager();
            MonitoringSystem monitor = new MonitoringSystem();
            monitor.SubscribeToEvents(eventManager);

            SystemStateManager systemStateManager = new SystemStateManager();

            while (continueMainMenu)
            {
                Console.WriteLine(@"
╔═══════════════════════════╗
║     BLNT GAME SERVER      ║
╚═══════════════════════════╝");
                Console.WriteLine("Server Management Menu");
                Console.WriteLine("=================================");
                Console.WriteLine();
                Console.WriteLine($"{(int)menuMain.playerManagement} ~ Player Management");
                Console.WriteLine($"{(int)menuMain.matchManagement} ~ Matchmaking");
                Console.WriteLine($"{(int)menuMain.monitoringManagement} ~ Monitoring");
                Console.WriteLine($"{(int)menuMain.exitMainMenu} ~ Exit");

                if (!int.TryParse(Console.ReadLine(), out int choiceMain))
                {
                    Console.WriteLine("Please select from the available numbers: 1 - 4");
                    continue;
                }

                menuMain optionMain = (menuMain)choiceMain;

                switch (optionMain)
                {
                    case menuMain.playerManagement:
                        bool continuePlayerMenu = true;

                        while (continuePlayerMenu)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Player Management Menu");
                            Console.WriteLine("=================================");
                            Console.WriteLine();
                            Console.WriteLine($"{(int)menuPlayer.createPlayer} ~ Create New Player");
                            Console.WriteLine($"{(int)menuPlayer.viewPlayers} ~ View All Players");
                            Console.WriteLine($"{(int)menuPlayer.searchPlayer} ~ Search For Player");
                            Console.WriteLine($"{(int)menuPlayer.updatePlayer} ~ Update a Player");
                            Console.WriteLine($"{(int)menuPlayer.deletePlayer} ~ Delete a Player");
                            Console.WriteLine($"{(int)menuPlayer.exitMenu} ~ Exit");

                            if (!int.TryParse(Console.ReadLine(), out int choicePlayer))
                            {
                                Console.WriteLine("Please select from the available numbers: 1 - 6");
                                continue;
                            }

                            menuPlayer optionPlayer = (menuPlayer)choicePlayer;
                            switch (optionPlayer)
                            {
                                case menuPlayer.createPlayer:
                                    manager.create();
                                    break;

                                case menuPlayer.viewPlayers:
                                    manager.view();
                                    break;

                                case menuPlayer.searchPlayer:
                                    manager.search();
                                    break;

                                case menuPlayer.updatePlayer:
                                    manager.update();
                                    break;

                                case menuPlayer.deletePlayer:
                                    manager.delete();
                                    break;

                                case menuPlayer.exitMenu:
                                    continuePlayerMenu = false;
                                    break;

                                default:
                                    Console.WriteLine("defaulted");
                                    break;
                            }
                        }
                        break;

                    case menuMain.matchManagement:
                        bool continueMatchMenu = true;

                        while (continueMatchMenu)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Matchmaking Menu");
                            Console.WriteLine("=================================");
                            Console.WriteLine();

                            Console.WriteLine($"{(int)menuMatch.joinQueue} ~ Join Queue");
                            Console.WriteLine($"{(int)menuMatch.leaveQueue} ~ Leave Queue");
                            Console.WriteLine($"{(int)menuMatch.viewQueue} ~ View Queue");
                            Console.WriteLine($"{(int)menuMatch.createMatch} ~ Create Match");
                            Console.WriteLine($"{(int)menuMatch.exitMenu} ~ Exit");

                            if (!int.TryParse(Console.ReadLine(), out int choiceMatch))
                            {
                                Console.WriteLine("Please select from the available numbers: 1 - 5");
                                continue;
                            }

                            menuMatch optionMatch = (menuMatch)choiceMatch;

                            switch (optionMatch)
                            {
                                case menuMatch.joinQueue:

                                    Console.WriteLine("Enter Player ID:");
                                    string joinID = Console.ReadLine().ToUpper().Trim();

                                    b_Player joinPlayer = manager.searchPlayer(joinID);

                                    if (joinPlayer != null)
                                    {
                                        matchmaking.JoinQueue(joinPlayer);
                                        eventManager.RaiseNotification($"{joinPlayer.playerName} joined the queue");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Player not found.");
                                    }

                                    break;

                                case menuMatch.leaveQueue:

                                    Console.WriteLine("Enter Player ID:");
                                    string leaveID = Console.ReadLine().ToUpper().Trim();

                                    b_Player leavePlayer = manager.searchPlayer(leaveID);

                                    if (leavePlayer != null)
                                    {
                                        matchmaking.LeaveQueue(leavePlayer.playerID);
                                        eventManager.RaiseNotification($"{leavePlayer.playerName} left the queue");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Player not found.");
                                    }

                                    break;

                                case menuMatch.viewQueue:

                                    matchmaking.ViewQueue();
                                    break;

                                case menuMatch.createMatch:

                                    matchmaking.CreateMatch();
                                    eventManager.RaiseMatchFound($"MATCH-{DateTime.UtcNow.Ticks}", "127.0.0.1", 10);
                                    break;

                                case menuMatch.exitMenu:

                                    continueMatchMenu = false;
                                    break;

                                default:

                                    Console.WriteLine("Invalid Option");
                                    break;
                            }
                        }
                        break;

                    case menuMain.monitoringManagement:
                        bool continueMonitorMenu = true;

                        while (continueMonitorMenu)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Monitoring Menu");
                            Console.WriteLine("======================");
                            Console.WriteLine("1 ~ Run Health Check");
                            Console.WriteLine("2 ~ Track Players");
                            Console.WriteLine("3 ~ Perform Cleanup");
                            Console.WriteLine("4 ~ System Status");
                            Console.WriteLine("5 ~ Exit");

                            if (!int.TryParse(Console.ReadLine(), out int choiceMonitor))
                            {
                                Console.WriteLine("Please select from the available options.");
                                continue;
                            }

                            menuMonitor optionMonitor = (menuMonitor)choiceMonitor;

                            switch (optionMonitor)
                            {
                                case menuMonitor.runHealthCheck:
                                    monitor.RunHealthCheck();
                                    break;

                                case menuMonitor.trackPlayers:
                                    
                                    foreach (var p in manager.players)
                                    {
                                        var dto = new crud_operations.Player(p.playerName, p.playerLvl);
                                        monitor.MonitorPlayer(dto);
                                    }
                                   
                                    eventManager.RaiseNotification("Manual tracking run completed.");
                                    break;

                                case menuMonitor.cleanupSystem:
                                    monitor.PerformCleanup();
                                    break;

                                case menuMonitor.systemStatus:
                                    string state = systemStateManager.LoadState();
                                    if (!string.IsNullOrEmpty(state))
                                    {
                                        Console.WriteLine("Saved system state:");
                                        Console.WriteLine(state);
                                    }
                                    else
                                    {
                                        Console.WriteLine("No saved system state available.");
                                    }
                                    break;

                                case menuMonitor.exitMenu:
                                    continueMonitorMenu = false;
                                    break;
                            }
                        }
                        break;

                    case menuMain.exitMainMenu:
                        continueMainMenu = false;
                        break;

                    default:
                        Console.WriteLine("defaulted");
                        break;
                }
            }
        }
    }
}