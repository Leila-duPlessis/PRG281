using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_operations
{

    // Create interface to control structure.
    internal interface I_CRUD_operations
    {
        void create();
        void view();
        void search();
        void update();
        void delete();
    }
}
