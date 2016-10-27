using System;

namespace NetCash.Models.AccountStates
{
    public abstract class State
    {
        public Account account { get; set; }
        public double Balance { get; set; }

        protected double interest;
        protected double lowerLimit;
        protected double upperLimit;

        protected DateTime LastInterestPaid;

        public abstract void Transfer(double amount);
        public abstract void PayInterest();
        public abstract void UpdateAmount(double amount);
    }
}