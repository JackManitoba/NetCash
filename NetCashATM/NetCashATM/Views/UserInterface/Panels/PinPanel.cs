using NetCashATM.Presenters;
using NetCashATM.HelperClasses;
using NetCashATM.Interfaces;
using NetCashATM.UserInterface.Buttons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCashATM.UserInterface.Panels
{
   public class PinPanel : Panel, ATMPanel , Subject 
    {
        private PINPresenter _pinPresenter;
        private List<Subject> _subjectList;
        private List<Observer> _observerList;
        public NavigationDataClass NavData;

        protected static TextBox _pinEntryBox;
        protected static Label _messageLabel;
        protected static Label _netCashLabel;

        public PinPanel()
        {
            _pinPresenter = new PINPresenter(this);
            _subjectList = new List<Subject>();
            _observerList = new List<Observer>();
            NavData = new NavigationDataClass();
            CreateChildControls();
        }

        public void CreateChildControls()
        {
            Name = "PinPanel";
            BackColor = System.Drawing.Color.White;
            Location = new System.Drawing.Point(109, 57);
            
            Size = new System.Drawing.Size(351, 194);
            TabIndex = 12;

            _pinEntryBox = new TextBox();

            _pinEntryBox.ReadOnly = true;
            _pinEntryBox.Name = "ENTER PIN";
            _pinEntryBox.Text = "";
            _pinEntryBox.SetBounds(((this.Width / 2) - 50), this.Height / 2, 100, 40);
            Controls.Add(_pinEntryBox);

            _netCashLabel = new Label();
            _netCashLabel.Text = "NET-CASH";
            _netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            Controls.Add(_netCashLabel);

            _messageLabel = new Label();
            _messageLabel.Text = "";
            _messageLabel.ForeColor = System.Drawing.Color.Red;
            _messageLabel.SetBounds(((this.Width / 2) - 70), ((this.Height / 2) - 70), 150, 40);
            Controls.Add(_messageLabel);

            NavData.AddNavigaion("MAIN");
        }




        //COMMAND RELATED FUNCTIONS

        public void Cancel()
        {
            NavData.SetNavigationPanelName("LOGOUT");
            NotifyObservers();
        }

        public void Clear()
        {
            _pinEntryBox.Clear();
            _pinEntryBox.Update();
        }

        public new void Enter()
        {
            NavData.SetNavigationPanelName("MAIN");
            NotifyObservers();
        }


        //PART OF OBSERVER DESIGN PATTERN -- SUBJECT PASSES ITSELF AS PARAMETER TO GET TEXT FROM AND UPDATES

        public void Update(Subject e)
        {
            ATMButton b = (ATMButton)e;
            _pinEntryBox.Text += b.Text;
            _pinEntryBox.Update();
        }

        public TextBox GetInput()
        {
            return _pinEntryBox;
        }

        public void DisplayMessage(string message)
        {
            _messageLabel.Text = message;
            Debug.WriteLine(_messageLabel.Text);
            _messageLabel.Update();
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
                e.Update(this);
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

        public void Action()
        {
            throw new NotImplementedException();
        }
    }
}
