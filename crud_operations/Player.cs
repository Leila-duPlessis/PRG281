using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static crud_operations.Manage_Players;

namespace crud_operations
{
    internal class Player
    {
        // Getting ans setting player properties
        internal string playerID { get; set; }
        internal string playerName { get; set; }
        internal int playerLvl { get; set; }
        internal Manage_Players.PlayerStatus playerStatus { get; set; }

        // Player Constructer
        public Player(string pID, string pN, int pL, PlayerStatus pS)
        {
            playerID = pID;
            playerName = pN;
            playerLvl = pL;
            playerStatus = pS;
        }


    }
}
