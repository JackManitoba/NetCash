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
   public class WithdrawalPanel : ATMPanel
    {
        protected static TextBox _amountEntryBox;
        protected static Label _withdrawalLabel;
        protected static Label _netCashLabel;
        protected static Label _messageLabel;
        protected static WithdrawalPresenter _withdrawalPresenter;

        public WithdrawalPanel()
        {
            CreateChildControls();
            _withdrawalPresenter = new WithdrawalPresenter(this);
           
        }

        public  override void CreateChildControls()
        {
            Name = "WithdrawalPanel";
            BackColor = System.Drawing.Color.White;
            Location = new System.Drawing.Point(109, 57);

            Size = new System.Drawing.Size(351, 194);
            TabIndex = 12;

            _withdrawalLabel = new Label();
            _withdrawalLabel.Text = "WITHDRAWAL";
            _withdrawalLabel.SetBounds(((this.Width / 2) - 40), (this.Height / 2), 100, 30);
            this.Controls.Add(_withdrawalLabel);

            _amountEntryBox = new System.Windows.Forms.TextBox();
            _amountEntryBox.Name = "ENTER AMOUNT";
            _amountEntryBox.ReadOnly = true;
            _amountEntryBox.Text = "";
            _amountEntryBox.SetBounds(((this.Width / 2) - 50), (this.Height / 2 + 30), 100, 40);
            this.Controls.Add(_amountEntryBox);

            _netCashLabel = new Label();
            _netCashLabel.Text = "NET-CASH";
            _netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            this.Controls.Add(_netCashLabel);

            _messageLabel = new Label();
            _messageLabel.Text = "";
            _messageLabel.ForeColor = System.Drawing.Color.Red;
            _messageLabel.SetBounds(((this.Width / 2) - 70), ((this.Height / 2) - 70), 150, 40);
            this.Controls.Add(_messageLabel);

        }
        public void SetErrorMessage(string message)
        {

            _messageLabel.Text = message;
            _messageLabel.Update();
        }

        public override void Update(Subject e)
        {
            ATMButton b = (ATMButton)e;
            _amountEntryBox.Text += b.Text;
            _amountEntryBox.Update();
            _messageLabel.Text = "";
            _messageLabel.Update();
        }

        public override void Cancel()
        {
            _withdrawalPresenter.LogOut();
        
        }

        public override void Clear()
        {
            _amountEntryBox.Clear();
            _amountEntryBox.Update();
        }

        public override void Enter()
        {
            _withdrawalPresenter.Withdraw();
            
        }
        public override TextBox GetInput()
        {
            return _amountEntryBox;
        }

        public void DisplayMessage(string message)
        {
            _messageLabel.Text = message;
            Debug.WriteLine(_messageLabel.Text);
            _messageLabel.Update();
        }
    }
}
