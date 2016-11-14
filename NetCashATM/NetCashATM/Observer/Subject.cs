using System.Collections.Generic;

namespace NetCashATM.Observers
{
    public interface Subject
    {
        List<Observer> ObserverList { get; }

        void NotifyObservers();

        void RegisterObserver(Observer e);
        void UnregisterObserver(Observer e);
    }
}
