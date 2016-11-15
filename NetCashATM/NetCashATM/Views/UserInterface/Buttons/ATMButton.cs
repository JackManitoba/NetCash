using NetCashATM.Observers;
using System.Collections.Generic;
using System.Windows.Forms;
using NetCashATM.Commands;

namespace NetCashATM.UserInterface.Buttons
{
    public class ATMButton : Button, Subject, Invoker
    {
        private List<Observer> _observerList;

        public ATMButton()
        {
            _observerList = new List<Observer>();
        }

        List<Observer> Subject.ObserverList
        {
            get
            {
                return _observerList;
            }
        }

        public void NotifyObservers()
        {
            for(int i = 0; i < _observerList.Count; i++)
            {
                _observerList[i].Update(this);
            }
        }

        public void RegisterObserver(Observer e)
        {
            _observerList.Add(e);
        }

        public void UnregisterObserver(Observer e)
        {
            _observerList.Remove(e);
        }

        public void ExecuteCommand(Command c)
        {
            c.Execute();
        }
    }
}
