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

        public Withdrawal(string cardNumber, string description, double amount1)
        {
            this.cardNumber = cardNumber;
            this.description = description;
            this.withdrawalAmount = amount1;
        }

        public int amount()
        {
            return Convert.ToInt32(withdrawalAmount);
        }

        public string type()
        {
            return "WITHDRAWAL";
        }
    }
}
