using System;
using System.ComponentModel.DataAnnotations;
using BankingFramework.DatabaseManagement;

namespace BankingFramework.AccountManager
{
    public class Account
    {
        public State State;

        private string _accountNumber;
        private double _balance;

        public Account(string accountNumber)
        {
            _accountNumber = accountNumber;        
            _balance = GetBalance();
            State = GetState();
        }

        public void IncreaseBalance(double transferAmount)
        {
            State.UpdateAmount(transferAmount);
        }

        public void DecreaseBalance(double transferAmount)
        {
            State.UpdateAmount(-transferAmount);
            State.PayInterest(transferAmount);
        }

        public State GetState()
        {
            if (_balance >= 0.0) return new BalancedState(this);
            else return new OverdrawnState(this);
        }

        public void UpdateAccountBalance()
        {
            _balance = DatabaseManager.GetInstance().GetAccountBalance(_accountNumber);
        }

        public double GetBalance()
        {
            return DatabaseManager.GetInstance().GetAccountBalance(_accountNumber);
        }

        public bool AreFundsAvailable(double amount)
        {
            double balanceWithOverdraft = GetBalance() + 100;
            if (balanceWithOverdraft >= amount) return true;
            else return false;         
        }

        public string GetAccountNumber()
        {
            return _accountNumber;
        }
   }
}