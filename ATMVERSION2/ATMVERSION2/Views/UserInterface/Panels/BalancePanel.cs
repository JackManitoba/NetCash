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

   public  class BalancePanel : ATMPanel
    {
        protected static Label netCashLabel;
        protected static Label balanceLabel;
        protected static Label currentBalance;
        public BalancePanel()
        {
            this.name = "BalancePanel";
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(109, 57);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(351, 194);
            this.TabIndex = 12;

            netCashLabel = new Label();
            netCashLabel.Text = "NET-CASH";
            netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            this.Controls.Add(netCashLabel);

            balanceLabel = new Label();
            balanceLabel.Text = "CURRENT BALANCE";
            balanceLabel.SetBounds(((this.Width / 2) - 50), (this.Height / 2 + 10), 150, 40);
            this.Controls.Add(balanceLabel);

            currentBalance = new Label();
            currentBalance.Text = "€ XXXXXX.XX";//Check database
            currentBalance.SetBounds(((this.Width / 2) - 30), (this.Height / 2 + 50), 100, 40);
            this.Controls.Add(currentBalance);
        }

        public override void update(Subject e)
        {
            ATMButton b = (ATMButton)e;
          
        }

        public void showBalance(string accountBalance)
        {
            currentBalance.Text = "€ " + accountBalance;
            currentBalance.Update();
        }

        public override void cancel()
        {
            this.navData.setNavigationPanelName(0, "LOGOUT");
            notifyObservers();
        }
        public override void clear()
        {

            this.navData.setNavigationPanelName(0, "MAIN");
            notifyObservers();
        }
        public override void enter()
        {

            this.navData.setNavigationPanelName(0, "MAIN");
            notifyObservers();
        }
    }
}

