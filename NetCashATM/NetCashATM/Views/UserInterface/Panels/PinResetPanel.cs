using NetCashATM.Observers;
using NetCashATM.Presenters;
using NetCashATM.UserInterface.Buttons;
using System.Windows.Forms;

namespace NetCashATM.UserInterface.Panels
{
   public class PinResetPanel : ATMPanel
    {
        private PinResetPresenter _pinResetPresenter;
        protected static TextBox _pinEntryBox;
        protected static Label _pinResetLabel;
        protected static Label _netCashLabel;

        public PinResetPanel()
        {
            CreateChildControls();
            _pinResetPresenter = new PinResetPresenter();

        }

        public override void CreateChildControls()
        {
            Name = "PinResetPanel";
            BackColor = System.Drawing.Color.White;
            Location = new System.Drawing.Point(109, 57);

            Size = new System.Drawing.Size(351, 194);
            TabIndex = 12;

            _pinResetLabel = new Label();
            _pinResetLabel.Text = "ENTER NEW PIN";
            _pinResetLabel.SetBounds(((this.Width / 2) - 45), (this.Height / 2), 120, 40);
            Controls.Add(_pinResetLabel);

            _pinEntryBox = new System.Windows.Forms.TextBox();
            _pinEntryBox.Name = "ENTER PIN";
            _pinEntryBox.ReadOnly = true;
            _pinEntryBox.Text = "";
            _pinEntryBox.SetBounds(((this.Width / 2) - 50), ((this.Height / 2) + 40), 100, 40);
            Controls.Add(_pinEntryBox);

            _netCashLabel = new Label();
            _netCashLabel.Text = "NET-CASH";
            _netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            Controls.Add(_netCashLabel);
        }
        
        public override void Update(Subject e)
        {
            ATMButton b = (ATMButton)e;
            _pinEntryBox.Text += b.Text;
            _pinEntryBox.Update();
        }
        public override void Cancel()
        {
            _pinResetPresenter.GoToMainMenu();
        }

        public override void Clear()
        {
            _pinEntryBox.Clear();
            _pinEntryBox.Update();
        }

        public override void Enter()
        {
            _pinResetPresenter.ResetPin(_pinEntryBox.Text);
        }
        public override TextBox GetInput()
        {
            return _pinEntryBox;
        }
    }
}
