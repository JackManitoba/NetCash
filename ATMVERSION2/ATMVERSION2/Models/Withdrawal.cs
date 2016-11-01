using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMVERSION2.Models
{
   public class Withdrawal
    {
        private ATMUser account;
        private string description;
        private double amount;
        public Withdrawal(ATMUser account,double amount)
        {
            this.account = account;
            this.amount = amount;
            this.description = "DEFAULT DESCRIPTION";
        }

        internal void withdrawAmountFromAccount()
        {
            if(account.getBalance() >=amount)
            account.setBalance(-amount) ;
        }
    }
}
