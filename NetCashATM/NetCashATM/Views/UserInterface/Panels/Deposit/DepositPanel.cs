using NetCashATM.UserInterface.Buttons;
using System.Windows.Forms;
using NetCashATM.Presenters;
using NetCashATM.Observers;

namespace NetCashATM.UserInterface.Panels
{
   public class DepositPanel : ATMPanel
    {
        private DepositPresenter _depositPresenter;
        protected static TextBox _amountEntryBox;
        protected static Label _depositLabel;
        protected static Label _netCashLabel;
        protected static Label _messageLabel;

        public DepositPanel()
        {
            CreateChildControls();
            _depositPresenter = new DepositPresenter();
        }

        public override void CreateChildControls()
        {
            this.Name = "DepositPanel";
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(109, 57);

            this.Size = new System.Drawing.Size(351, 194);
            this.TabIndex = 12;

            _depositLabel = new Label();
            _depositLabel.Text = "DEPOSIT";
            _depositLabel.SetBounds(((this.Width / 2) - 25), (this.Height / 2), 100, 30);
            this.Controls.Add(_depositLabel);

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

        public override void Update(Subject e)
        {
            ATMButton b = (ATMButton)e;
            _amountEntryBox.Text += b.Text;
            _amountEntryBox.Update();
            _messageLabel.Text = "";
            _messageLabel.Update();
        }

        public void SetErrorMessage(string message)
        {

            _messageLabel.Text = message;
            _messageLabel.Update();
        }

        public override void Cancel()
        {
            _depositPresenter.LogOut();
        }
        public override void Clear()
        {
            _amountEntryBox.Clear();
            _amountEntryBox.Update();
        }
        public override void Enter()
        {
            _depositPresenter.Deposit(_amountEntryBox.Text);
        }

        public override TextBox GetInput()
        {
            return _amountEntryBox;
        }
    }
}

