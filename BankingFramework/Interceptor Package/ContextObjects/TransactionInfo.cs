using BankingFramework.AccountManager;
using System;

namespace BankingFramework.Interceptor_Package
{
   public  class TransactionInfo : ContextObject
    {
        private Account _account;
        private string _description;
        private int _transactionAmount;

        public TransactionInfo(Account account, string description, int amount)
        {
            _account = account;
            _description = description;
            _transactionAmount = amount;
        }

        public string GetAccountNumber()
        {
            return _account.AccountNumber;
        }

        public string GetAccountBalance()
        {
            return _account.Balance.ToString();
        }

        public string GetDescription()
        {
            return _description;
        }

        public string GetAmount()
        {
            return _transactionAmount.ToString();
        }

        public string GetObj()
        {
            return "TransactionInfo: ";
        }

        public string GetShortDescription()
        {
            return _description;
        }

        public string GetVerboseDescription()
        {
            return "Account: " + GetAccountNumber() + " Description: " + _description + " Amount: € " + GetAmount() + " " + DateTime.Now;
        }
    }
}
