using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCashATM.Interfaces
{
    interface Invoker
    {
        void ExecuteCommand(Command command);
    }
}
