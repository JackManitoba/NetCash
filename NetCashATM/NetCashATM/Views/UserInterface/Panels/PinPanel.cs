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
   public class PinPanel : ATMPanel , Subject 
    {
        protected static TextBox _pinEntryBox;
        protected static Label _messageLabel;
        protected static Label _netCashLabel;

        public PinPanel()
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
                

        //PART OF OBSERVER DESIGN PATTERN -- SUBJECT PASSES ITSELF AS PARAMETER TO GET TEXT FROM AND UPDATES

        public override void Update(Subject e)
        {
            ATMButton b = (ATMButton)e;
            _pinEntryBox.Text += b.Text;
            _pinEntryBox.Update();
        }

        public override void Cancel()
        {
            NavData.SetNavigationPanelName("LOGOUT");
            NotifyObservers();
        }

        public override void Clear()
        {
            _pinEntryBox.Clear();
            _pinEntryBox.Update();
        }

        public override void Enter()
        {
                //pinEntryBox.Clear();
                NavData.SetNavigationPanelName("MAIN");
                NotifyObservers();           
        }

        public override TextBox GetInput()
        {
            return _pinEntryBox;
        }

        public void DisplayMessage(string message)
        {
            _messageLabel.Text = message;
            Debug.WriteLine(_messageLabel.Text);
            _messageLabel.Update();
        }
    }
}
