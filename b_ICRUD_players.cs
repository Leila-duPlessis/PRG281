using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLNT_Game_Server
{
    internal interface b_ICRUD_players
    {
        void create();
        void view();
        void search();
        void update();
        void delete();
    }
}
