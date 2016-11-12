using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCashATM.Interfaces
{
    public interface Subject
    {
        List<Observer> ObserverList { get; }

        void NotifyObservers();

        void RegisterObserver(Observer e);
        void UnregisterObserver(Observer e);
    }
}
