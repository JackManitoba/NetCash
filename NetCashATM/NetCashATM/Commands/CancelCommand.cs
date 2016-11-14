
namespace NetCashATM.Commands
{
    class CancelCommand : Command
    {
        private Reciever _reciever;

        public CancelCommand(Reciever r)
        {
            _reciever = r;
        }

        public void Execute()
        {
            _reciever.Cancel();
        }
    }
}
