using ATMVERSION2.Interfaces;
using ATMVERSION2.UserInterface.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMVERSION2.UserInterface.Panels
{

    class LogoutPanel : ATMPanel
    {
        protected static Label netCashLabel;
        protected static Label Message;
      
        public LogoutPanel()
        {
            this.name = "LogoutPanel";
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(109, 57);
            this.Name = "logout";
            this.Size = new System.Drawing.Size(351, 194);
            this.TabIndex = 12;

            netCashLabel = new Label();
            netCashLabel.Text = "NET-CASH";
            netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            this.Controls.Add(netCashLabel);

            Message = new Label();
            Message.Text = "PLEASE TAKE YOUR CARD, PRESS ENTER TO CONTINUE";
            Message.SetBounds(((this.Width / 2) - 50), (this.Height / 2 + 10), 150, 40);
            this.Controls.Add(Message);


        }


        public override void update(Subject e)
        {
            ATMButton b = (ATMButton)e;
          
        }

        public override void cancel()
        {
            this.navData.setNavigationPanelName("PIN");
            notifyObservers();

        }
        public override void clear()
        {
            this.navData.setNavigationPanelName("PIN");
            notifyObservers();

        }
        public override void enter()
        {
            this.navData.setNavigationPanelName("PIN");
            notifyObservers();
        }

    }
}