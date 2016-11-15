using NetCashATM.Observers;
using NetCashATM.Presenters;
using NetCashATM.UserInterface.Buttons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace NetCashATM.UserInterface.Panels
{
    public class CardCancelledPanel : ATMPanel
    {
        private LoginPresenter _loginPresenter;
        private static TextBox _pinEntryBox;
        private static Label _messageLabel;
        private static Label _netCashLabel;


        public CardCancelledPanel()
        {
            Debug.WriteLine("PinPanel.PinPanel()");
            CreateChildControls();
            _loginPresenter = new LoginPresenter();
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
            _messageLabel.Text = "THIS CARD HAS BEEN CANCELLED";
            _messageLabel.ForeColor = System.Drawing.Color.Red;
            _messageLabel.SetBounds(((this.Width / 2) - 70), ((this.Height / 2) - 70), 150, 40);
            Controls.Add(_messageLabel);
        }

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
    }
}
