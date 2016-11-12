using NetCashATM.UserInterface.Panels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCashATM.Views.UserInterface.Panels
{
    public class PrintInfo : ATMPanel
    {
        public Label Message;
        public string Filename;

        public PrintInfo()
        {

            BackColor = System.Drawing.Color.White;
            Location = new System.Drawing.Point(109, 57);

            Size = new System.Drawing.Size(351, 194);
            Name = "PrintInfo";

            Message = new Label();
            Message.Text = "Printing";
            Message.SetBounds(((Width / 2) - 50), (Height / 2 + 10), 150, 40);
            Controls.Add(Message);
        }

        public override void Enter()
        {
            NavData.SetNavigationPanelName("MAIN");

            NotifyObservers();
        }

        public void SetFileName(string accountNumber)
        {
            var path = (AppDomain.CurrentDomain.BaseDirectory);
            int position = path.IndexOf("NetCash");
            var substring = path.Substring(0, position);
            path = substring + "NetCash\\logs\\TransactionsLog" + accountNumber.Trim() + ".txt";

            Filename = @path;
            Process.Start("notepad.exe", Filename);
            Message.Text = "Printing Complete, press enter to continue";
        }

    }
}

