using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using NetCashATM.Observers;
using NetCashATM.Commands;

namespace NetCashATM.UserInterface.Panels
{
    public class ATMPanel : Panel, Observer, Subject, Reciever
    {
        public virtual void CreateChildControls() { }
        //ATMPANEL IS ACTING AS AN OBSERVER OF KEYPAD BUTTONS(SUBJECTS) -- WILL MAKE THIS CLASS IMPLEMENT OBSERVER INTERFACE 
        private List<Subject> _subjectList;
        private List<Observer> _observerList;
        public string Name;

        public ATMPanel()
        {
            _subjectList = new List<Subject>();
            _observerList = new List<Observer>();
        }

        List<Observer> Subject.ObserverList
        {
            get
            {
                return _observerList;
            }
        }

        List<Subject> Observer.SubjectList
        {
            get
            {
                return _subjectList;
            }
        }

        public void NotifyObservers()
        {
            foreach (Observer e in _observerList)
            {
                Debug.WriteLine("ATMPanel.NotifyObservers SHOULD NOT BE CALLED");
                e.Update(this);
            }
        }

        public void RegisterObserver(Observer e)
        {

            Debug.WriteLine("ATMPanel.RegisterObserver SHOULD NOT BE CALLED");
            _observerList.Add(e);
        }

        public void UnregisterObserver(Observer e)
        {

            Debug.WriteLine("ATMPanel.UnregisterObserver SHOULD NOT BE CALLED");
            _observerList.Remove(e);
        }

        //????????
        public void Update()
        {

        }

        //????????
        public virtual void Update(Subject e)
        {

        }

        //????????
        public void Action()
        {
            throw new NotImplementedException();
        }

        //????????
        public virtual TextBox GetInput() { return null; }
        public virtual void Cancel() { }
        public virtual void Clear() { }
        public virtual void Enter() { }
    }
}
