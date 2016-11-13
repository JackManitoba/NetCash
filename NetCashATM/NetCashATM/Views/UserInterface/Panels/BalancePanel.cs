using NetCashATM.Interfaces;
using NetCashATM.Presenters;
using NetCashATM.UserInterface.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCashATM.UserInterface.Panels
{

   public  class BalancePanel : ATMPanel
    {
        private BalancePresenter _balancePresenter;
        private static Label _netCashLabel;
        private static Label _balanceLabel;
        private static Label _currentBalance;

        public BalancePanel()
        {
            CreateChildControls();
            _balancePresenter = new BalancePresenter(this);
        }

        public override void CreateChildControls()
        {
            this.Name = "BalancePanel";
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(109, 57);

            this.Size = new System.Drawing.Size(351, 194);
            this.TabIndex = 12;

            _netCashLabel = new Label();
            _netCashLabel.Text = "NET-CASH";
            _netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            this.Controls.Add(_netCashLabel);

            _balanceLabel = new Label();
            _balanceLabel.Text = "CURRENT BALANCE";
            _balanceLabel.SetBounds(((this.Width / 2) - 50), (this.Height / 2 + 10), 150, 40);
            this.Controls.Add(_balanceLabel);

            _currentBalance = new Label();
            _currentBalance.Text = "€ XXXXXX.XX";//Check database
            _currentBalance.SetBounds(((this.Width / 2) - 30), (this.Height / 2 + 50), 100, 40);
            this.Controls.Add(_currentBalance);
        }

        public override void Update(Subject e)
        {
            ATMButton b = (ATMButton)e;         
        }

        public void ShowBalance(string accountBalance)
        {
            _currentBalance.Text = "€ " + accountBalance;
            _currentBalance.Update();
        }

        public override void Cancel()
        {
            _balancePresenter.LogOut();
        }
        public override void Clear()
        {
            _balancePresenter.GoToMain();
        }
        public override void Enter()
        {
            _balancePresenter.GoToMain();
        }
    }
}

