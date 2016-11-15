using NetCashATM.Observers;
using NetCashATM.Presenters;
using NetCashATM.UserInterface.Buttons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace NetCashATM.UserInterface.Panels
{
    class MainPanel : ATMPanel
    {
        private MenuPresenter _menuPresenter;
        private List<Subject> _subjectList;
        protected static Label _netCashLabel;
        protected static Label _withdrawalLabel;
        protected static Label _depositLabel;
        protected static Label _balanceLabel;
        protected static Label _pinResetLabel;
        protected static Label _printReceiptLabel;
        protected static Label _exitLabel;

        public MainPanel()
        {
            CreateChildControls();
            _menuPresenter = new MenuPresenter();
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
               _menuPresenter.NavigateToSelected("WithdrawalPanel");
            }
            else if (b.Text == "2")
            {
                _menuPresenter.NavigateToSelected("BalancePanel");
            }
            else if (b.Text == "3")
            {
                _menuPresenter.NavigateToSelected("DepositPanel");
            }
            else if (b.Text == "4")
            {
                _menuPresenter.NavigateToSelected("PinResetPanel");
            }
            else if (b.Text == "5")
            {
                _menuPresenter.NavigateToSelected("PrintInfo");
            }
            else if (b.Text == "6")
            {
                _menuPresenter.NavigateToSelected("LogoutPanel");
            }
        }

        public override void Cancel()
        {
            _menuPresenter.NavigateToSelected("LogoutPanel");
        }

        public override void Clear()
        {
        }

        public override void Enter()
        {
        }
    }
}
