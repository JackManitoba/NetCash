using BankingFramework.DatabaseManagement;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace BankingFramework.AccountManager
{
    public class OverdrawnState : State
    {
        private double _interestRate;

        public OverdrawnState(State State) : this(State.Account) { }

        public OverdrawnState(Account account)
        {
            Account = account;
            Balance = account.GetBalance();
            _interestRate = GetInterestRate();
        }

        public override void UpdateAmount(double amount)
        {
            DatabaseManager.GetInstance().UpdateBalance(Account.GetAccountNumber(), amount);
            StateChangeCheck();
        }

        public double GetInterestRate()
        {
            return 0.1;
        }

        public override void PayInterest(double amount)
        {
            double interest = amount * _interestRate;
            DatabaseManager.GetInstance().UpdateBalance(Account.GetAccountNumber(), -interest);
        }

        public override void StateChangeCheck()
        {
            if (Balance > 0.0)
            {
                Account.State = new OverdrawnState(this);
            }
        }
    }
}