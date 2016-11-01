using ATMVERSION2.Interfaces;
using ATMVERSION2.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMVERSION2.UserInterface.Panels
{
    public class ATMPanel : Panel, Observer, Subject, Reciever

    {
        //ATMPANEL IS ACTING AS AN OBSERVER OF KEYPAD BUTTONS(SUBJECTS) -- WILL MAKE THIS CLASS IMPLEMENT OBSERVER INTERFACE 
        private List<Subject> subjectList;
        private List<Observer> observerList;
        public NavigationDataClass navData;
        public string name;
        public ATMPanel() { subjectList = new List<Subject>(); observerList = new List<Observer>();
            navData = new NavigationDataClass(); }

        List<Observer> Subject.observerList
        {
            get
            {
                return this.observerList;
            }
        }

        List<Subject> Observer.subjectList
        {
            get
            {
                return this.subjectList;
            }


        }

        public void notifyObservers()
        {
            foreach (Observer e in this.observerList)
            {
                e.update(this);
            }
        }

        public void registerObserver(Observer e)
        {
            this.observerList.Add(e);
        }

        public void unregisterObserver(Observer e)
        {
            this.observerList.Remove(e);
        }

        public void update()
        {

        }

        public virtual void update(Subject e)
        {

        }

        public void action()
        {
            throw new NotImplementedException();
        }
        public virtual TextBox getInput() { return null; }
        public virtual void cancel() { }
        public virtual void clear() { }
        public virtual void enter() { }
    }
}
