namespace NetCashATM.Observers
{
    class RegistrationHelper
    {
        public RegistrationHelper(){ }

        public void registerObserverToSubject(Observer e, Subject i)
        {
            i.RegisterObserver(e);
        }

        public void UnregisterObserverToSubject(Observer e, Subject i)
        {
            i.UnregisterObserver(e);
        }
    }
}
