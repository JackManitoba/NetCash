
namespace NetCashATM.Commands
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
