using BankingFramework.AccountManager;
using BankingFramework.DatabaseManagement;
using System;

namespace BankingFramework.BankTransactions
{
    public class Deposit : Transaction
    {
        private Account _depositAccount;
        private double _depositAmount;
        private string _description;

        public Deposit(Account account, string description, double amount)
        {
            _description = description;
            _depositAmount = amount;
            _depositAccount = account;
        }

        public double GetAmount()
        {
            return _depositAmount;
        }

        public new string GetType()
        {
            return "DEPOSIT";
        }

        public void PerformTransaction()
        {
            _depositAccount.IncreaseBalance(_depositAmount);
            DatabaseManager.GetInstance().AddDepositToDatabase(_depositAccount.GetAccountNumber(), _depositAmount);
        }

        public bool AreFundsAvailable()
        {
            return _depositAccount.AreFundsAvailable(_depositAmount);
        }
    }
}
