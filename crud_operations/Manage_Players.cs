using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace crud_operations
{
    internal class Manage_Players : I_CRUD_operations
    {
        // Player list
        internal List<Player> players = new List<Player>();

        // Constructor
        public Manage_Players()
        {
            // DUMMY DATA FOR TESTING
            players.Add(new Player("PLAYER1", "ShadowHunter", 25, PlayerStatus.Online));
            players.Add(new Player("PLAYER2", "GhostWolf", 18, PlayerStatus.Offline));
            players.Add(new Player("PLAYER3", "DragonSlayer", 42, PlayerStatus.inMatch));
            players.Add(new Player("PLAYER4", "NightCrawler", 31, PlayerStatus.inQueue));
            players.Add(new Player("PLAYER5", "CyberKnight", 12, PlayerStatus.Online));
        }

        // This method is for searching for player in players list, if a player is return player object if not then returns null.
        internal Player searchPlayer(string playerID)
        {
            foreach (Player player in players)
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

        public void create()
        {
            string pID = "PLAYER" + (players.Count + 1);
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

            Player newPlayer = new Player(pID, pN, pL, pS);
            players.Add(newPlayer);
            Console.WriteLine("You have created a player successfully");

        }
        public void view()
        {
            foreach (Player player in players)
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

            Player foundPlayer = searchPlayer(playerID);

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

            Player foundPlayer = searchPlayer(playerID);

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
                Console.WriteLine("Player not found.");
            }
        }
        public void delete()
        {
            Console.WriteLine("Enter PlayerID that you wish to delete");
            Console.WriteLine("--- This choice will be permanent and it will be impossible to restore this player ---");
            string playerID = Console.ReadLine().ToUpper().Trim();

            Player player = searchPlayer(playerID);

            if (player != null)
            {
                players.Remove(player);
            }
            else
            {
                Console.WriteLine("Player does not exist");
            }
        }
    }
}
