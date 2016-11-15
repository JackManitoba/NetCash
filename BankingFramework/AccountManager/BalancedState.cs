using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankingFramework.InterceptorPackage.Dispatchers;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.DatabaseManagement;

namespace BankingFramework.AccountManager
{
    public class BalancedState : State
    {
        private double InterestRate;

        public BalancedState(State state): this(state.Account) {}

        public BalancedState(Account account)
        { 
            this.Account = account;
            this.Balance = account.GetBalance();
            this.InterestRate = GetInterestRate();
        }

        public override void UpdateAmount(double _amount)
        {
            DatabaseManager.GetInstance().UpdateBalance(Account.GetAccountNumber(), _amount);
            StateChangeCheck();
        }

        private double GetInterestRate()
        {
            return 0.0;
        }

        public override void PayInterest(double amount)
        {
            //No interest paid
        }

        public override void StateChangeCheck()
        {
            if(Balance < 0.0)
            {
                Account.State = new OverdrawnState(this);
            }
        }
    }
}