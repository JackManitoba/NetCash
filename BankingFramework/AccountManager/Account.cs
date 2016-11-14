using System;
using System.ComponentModel.DataAnnotations;
using BankingFramework.DatabaseManagement;

namespace BankingFramework.AccountManager
{
    public class Account
    {
        public State State;

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        public double Balance { get; set; }

        public Account(string accountnumber)
        {
            AccountNumber = accountnumber;        
            Balance = GetBalance();
            State = GetState();
        }

        public void IncreaseBalance(double transferAmount)
        {
            State.UpdateAmount(Convert.ToInt32(transferAmount));
        }

        public void DecreaseBalance(double transferAmount)
        {
            State.UpdateAmount(-Convert.ToInt32(transferAmount));
            State.PayInterest(transferAmount);
        }

        private State GetState()
        {
            if (Balance >= 0.0) return new BalancedState(this);
            else return new OverdrawnState(this);
        }

        public void UpdateAccountBalance()
        {
            Balance = DatabaseManager.GetInstance().GetAccountBalance(AccountNumber);
        }

        private double GetBalance()
        {
            DatabaseManager.GetInstance();
            return DatabaseManager.GetInstance().GetAccountBalance(AccountNumber);
        }

     /*   public void UpdateAmount(Transaction t)
        {
            State.UpdateAmount(t.GetAmount());
            //DatabaseManager.GetInstance().AddTransactionToDatabase(t);
            Balance =  GetBalance();     
        }

        public void UpdateAmount(int amount)
        {
            State.UpdateAmount(amount);
            Balance = GetBalance();     
        }
        */

        public bool AreFundsAvailable(double amount)
        {
            double balanceWithOverdraft = GetBalance() + 100;
            if (balanceWithOverdraft >= amount) return true;
            else return false;         
        }
   }
}