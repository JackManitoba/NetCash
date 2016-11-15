using BankingFramework.AccountManager;
using BankingFramework.DatabaseManagement;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using System;

namespace BankingFramework.BankTransactions
{
    public class Transfer : Transaction
    {
        private  Account _incomingTransferAccount;
        private  Account _outgoingTransferAccount;

        private double _transferAmount;

        public Transfer(string accountNumber, string targetAccountNumber, string description, double amount)
        {
            _transferAmount = amount;

            _incomingTransferAccount = new Account(targetAccountNumber);
            _outgoingTransferAccount = new Account(accountNumber);
        }

        public new string GetType()
        {
            return "Transfer";
        }

        public double GetAmount()
        {
            return _transferAmount;
        }

        public void PerformTransaction()
        {
            _incomingTransferAccount.IncreaseBalance(_transferAmount);

            _outgoingTransferAccount.DecreaseBalance(_transferAmount);
            DatabaseManager.GetInstance().AddTransferToDatabase(_outgoingTransferAccount.GetAccountNumber(), _incomingTransferAccount.GetAccountNumber(), GetType(), _transferAmount);
        }

        public bool AreFundsAvailable()
        {
            return _outgoingTransferAccount.AreFundsAvailable(_transferAmount);
        }
    }
}
