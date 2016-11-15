using BankingFramework.DatabaseManagement;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace BankingFramework.AccountManager
{
    public class OverdrawnState : State
    {
        private double _interestRate;

        public OverdrawnState(string accountNumber)
        {
            _accountNumber = accountNumber;
            _interestRate = GetInterestRate();
        }

        public override void UpdateAmount(double amount)
        {
            DatabaseManager.GetInstance().UpdateBalance(_accountNumber, amount);

        }

        public double GetInterestRate()
        {
            return 0.1;
        }

        public override void PayInterest(double amount)
        {
            double interest = amount * _interestRate;
            DatabaseManager.GetInstance().UpdateBalance(_accountNumber, -interest);
        }
    }
}