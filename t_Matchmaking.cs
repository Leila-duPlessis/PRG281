using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BLNT_Game_Server.b_Manage_Players;

namespace BLNT_Game_Server
{
    internal class t_Matchmaking
    {
        private List<b_Player> queue = new List<b_Player>();
        private const int MAX_PLAYERS = 10;

        public void JoinQueue(b_Player player)
        {
            if (queue.Count >= MAX_PLAYERS)
            {
                Console.WriteLine("Queue is full!");
                return;
            }

            if (queue.Any(p => p.playerID == player.playerID))
            {
                Console.WriteLine("Player is already in the queue!");
                return;
            }

            queue.Add(player);
            player.playerStatus = PlayerStatus.inQueue; // BRANDON ADDED THIS
            Console.WriteLine(player.playerName + " joined the queue.");
        }

        public void LeaveQueue(string playerID)
        {
            b_Player player = queue.FirstOrDefault(p => p.playerID == playerID);

            if (player == null)
            {
                Console.WriteLine("Player is not in the queue.");
                return;
            }

            queue.Remove(player);
            player.playerStatus = PlayerStatus.Online; // BRANDON ADDED THIS
            Console.WriteLine(player.playerName + " left the queue.");
        }

        public void ViewQueue()
        {
            Console.WriteLine("\n--- MATCHMAKING QUEUE ---");

            if (queue.Count == 0)
            {
                Console.WriteLine("Queue is empty.");
                return;
            }

            foreach (b_Player player in queue)
            {
                Console.WriteLine(player.playerID + " - " +
                                  player.playerName + " - Level " +
                                  player.playerLvl);
            }

            Console.WriteLine("Players: " + queue.Count + "/10");
        }

        public void CreateMatch()
        {
            if (queue.Count < MAX_PLAYERS)
            {
                Console.WriteLine("Not enough players. 10 players are required.");
                return;
            }

            List<b_Player> players = queue.OrderBy(p => p.playerLvl).Take(10).ToList();

            int levelDifference = players[9].playerLvl - players[0].playerLvl;

            if (levelDifference > 10)
            {
                Console.WriteLine("Players have levels that are too far apart.");
                return;
            }

            Console.WriteLine("\n--- MATCH CREATED ---");
            Console.WriteLine("Team A:");

            for (int i = 0; i < 10; i += 2)
            {
                Console.WriteLine(players[i].playerName +
                                  " - Level " + players[i].playerLvl);
            }

            Console.WriteLine("\nTeam B:");

            for (int i = 1; i < 10; i += 2)
            {
                Console.WriteLine(players[i].playerName +
                                  " - Level " + players[i].playerLvl);
            }
            
            foreach (var player in players)
            {
                player.playerStatus = PlayerStatus.inMatch;
            } // BRANDON ADDED THIS FOREACH

            queue.RemoveRange(0, 10);
        }
    }
}
