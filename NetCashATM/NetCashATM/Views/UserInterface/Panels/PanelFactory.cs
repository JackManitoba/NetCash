﻿using NetCashATM.Views.UserInterface.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCashATM.UserInterface.Panels
{
  public class PanelFactory
    {
        public ATMPanel GetPanel(string identifier)
        {
            ATMPanel p = null;
            switch (identifier)
            {
                case "BalancePanel":
                    {
                        p = new BalancePanel();
                        break;
                    }
                case "DepositPanel":
                    {
                        p = new DepositPanel();
                        break;
                    }
                case "MainPanel":
                    {
                        p = new MainPanel();
                        break;
                    }
                case "PRINT":
                    {
                        p = new PrintInfo();
                        break;
                    }
                case "PIN":
                    {
                        p = new PinPanel();
                        break;
                    }
                case "PINRESET":
                    {
                        p = new PinResetPanel();
                        break;
                    }
                case "WITHDRAWAL":
                    {
                        p = new WithdrawalPanel();
                        break;
                    }
                case "LOGOUT":
                    {
                        p = new LogoutPanel();
                        break;
                    }
            }
            return p;
        }
    }
}
