using System;

namespace BankingFramework.AccountManager
{
    public abstract class State
    {
        public Account Account { get; set; }
        public double Balance { get; set; }

        protected double Interest;

        protected DateTime LastInterestPaid;
        
        public abstract void PayInterest(double amount);
        public abstract void UpdateAmount(double amount);
        public abstract void StateChangeCheck();
    }
}