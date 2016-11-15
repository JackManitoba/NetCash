using NetCashATM.Observers;
using NetCashATM.Presenters;
using NetCashATM.UserInterface.Buttons;
using System.Windows.Forms;

namespace NetCashATM.UserInterface.Panels
{  
        public class WithdrawalPanelATMError : ATMPanel
        {
            private static TextBox _amountEntryBox;
            private static Label _withdrawalLabel;
            private static Label _netCashLabel;
            private static Label _messageLabel;
            private static WithdrawalPresenter _withdrawalPresenter;

            public WithdrawalPanelATMError()
            {
                CreateChildControls();
                _withdrawalPresenter = new WithdrawalPresenter();
            }

            public override void CreateChildControls()
            {
                Name = "WithdrawalPanel";
                BackColor = System.Drawing.Color.White;
                Location = new System.Drawing.Point(109, 57);

                Size = new System.Drawing.Size(351, 194);
                TabIndex = 12;

                _withdrawalLabel = new Label();
                _withdrawalLabel.Text = "WITHDRAWAL";
                _withdrawalLabel.SetBounds(((this.Width / 2) - 40), (this.Height / 2), 100, 30);
                Controls.Add(_withdrawalLabel);

                _amountEntryBox = new System.Windows.Forms.TextBox();
                _amountEntryBox.Name = "ENTER AMOUNT";
                _amountEntryBox.ReadOnly = true;
                _amountEntryBox.Text = "";
                _amountEntryBox.SetBounds(((this.Width / 2) - 50), (this.Height / 2 + 30), 100, 40);
                Controls.Add(_amountEntryBox);

                _netCashLabel = new Label();
                _netCashLabel.Text = "NET-CASH";
                _netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
                Controls.Add(_netCashLabel);

                _messageLabel = new Label();
                _messageLabel.Text = "THIS ATM CANNOT DISPENSE AMOUNTS LESS THAN €10";
                _messageLabel.ForeColor = System.Drawing.Color.Red;
                _messageLabel.SetBounds(((this.Width / 2) - 70), ((this.Height / 2) - 70), 150, 40);
                Controls.Add(_messageLabel);
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
                _withdrawalPresenter.Withdraw(_amountEntryBox.Text);
            }

            public override TextBox GetInput()
            {
                return _amountEntryBox;
            }       
        }
    }
