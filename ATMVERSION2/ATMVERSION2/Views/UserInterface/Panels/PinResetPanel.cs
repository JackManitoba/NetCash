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
   public class PinResetPanel : ATMPanel
    {
        protected static TextBox pinEntryBox;
        protected static Label pinResetLabel;
        protected static Label netCashLabel;
        public PinResetPanel()
        {
            this.name = "PinResetPanel";
            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(109, 57);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(351, 194);
            this.TabIndex = 12;

            pinResetLabel = new Label();
            pinResetLabel.Text = "ENTER NEW PIN";
            pinResetLabel.SetBounds(((this.Width / 2) - 45), (this.Height / 2), 120, 40);
            this.Controls.Add(pinResetLabel);

            pinEntryBox = new System.Windows.Forms.TextBox();
            pinEntryBox.Name = "ENTER PIN";
            pinEntryBox.Text = "";
            pinEntryBox.SetBounds(((this.Width / 2) - 50), ((this.Height / 2) + 40), 100, 40);
            this.Controls.Add(pinEntryBox);

            netCashLabel = new Label();
            netCashLabel.Text = "NET-CASH";
            netCashLabel.SetBounds(((this.Width / 2) - 30), ((this.Height / 2) - 30), 100, 40);
            this.Controls.Add(netCashLabel);
        }

        public override void update(Subject e)
        {
            ATMButton b = (ATMButton)e;
            pinEntryBox.Text += b.Text;
            pinEntryBox.Update();
        }

        public override void cancel()
        {
            this.navData.setNavigationPanelName(0, "LOGOUT");
            notifyObservers();
        }
        public override void clear()
        {
            pinEntryBox.Clear();
            pinEntryBox.Update();
        }
        public override void enter()
        {
            if (pinEntryBox.Text.Length == 4)
            {
                this.navData.setNavigationPanelName(0, "MAIN");
                notifyObservers();
            }
        }
        public override TextBox getInput()
        {
            return pinEntryBox;
        }


    }
}
