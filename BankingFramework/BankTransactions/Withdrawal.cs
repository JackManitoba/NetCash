using BankingFramework.AccountManager;
using System;

namespace BankingFramework.BankTransactions
{
    public class Withdrawal : Transaction
    {
        private double _withdrawalAmount;
        private string _cardNumber;
        private string _description;
        Account WithdrawalAccount;

        public Withdrawal(Account account, string description, double amount)
        {
    
            _description = description;
            _withdrawalAmount = amount;
            string accountNumber = account.AccountNumber;
            WithdrawalAccount = account;
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
            WithdrawalAccount.DecreaseBalance(_withdrawalAmount);
        }

        public bool AreFundsAvailable()
        {
            return WithdrawalAccount.AreFundsAvailable(_withdrawalAmount);
        }
    }
}
