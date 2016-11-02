using ATMVERSION2.Interfaces;
using ATMVERSION2.UserInterface.Buttons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMVERSION2.UserInterface.Panels
{
    class MainPanel : ATMPanel
    {
        protected static Label netCashLabel;
        protected static Label withdrawalLabel;
        protected static Label depositLabel;
        protected static Label balanceLabel;
        protected static Label pinResetLabel;
        protected static Label printReceiptLabel;
        protected static Label exitLabel;
        protected static Label confirmLabel;
        public MainPanel()
        {
            this.name = "MainPanel";
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(109, 57);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(351, 194);
            this.TabIndex = 12;

            netCashLabel = new Label();
            netCashLabel.Text = "NET-CASH";
            netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            this.Controls.Add(netCashLabel);

            confirmLabel = new Label();
            confirmLabel.Text = "";
            confirmLabel.ForeColor = System.Drawing.Color.Blue;
            confirmLabel.SetBounds(((this.Width / 2) - 70), ((this.Height / 2)-70), 150, 40);
            this.Controls.Add(confirmLabel);

            withdrawalLabel = new Label();
            withdrawalLabel.Text = "1 : WITHDRAWAL";
            withdrawalLabel.SetBounds(0, (this.Height / 2), 100, 40);
            this.Controls.Add(withdrawalLabel);

            depositLabel = new Label();
            depositLabel.Text = "3 : DEPOSIT";
            depositLabel.SetBounds(0, ((this.Height / 2) + 40), 100, 40);
            this.Controls.Add(depositLabel);

            printReceiptLabel = new Label();
            printReceiptLabel.Text = "5 : PRINT RECEIPT";
            printReceiptLabel.SetBounds(0, ((this.Height / 2) + 80), 150, 40);
            this.Controls.Add(printReceiptLabel);

            balanceLabel = new Label();
            balanceLabel.Text = "BALANCE : 2";
            balanceLabel.SetBounds((this.Width - 73), (this.Height / 2), 100, 40);
            this.Controls.Add(balanceLabel);

            pinResetLabel = new Label();
            pinResetLabel.Text = "PIN RESET : 4";
            pinResetLabel.SetBounds((this.Width - 80), ((this.Height / 2) + 40), 100, 40);
            this.Controls.Add(pinResetLabel);

            exitLabel = new Label();
            exitLabel.Text = "EXIT : 6";
            exitLabel.SetBounds((this.Width - 47), ((this.Height / 2) + 80), 100, 40);
            this.Controls.Add(exitLabel);
        }
          
    
        public override void update(Subject e)
        {
            ATMButton b = (ATMButton)e;
            if (b.Text == "1")
            {
                confirmLabel.Text = "WITHDRAWAL SELECTED, CONFIRM SELECTION? : ENTER";
                //Change to withdrawalLabel Panel
                this.navData.setNavigationPanelName("WITHDRAWAL");
            }
            else if (b.Text == "2")
            {
                confirmLabel.Text = "BALANCE SELECTED, CONFIRM SELECTION? : ENTER";
                //Change to balanceLabel Panel
                this.navData.setNavigationPanelName("BALANCE");
            }
            else if (b.Text == "3")
            {
                confirmLabel.Text = "DEPOSIT SELECTED, CONFIRM SELECTION? : ENTER";
                //Change to depositLabel Panel
                this.navData.setNavigationPanelName("DEPOSIT");
            }
            else if (b.Text == "4")
            {
                confirmLabel.Text = "PIN RESET SELECTED, CONFIRM SELECTION? : ENTER";
                //Change to Pin Reset Panel
                this.navData.setNavigationPanelName("PINRESET");
            }
            else if (b.Text == "5")
            {
                confirmLabel.Text = "PRINT SELECTED, CONFIRM SELECTION? : ENTER";
                //print receipt
                this.navData.setNavigationPanelName("PRINT");
            }
            else if (b.Text == "6")
            {
                confirmLabel.Text = "EXIT SELECTED, CONFIRM SELECTION? : ENTER";
                //exitLabel
                this.navData.setNavigationPanelName("LOGOUT");
            }
        }

        public override void cancel()
        {
            this.navData.setNavigationPanelName("LOGOUT");
            notifyObservers();
        }
        public override void clear()
        {
            confirmLabel.Text = "";
            this.navData.setNavigationPanelName("");
        }
        public override void enter()
        {
            if (this.navData.getNavigationPanelName() != "")
            {
                Debug.WriteLine("Selection : " + navData.getNavigationPanelName());
                notifyObservers();
            }
            else
            {
                confirmLabel.Text = "PLEASE ENTER A SELECTION";
            }
        }
    }
}
