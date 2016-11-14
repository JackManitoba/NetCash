using NetCashATM.Observers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using NetCashATM.Commands;

namespace NetCashATM.UserInterface.Buttons
{
    public class ATMButton : Button, Subject, Invoker
    {
        //ATMBUTTON NOW ACTS AS A SUBJECT -- WILL BE MADE TO INHERIT KEYPADSUBJECT INTERFACE
        private List<Observer> _observerList;
        public ATMButton() { _observerList = new List<Observer>(); }


        List<Observer> Subject.ObserverList
        {
            get
            {
                return _observerList;
            }
        }



        //NOTIFIES ALL OBSERVERS(ATM PANELS) OF AND PASSESS ITSELF AS A PARAMETER 
        public void NotifyObservers()
        {
            Debug.WriteLine("NotifyObservers: " + "_observerList count: " + _observerList.Count);

            for(int i = 0; i < _observerList.Count; i++)
            {

                Debug.WriteLine("NotifyObservers: " + i.ToString());
                _observerList[i].Update(this);
            }

        }

        public void RegisterObserver(Observer e)
        {
            Debug.WriteLine("Register Observer(ATMButton):" + e.ToString());
            _observerList.Add(e);
        }

        //ADDS AN OBSERVER TO THE SUBJECT
        //public void registerObserver(ATMPanel e) { this.observerList.Add(e); }

        public void UnregisterObserver(Observer e)
        {

            Debug.WriteLine("Unregister Observer(ATMButton):" + e.ToString());
            _observerList.Remove(e);
        }


        public void ExecuteCommand(Command c)
        {
            c.Execute();
        }
    }

}
