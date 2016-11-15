using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using NetCashATM.Observers;
using NetCashATM.Commands;

namespace NetCashATM.UserInterface.Panels
{
    public class ATMPanel : Panel, Observer, Reciever
    {
        public virtual void CreateChildControls() { }
        private List<Subject> _subjectList;
        private List<Observer> _observerList;

        public ATMPanel()
        {
            _subjectList = new List<Subject>();
            _observerList = new List<Observer>();
        }

        List<Subject> Observer.SubjectList
        {
            get
            {
                return _subjectList;
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

        public virtual void Update(Subject e)
        {
        }

        public void Action()
        {
        }

        public virtual TextBox GetInput() { return null; }
        public virtual void Cancel() { }
        public virtual void Clear() { }
        public virtual new void Enter() { }
    }
}
