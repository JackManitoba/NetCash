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
        private double _interestRate;

        public BalancedState(string accountNumber)
        {
            _accountNumber = accountNumber;
            _interestRate = GetInterestRate();
        }

        public override void UpdateAmount(double _amount)
        {
            DatabaseManager.GetInstance().UpdateBalance(_accountNumber, _amount);
        }

        private double GetInterestRate()
        {
            return 0.0;
        }

        public override void PayInterest(double amount)
        {
            //No interest paid
        }
    }
}