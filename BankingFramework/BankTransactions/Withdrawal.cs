using BankingFramework.AccountManager;
using BankingFramework.DatabaseManagement;
using System;

namespace BankingFramework.BankTransactions
{
    public class Withdrawal : Transaction
    {
        private double _withdrawalAmount;
        private string _description;
        private Account _withdrawalAccount;

        public Withdrawal(Account account, string description, double amount)
        {
    
            _description = description;
            _withdrawalAmount = amount;
            _withdrawalAccount = account;
        }

        public double GetAmount()
        {
            return _withdrawalAmount;
        }

        public new string GetType()
        {
            return "WITHDRAWAL";
        }

        public void PerformTransaction()
        {
            _withdrawalAccount.DecreaseBalance(_withdrawalAmount);
            DatabaseManager.GetInstance().AddWithdrawalToDatabase(_withdrawalAccount.GetAccountNumber(), _withdrawalAmount);
        }

        public bool AreFundsAvailable()
        {
            return _withdrawalAccount.AreFundsAvailable(_withdrawalAmount);
        }
    }
}
