using NetCashATM.Observers;
using NetCashATM.Presenters;
using NetCashATM.UserInterface.Buttons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace NetCashATM.UserInterface.Panels
{
   public class PinPanel : ATMPanel 
    {
        private LoginPresenter _loginPresenter;
        private List<Subject> _subjectList;
        private List<Observer> _observerList;

        protected static TextBox _pinEntryBox;
        protected static Label _messageLabel;
        protected static Label _netCashLabel;

        public PinPanel()
        {
            Debug.WriteLine("PinPanel.PinPanel()");
            CreateChildControls();
            _loginPresenter = new LoginPresenter();
            //NavData = new NavigationDataClass();
            
        }

        public override void CreateChildControls()
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

          //  NavData.AddNavigaion("MAIN");
        }




        //COMMAND RELATED FUNCTIONS

        public override void Cancel()
        {
        }

        public override void Clear()
        {
            _pinEntryBox.Clear();
            _pinEntryBox.Update();
        }

        public override void Enter()
        {
            _loginPresenter.Login(_pinEntryBox.Text);
        }


        //PART OF OBSERVER DESIGN PATTERN -- SUBJECT PASSES ITSELF AS PARAMETER TO GET TEXT FROM AND UPDATES

        public override void Update(Subject e)
        {
            Debug.WriteLine("PinPanel.Update");
            ATMButton b = (ATMButton)e;
            _pinEntryBox.Text += b.Text;
            _pinEntryBox.Update();
        }

        public override TextBox GetInput()
        {
            return _pinEntryBox;
        }

        public void DisplayMessage(string message)
        {

            Debug.WriteLine("PinPanel.DisplayMessage");
            _messageLabel.Text = message;
            Debug.WriteLine(_messageLabel.Text);
            _messageLabel.Update();
        }

        

        public void NotifyObservers()
        {

            Debug.WriteLine("PinPanel.NotifyObservers SHOULD NOT BE CALLED");
            foreach (Observer e in _observerList)
            {
                e.Update(this);
            }
        }

        public void RegisterObserver(Observer e)
        {
            Debug.WriteLine("PinPanel.RegisterObserver SHOULD NOT BE CALLED");
            _observerList.Add(e);
        }

        public void UnregisterObserver(Observer e)
        {
            Debug.WriteLine("PinPanel.UnregisterObserver SHOULD NOT BE CALLED");
            _observerList.Remove(e);
        }

        public void Action()
        {
            throw new NotImplementedException();
        }
    }
}
