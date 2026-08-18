using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BLNT_Game_Server.b_Manage_Players;

namespace BLNT_Game_Server
{
    internal class b_Player
    {
        // Getting ans setting player properties
        internal string playerID { get; set; }
        internal string playerName { get; set; }
        internal int playerLvl { get; set; }
        internal b_Manage_Players.PlayerStatus playerStatus { get; set; }

        // Player Constructer
        public b_Player(string pID, string pN, int pL, PlayerStatus pS)
        {
            playerID = pID;
            playerName = pN;
            playerLvl = pL;
            playerStatus = pS;
        }
    }
}
