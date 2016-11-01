using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.UserInterface.Panels
{

  public class PanelFactory
    {
        public ATMPanel getPanel(string identifier)
        {
            ATMPanel p = null;
            switch (identifier)
            {
                case "BALANCE":
                    {
                        p = new BalancePanel();
                        break;
                    }
                case "DEPOSIT":
                    {
                        p = new DepositPanel();
                        break;
                    }
                case "MAIN":
                    {
                        p = new MainPanel();
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
