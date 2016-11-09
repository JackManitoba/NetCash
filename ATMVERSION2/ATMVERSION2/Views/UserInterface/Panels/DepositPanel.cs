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
   public class DepositPanel : ATMPanel
    {
        protected static TextBox amountEntryBox;
        protected static Label depositLabel;
        protected static Label netCashLabel;
        protected static Label messageLabel;
        public DepositPanel()
        {
            this.name = "DepositPanel";
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(109, 57);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(351, 194);
            this.TabIndex = 12;

            depositLabel = new Label();
            depositLabel.Text = "DEPOSIT";
            depositLabel.SetBounds(((this.Width / 2) - 25), (this.Height / 2), 100, 30);
            this.Controls.Add(depositLabel);

            amountEntryBox = new System.Windows.Forms.TextBox();
            amountEntryBox.Name = "ENTER AMOUNT";
            amountEntryBox.ReadOnly = true;
            amountEntryBox.Text = "";
            amountEntryBox.SetBounds(((this.Width / 2) - 50), (this.Height / 2 + 30), 100, 40);
            this.Controls.Add(amountEntryBox);

            netCashLabel = new Label();
            netCashLabel.Text = "NET-CASH";
            netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            this.Controls.Add(netCashLabel);

            messageLabel = new Label();
            messageLabel.Text = "";
            messageLabel.ForeColor = System.Drawing.Color.Red;
            messageLabel.SetBounds(((this.Width / 2) - 70), ((this.Height / 2) - 70), 150, 40);
            this.Controls.Add(messageLabel);
        }

        public override void update(Subject e)
        {
            ATMButton b = (ATMButton)e;
            amountEntryBox.Text += b.Text;
            amountEntryBox.Update();
            messageLabel.Text = "";
            messageLabel.Update();
        }

        public override void cancel()
        {
            this.navData.setNavigationPanelName("LOGOUT");
            notifyObservers();
        }
        public override void clear()
        {
            amountEntryBox.Clear();
            amountEntryBox.Update();
        }
        public override void enter()
        {
            if (amountEntryBox.Text == "")
            {
                //do nothing
            }
            else
            {
                if (((Convert.ToInt32(amountEntryBox.Text)) % 10) != 0)
                {
                    messageLabel.Text = "THIS MACHINE DOES NOT ACCEPT ANY CURRECY LESS THAN €10";
                    messageLabel.Update();
                }
                else
                {
                    this.navData.setNavigationPanelName("MAIN");
                    notifyObservers();
                }
            }                
        }
        public override TextBox getInput()
        {
            return amountEntryBox;
        }
    }
}

