using NetCashATM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCashATM.ConcreteCommands
{
    class EnterCommand : Command
    {
        private Reciever _reciever;

        public EnterCommand(Reciever r)
        {
            _reciever = r;
        }

        public void Execute()
        {
            _reciever.Enter();
        }
    }
}
