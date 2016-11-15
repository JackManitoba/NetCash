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
            StateChangeCheck();
        }

        public void DecreaseBalance(double transferAmount)
        {
            State.UpdateAmount(-transferAmount);
            State.PayInterest(transferAmount);
            StateChangeCheck();
        }

        public State GetState()
        {
            if (_balance >= 0.0) return new BalancedState(_accountNumber);
            else return new OverdrawnState(_accountNumber);
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

        public void StateChangeCheck()
        {
            if (_balance > 0.0)
            {
                State = new OverdrawnState(_accountNumber);
            }
        }
    }
}