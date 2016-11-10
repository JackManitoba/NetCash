using Helpers.AccountManager;
using Helpers.BankTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.FacadeClasses
{
    public class OnlineAppFacade
    {
        Account Account;

        public OnlineAppFacade(string AccountNumber)
        {
            Account = new Account(AccountNumber);
        }

        public bool areFundsAvailable(double transferAmount)
        {
            return Account.AreFundsAvailable(transferAmount);
        }

        public void PerformTransaction(string TargetAccountNumber, double TransferAmount)
        {
            Transaction Transfer = new Transfer(Account.AccountNumber, TargetAccountNumber, "TRANSFER", TransferAmount);
            Transfer.PerformTransaction();
        }
    }
}
