using Helpers.AccountManager;
using Helpers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.BankTransactions
{
    public class Withdrawal : Transaction
    {
        private double withdrawalAmount;
        private string cardNumber;
        private string description;
        private AccountManager.Account WithdrawalAccount;

        public Withdrawal(Account account, string description, double amount1)
        {
    
            this.description = description;
            this.withdrawalAmount = amount1;
            string accountNumber = account.AccountNumber;
            this.WithdrawalAccount = account;
        }

        public int amount()
        {
            return Convert.ToInt32(withdrawalAmount);
        }

        public string type()
        {
            return "WITHDRAWAL";
        }

        public void PerformTransaction()
        {
            WithdrawalAccount.DecreaseBalance(withdrawalAmount);
            DatabaseManager.getInstance().addTransactionToDatabase(this);
        }

        public bool AreFundsAvailable()
        {
            return WithdrawalAccount.AreFundsAvailable(withdrawalAmount);
        }
        public string sourceAccount()
        {
            return this.WithdrawalAccount.AccountNumber;
        }

        public string targetAccount()
        {
            return "";
        }
    }
}
