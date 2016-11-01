using ATMVERSION2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMVERSION2.UserInterface.Panels;
using ATMVERSION2.Views;
using ATMVERSION2.Models;

namespace ATMVERSION2.Controllers
{
    public class PinVerificationController : Controller
    {
        Model model;
        View view;
        public PinVerificationController(Model m, View v)
        {
            model = m;
            view = v;
        }

        public void performDeposit(Deposit deposit)
        {
            deposit.depositAmountToAccount();
        }

        public void performWithdrawal(Withdrawal withdrawal)
        {
            withdrawal.withdrawAmountFromAccount();
        }

        public void resetAccountPin(string newPin)
        {
           //
        }

        public void setPanel(ATMPanel currentPanel)
        {
            ATMMainView view = (ATMMainView)this.view;
            view.setCurrentPanel(currentPanel);
        }
    }
}
