using BankingFramework.AccountManager;
using BankingFramework.Utils;
using System;

namespace BankingFramework.BankTransactions
{
    public class Withdrawal : Transaction
    {
        private double _withdrawalAmount;
        private string _cardNumber;
        private string _description;
        private Account _withdrawalAccount;

        public Withdrawal(Account account, string description, double amount)
        {
    
            _description = description;
            _withdrawalAmount = amount;
            string accountNumber = account.AccountNumber;
            _withdrawalAccount = account;
        }

        public int GetAmount()
        {
            return Convert.ToInt32(_withdrawalAmount);
        }

        public string GetType()
        {
            return "WITHDRAWAL";
        }

        public void PerformTransaction()
        {
            _withdrawalAccount.DecreaseBalance(_withdrawalAmount);
            DatabaseManager.GetInstance().AddTransactionToDatabase(this);
        }

        public bool AreFundsAvailable()
        {
            return _withdrawalAccount.AreFundsAvailable(_withdrawalAmount);
        }
        public string SourceAccount()
        {
            return this._withdrawalAccount.AccountNumber;
        }

        public string TargetAccount()
        {
            return "";
        }
    }
}
