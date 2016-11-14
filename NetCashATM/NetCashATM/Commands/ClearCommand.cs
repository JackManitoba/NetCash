
namespace NetCashATM.Commands
{
    class ClearCommand : Command
    {
        private Reciever _reciever;

        public ClearCommand(Reciever r)
        {
            _reciever = r;
        }

        public void Execute()
        {
            _reciever.Clear();
        }
    }
}
