using BankingFramework.AccountManager;
using BankingFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingFramework.BankTransactions
{
    public class Deposit : Transaction
    {
        private Account _depositAccount;

        private double _depositAmount;

        private string _description;

        private string _cardNumber;

        public Deposit() { }

        public Deposit(Account account, string description, double amount)
        {     
            _description = description;
            _depositAmount = amount;
            _depositAccount = account;
        }

        public int GetAmount()
        {
            return Convert.ToInt32(_depositAmount);
        }

        public string GetType()
        {
            return "DEPOSIT";
        }
        
        public void PerformTransaction()
        {
            _depositAccount.IncreaseBalance(_depositAmount);
            DatabaseManager.GetInstance().AddTransactionToDatabase(this);
        }

        public bool AreFundsAvailable()
        {
            return _depositAccount.AreFundsAvailable(_depositAmount);
        }

        public string SourceAccount()
        {
            return "";
        }

        public string TargetAccount()
        {
            return this._depositAccount.AccountNumber;
        }
    }
}
