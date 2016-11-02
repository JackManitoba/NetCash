using ATMVERSION2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.ConcreteCommands
{
    class CancelCommand : Command
    {
        private Reciever reciever;

        public CancelCommand(Reciever r)
        {
            this.reciever = r;
        }

        public void execute()
        {
            this.reciever.cancel();
        }
    }
}
