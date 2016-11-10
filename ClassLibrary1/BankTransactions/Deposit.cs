using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.BankTransactions
{
    public class Deposit : Transaction
    {
        private AccountManager.Account DepositAccount;

        private double depositAmount;

        private string description;

        private string cardNumber;

        public Deposit() { }

        public Deposit(string cardNumber, string description, double amount)
        {
            this.cardNumber = cardNumber;
            this.description = description;
            this.depositAmount = amount;
        }

        public int amount()
        {
            return Convert.ToInt32(depositAmount);
        }

        public string type()
        {
            return "DEPOSIT";
        }
        
        public void PerformTransaction()
        {
            DepositAccount.IncreaseBalance(depositAmount);
        }

        public bool AreFundsAvailable()
        {
            return DepositAccount.AreFundsAvailable(depositAmount);
        }
    }
}
