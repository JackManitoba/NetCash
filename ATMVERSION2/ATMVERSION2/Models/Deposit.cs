using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.Models
{
    public class Deposit
    {
        private ATMUser account;
        private string description;
        private double amount;
        public Deposit(ATMUser account, double amount)
        {
            this.account = account;
            this.amount = amount;
            this.description = "DEFAULT DESCRIPTION";
        }

        internal void depositAmountToAccount()
        {           
                account.setBalance(amount);
        }
    }
}
