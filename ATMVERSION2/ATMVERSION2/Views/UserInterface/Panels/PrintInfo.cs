using ATMVERSION2.UserInterface.Panels;
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

namespace ATMVERSION2.Views.UserInterface.Panels
{
   public class PrintInfo :  ATMPanel
    {
        Label message;
        string filename;

        public PrintInfo()
        {

            this.BackColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(109, 57);
           
            this.Size = new System.Drawing.Size(351, 194);
            this.name = "PrintInfo";

           message = new Label();
            message.Text = "Printing";
            message.SetBounds(((this.Width / 2) - 50), (this.Height / 2 + 10), 150, 40);
            this.Controls.Add(message);
            
           

           

        }

       

        public override void enter()
        {
            this.navData.setNavigationPanelName("MAIN");

            notifyObservers();
        }

        internal void setFileName(string accountNumber)
        {
   
            this.filename = @"TransactionsLog" + accountNumber.Trim() + ".txt";
            Process.Start("notepad.exe", this.filename);
            message.Text = "Printing Complete, press enter to continue";
        }
    }

        
    }

