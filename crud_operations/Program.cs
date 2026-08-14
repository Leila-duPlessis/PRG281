using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_operations
{
    // Enum menu creation
    enum menu { createPlayer = 1, viewPlayers = 2, searchPlayer = 3, updatePlayer = 4, deletePlayer = 5, exitMenu = 6 }
    internal class Program
    {
        static void Main(string[] args)
        {
            Manage_Players manager = new Manage_Players();

            // Exit control for while looped menu
            bool continueMenu = true;
            while (continueMenu)
            {
                // -> Menu frontend and reading of choice selected
                Console.WriteLine(@"
╔═══════════════════════════╗
║     BLNT GAME SERVER      ║
╚═══════════════════════════╝");
                Console.WriteLine("Player Management Menu");
                Console.WriteLine("=================================");
                Console.WriteLine();
                Console.WriteLine($"{(int)menu.createPlayer} ~ Create New Player");
                Console.WriteLine($"{(int)menu.viewPlayers} ~ View All Players");
                Console.WriteLine($"{(int)menu.searchPlayer} ~ Search For Player");
                Console.WriteLine($"{(int)menu.updatePlayer} ~ Update a Player");
                Console.WriteLine($"{(int)menu.deletePlayer} ~ Delete a Player");
                Console.WriteLine($"{(int)menu.exitMenu} ~ Exit");
                int choice = int.Parse(Console.ReadLine());
                menu option = (menu)choice;
                // <-

                // Actual menu option paths
                switch (option)
                {
                    case menu.createPlayer:
                        manager.create();
                        break;

                    case menu.viewPlayers:
                        manager.view();
                        break;

                    case menu.searchPlayer:
                        manager.search();
                        break;

                    case menu.updatePlayer:
                        manager.update();
                        break;

                    case menu.deletePlayer:
                        manager.delete();
                        break;

                    case menu.exitMenu:
                        continueMenu = false;
                        break;

                    default:
                        Console.WriteLine("kys");
                        break;
                }
            }
        }
    }
}
