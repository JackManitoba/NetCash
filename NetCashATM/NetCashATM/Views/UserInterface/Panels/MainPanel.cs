using NetCashATM.HelperClasses;
using NetCashATM.Interfaces;
using NetCashATM.Presenters;
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
    class MainPanel : ATMPanel
    {
        private MenuPresenter _menuPresenter;

        private List<Subject> _subjectList;
        private List<Observer> _observerList;
        public NavigationDataClass NavData;

        protected static Label _netCashLabel;
        protected static Label _withdrawalLabel;
        protected static Label _depositLabel;
        protected static Label _balanceLabel;
        protected static Label _pinResetLabel;
        protected static Label _printReceiptLabel;
        protected static Label _exitLabel;
        //protected static Label _confirmLabel;

        public MainPanel()
        {
            Debug.WriteLine("MainPanel.MainPanel()");
            CreateChildControls();
            _menuPresenter = new MenuPresenter(this);
        }

        public override void CreateChildControls()
        {

            this.Name = "MainPanel";
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(109, 57);

            this.Size = new System.Drawing.Size(351, 194);
            this.TabIndex = 12;

            _netCashLabel = new Label();
            _netCashLabel.Text = "NET-CASH";
            _netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            this.Controls.Add(_netCashLabel);

            /*
            _confirmLabel = new Label();
            _confirmLabel.Text = "";
            _confirmLabel.ForeColor = System.Drawing.Color.Blue;
            _confirmLabel.SetBounds(((this.Width / 2) - 70), ((this.Height / 2) - 70), 150, 40);
            this.Controls.Add(_confirmLabel);
            */

            _withdrawalLabel = new Label();
            _withdrawalLabel.Text = "1 : WITHDRAWAL";
            _withdrawalLabel.SetBounds(0, (this.Height / 2), 100, 40);
            this.Controls.Add(_withdrawalLabel);

            _depositLabel = new Label();
            _depositLabel.Text = "3 : DEPOSIT";
            _depositLabel.SetBounds(0, ((this.Height / 2) + 40), 100, 40);
            this.Controls.Add(_depositLabel);

            _printReceiptLabel = new Label();
            _printReceiptLabel.Text = "5 : PRINT RECEIPT";
            _printReceiptLabel.SetBounds(0, ((this.Height / 2) + 80), 150, 40);
            this.Controls.Add(_printReceiptLabel);

            _balanceLabel = new Label();
            _balanceLabel.Text = "BALANCE : 2";
            _balanceLabel.SetBounds((this.Width - 73), (this.Height / 2), 100, 40);
            this.Controls.Add(_balanceLabel);

            _pinResetLabel = new Label();
            _pinResetLabel.Text = "PIN RESET : 4";
            _pinResetLabel.SetBounds((this.Width - 80), ((this.Height / 2) + 40), 100, 40);
            this.Controls.Add(_pinResetLabel);

            _exitLabel = new Label();
            _exitLabel.Text = "EXIT : 6";
            _exitLabel.SetBounds((this.Width - 47), ((this.Height / 2) + 80), 100, 40);
            this.Controls.Add(_exitLabel);
        }



        public override void Update(Subject e)
        {
            ATMButton b = (ATMButton)e;
            Debug.WriteLine("MainPanel.Update: " + b.Text);
            if (b.Text == "1")
            {
               // _confirmLabel.Text = "WITHDRAWAL SELECTED, CONFIRM SELECTION? : ENTER";
                _menuPresenter.NavigateToSelected("WithdrawalPanel");
            }
            else if (b.Text == "2")
            {
                //_confirmLabel.Text = "BALANCE SELECTED, CONFIRM SELECTION? : ENTER";
                _menuPresenter.NavigateToSelected("BalancePanel");
            }
            else if (b.Text == "3")
            {
                //_confirmLabel.Text = "DEPOSIT SELECTED, CONFIRM SELECTION? : ENTER";
                _menuPresenter.NavigateToSelected("DepositPanel");
            }
            else if (b.Text == "4")
            {
                //_confirmLabel.Text = "PIN RESET SELECTED, CONFIRM SELECTION? : ENTER";
                _menuPresenter.NavigateToSelected("PinResetPanel");
            }
            else if (b.Text == "5")
            {
               // _confirmLabel.Text = "PRINT SELECTED, CONFIRM SELECTION? : ENTER";
                _menuPresenter.NavigateToSelected("PrintInfo");
            }
            else if (b.Text == "6")
            {
                //_confirmLabel.Text = "EXIT SELECTED, CONFIRM SELECTION? : ENTER";
                _menuPresenter.NavigateToSelected("LogoutPanel");
            }
        }

        public override void Cancel()
        {
            _menuPresenter.NavigateToSelected("LOGOUT");
           // NotifyObservers();
        }

        public override void Clear()
        {
            //_confirmLabel.Text = "";
            NavData.SetNavigationPanelName("");
        }

        /*    public override void Enter()
            {
                if (NavData.GetNavigationPanelName() != "")
                {
                    Debug.WriteLine("Selection : " + NavData.GetNavigationPanelName());
                    NotifyObservers();
                }
                else
                {
                    _confirmLabel.Text = "PLEASE ENTER A SELECTION";
                }
            }
            */



        public void NotifyObservers()
        {
            Debug.WriteLine("MainPanel.NotifyObservers SHOULD NOT BE CALLED");
            foreach (Observer e in _observerList)
            {
                e.Update(this);
            }
        }

        public void RegisterObserver(Observer e)
        {
            Debug.WriteLine("MainPanel.RegisterObserver SHOULD NOT BE CALLED");
            _observerList.Add(e);
        }

        public void UnregisterObserver(Observer e)
        {
            Debug.WriteLine("MainPanel.UnregisterObserver SHOULD NOT BE CALLED");
            _observerList.Remove(e);
        }

        public void Action()
        {
            throw new NotImplementedException();
        }










    }
}
