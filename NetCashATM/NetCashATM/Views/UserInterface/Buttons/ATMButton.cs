using NetCashATM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            foreach (Observer i in _observerList)
            {
                i.Update(this);
            }
        }

        public void RegisterObserver(Observer e)
        {
            _observerList.Add(e);
        }

        //ADDS AN OBSERVER TO THE SUBJECT
        //public void registerObserver(ATMPanel e) { this.observerList.Add(e); }

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
