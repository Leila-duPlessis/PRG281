using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLNT_Game_Server
{
    internal class b_Manage_Players : b_ICRUD_players
    {
        // Player list
        internal List<b_Player> players = new List<b_Player>();

        // Constructor
        public b_Manage_Players()
        {
            // DUMMY DATA FOR TESTING
            players.Add(new b_Player("PLAYER1", "ShadowHunter", 12, PlayerStatus.Online));
            players.Add(new b_Player("PLAYER2", "GhostWolf", 12, PlayerStatus.Offline));
            players.Add(new b_Player("PLAYER3", "DragonSlayer", 12, PlayerStatus.inMatch));
            players.Add(new b_Player("PLAYER4", "NightCrawler", 12, PlayerStatus.inQueue));
            players.Add(new b_Player("PLAYER5", "CyberKnight", 12, PlayerStatus.Online));
            players.Add(new b_Player("PLAYER6", "IronFang", 12, PlayerStatus.Online));
            players.Add(new b_Player("PLAYER7", "StormBreaker", 12, PlayerStatus.Offline));
            players.Add(new b_Player("PLAYER8", "PhantomStrike", 12, PlayerStatus.inQueue));
            players.Add(new b_Player("PLAYER9", "BlazeRunner", 12, PlayerStatus.Online));
            players.Add(new b_Player("PLAYER10", "VenomClaw", 12, PlayerStatus.inMatch));
            players.Add(new b_Player("PLAYER11", "TitanForge", 33, PlayerStatus.Online));
            players.Add(new b_Player("PLAYER12", "RogueSpectre", 16, PlayerStatus.Offline));
            players.Add(new b_Player("PLAYER13", "DarkSentinel", 38, PlayerStatus.inQueue));
            players.Add(new b_Player("PLAYER14", "FrostReaper", 24, PlayerStatus.Online));
            players.Add(new b_Player("PLAYER15", "CrimsonWolf", 45, PlayerStatus.inMatch));
        }

        // This method is for searching for player in players list, if a player exists return player object if not then returns null.
        internal b_Player searchPlayer(string playerID)
        {
            foreach (b_Player player in players)
            {
                if (player.playerID == playerID)
                {
                    return player;
                }
            }

            return null;
        }
        // Player status options
        public enum PlayerStatus
        {
            Offline = 1, Online = 2, inQueue = 3, inMatch = 4
        }

        // Change nextPlayerNumber too 1 if dummy data is removed
        private static int nextPlayerNumber = 16;
        public void create()
        {
            string pID = "PLAYER" + (nextPlayerNumber++);
            Console.WriteLine("Please enter username");
            string pN = Console.ReadLine();
            Console.WriteLine("Please enter player level");
            int pL = int.Parse(Console.ReadLine());
            Console.WriteLine("Select Player Status:");
            Console.WriteLine("1 ~ Offline");
            Console.WriteLine("2 ~ Online");
            Console.WriteLine("3 ~ InQueue");
            Console.WriteLine("4 ~ InMatch");

            int statusChoice = int.Parse(Console.ReadLine());
            PlayerStatus pS = (PlayerStatus)statusChoice;

            b_Player newPlayer = new b_Player(pID, pN, pL, pS);
            players.Add(newPlayer);
            Console.WriteLine("You have created a player successfully");

        }
        public void view()
        {
            foreach (b_Player player in players)
            {
                Console.WriteLine($"ID: {player.playerID}\n" +
                                  $"Name: {player.playerName}\n" +
                                  $"Level: {player.playerLvl}\n" +
                                  $"Status: {player.playerStatus}\n");
            }
        }
        public void search()
        {
            Console.WriteLine("Enter PlayerID");
            string playerID = Console.ReadLine().ToUpper().Trim();

            b_Player foundPlayer = searchPlayer(playerID);

            // If the previous method found a player on line: 18 (In this file) it will display found player and if not it will write player does not exist
            if (foundPlayer != null)
            {
                Console.WriteLine("Searched player info is below");
                Console.WriteLine($"ID: {foundPlayer.playerID}");
                Console.WriteLine($"Name: {foundPlayer.playerName}");
                Console.WriteLine($"Level: {foundPlayer.playerLvl}");
                Console.WriteLine($"Status: {foundPlayer.playerStatus}");
            }
            else
            {
                Console.WriteLine("Player does not exist");
            }
        }
        public void update()
        {
            Console.WriteLine("Enter PlayerID you wish to update");
            string playerID = Console.ReadLine().ToUpper().Trim();

            b_Player foundPlayer = searchPlayer(playerID);

            if (foundPlayer != null)
            {
                Console.WriteLine("Enter new username");
                foundPlayer.playerName = Console.ReadLine();

                Console.WriteLine("Enter new level");
                foundPlayer.playerLvl = int.Parse(Console.ReadLine());

                Console.WriteLine("Select New Status");
                Console.WriteLine("1 ~ Offline");
                Console.WriteLine("2 ~ Online");
                Console.WriteLine("3 ~ InQueue");
                Console.WriteLine("4 ~ InMatch");

                int choice = int.Parse(Console.ReadLine());
                foundPlayer.playerStatus = ((PlayerStatus)choice);

                Console.WriteLine("Player updated successfully!");
            }
            else
            {
                Console.WriteLine("Player does not exist.");
            }
        }
        public void delete()
        {
            Console.WriteLine("Enter PlayerID that you wish to delete");
            string playerID = Console.ReadLine().ToUpper().Trim();

            b_Player player = searchPlayer(playerID);
            if (player != null)
            {
                Console.WriteLine($"Are you sure you want to delete: {playerID}");
                Console.WriteLine("--- This choice will be permanent and it will be impossible to restore this player ---");
                Console.WriteLine("Y/N");
                string confirmDelete = Console.ReadLine().ToLower().Trim();

                if (confirmDelete == "y" || confirmDelete == "yes")
                {
                    players.Remove(player);
                    Console.WriteLine($"{playerID} was removed successfully");
                }
                else if (confirmDelete == "n" || confirmDelete == "no")
                {
                    Console.WriteLine("Player was not removed");
                    return;
                }
                else
                {
                    Console.WriteLine("Process was cancelled");
                    return;
                }

            }
            else
            {
                Console.WriteLine("Player does not exist");
            }
        }
    }
}
