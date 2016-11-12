using NetCashATM.Interfaces;
using NetCashATM.UserInterface.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCashATM.UserInterface.Panels
{
    class LogoutPanel : ATMPanel
    {
        protected static Label _netCashLabel;
        protected static Label _message;
      
        public LogoutPanel()
        {
            this.Name = "LogoutPanel";
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(109, 57);
            this.Name = "logout";
            this.Size = new System.Drawing.Size(351, 194);
            this.TabIndex = 12;

            _netCashLabel = new Label();
            _netCashLabel.Text = "NET-CASH";
            _netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            this.Controls.Add(_netCashLabel);

            _message = new Label();
            _message.Text = "PLEASE TAKE YOUR CARD, PRESS ENTER TO CONTINUE";
            _message.SetBounds(((this.Width / 2) - 50), (this.Height / 2 + 10), 150, 40);
            this.Controls.Add(_message);
        }


        public override void Update(Subject e)
        {
            ATMButton b = (ATMButton)e;    
        }

        public override void Cancel()
        {
            NavData.SetNavigationPanelName("PIN");
            NotifyObservers();
        }

        public override void Clear()
        {
            NavData.SetNavigationPanelName("PIN");
            NotifyObservers();
        }

        public override void Enter()
        {
            NavData.SetNavigationPanelName("PIN");
            NotifyObservers();
        }
    }
}