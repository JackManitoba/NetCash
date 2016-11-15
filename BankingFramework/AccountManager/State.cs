using System;

namespace BankingFramework.AccountManager
{
    public abstract class State
    {
        public string _accountNumber;
        public double _balance;
        public double _interest;
        
        public abstract void PayInterest(double amount);
        public abstract void UpdateAmount(double amount);
    }
}