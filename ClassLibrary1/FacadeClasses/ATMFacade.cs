using Helpers.AccountManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.FacadeClasses
{
    public class ATMFacade
    {
        private ATMAccount account;

        public ATMFacade(string accountNumber)
        {
            account = new ATMAccount(accountNumber);
        }

        public void updateAccountPinNumber(string newPin)
        {
            account.updatePin(newPin);
        }

        public string getAccountBalance()
        {
            return account.Balance.ToString();
        }

        public bool validateAccount(string text)
        {
           return account.IsValid(text);

        }

        public void cancelCard(string currentCardNumber)
        {
            CancelCard Canceler = new CancelCard(currentCardNumber);
        }
    }
}
