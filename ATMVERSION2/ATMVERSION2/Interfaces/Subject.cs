using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.Interfaces
{
    public interface Subject
    {
        List<Observer> observerList { get; }


        void notifyObservers();


        void registerObserver(Observer e);
        void unregisterObserver(Observer e);


    }
}
