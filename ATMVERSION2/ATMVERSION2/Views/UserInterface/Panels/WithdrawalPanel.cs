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
   public class WithdrawalPanel : ATMPanel
    {
        protected static TextBox amountEntryBox;
        protected static Label withdrawalLabel;
        protected static Label netCashLabel;
        public WithdrawalPanel()
        {
            this.name = "WithdrawalPanel";
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(109, 57);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(351, 194);
            this.TabIndex = 12;

            withdrawalLabel = new Label();
            withdrawalLabel.Text = "WITHDRAWAL";
            withdrawalLabel.SetBounds(((this.Width / 2) - 40), (this.Height / 2), 100, 30);
            this.Controls.Add(withdrawalLabel);

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
        }

        public override void update(Subject e)
        {
            ATMButton b = (ATMButton)e;
            amountEntryBox.Text += b.Text;
            amountEntryBox.Update();
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
            this.navData.setNavigationPanelName("LOGOUT");
               
            notifyObservers();
        }
        public override TextBox getInput()
        {
            return amountEntryBox;
        }
    }
}
