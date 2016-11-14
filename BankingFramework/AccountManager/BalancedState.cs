using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankingFramework.InterceptorPackage.Dispatchers;
using BankingFramework.InterceptorPackage.ContextObjects;

namespace BankingFramework.AccountManager
{
    public class BalancedState : State
    {
        private double InterestRate;

        public BalancedState(State state): this(state.Account)
        {
          
        }

        public BalancedState(Account account)
        { 
            this.Account = account;
            this.Balance = account.Balance;
            this.InterestRate = GetInterestRate();
        }

        public override void UpdateAmount(double _amount)
        {
            ClientRequestDispatcher.TheInstance().DispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteRequest("BalancedState Class, updateAmount() method", "Attempt to write to Account database"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[Account] Set [Balance]=@b WHERE [AccountNumber] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = Account.AccountNumber;
                cmd.Parameters
                    .Add(new SqlParameter("@b", SqlDbType.Money))
                    .Value = this.Account.Balance + _amount;

                connection.Open();

                cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Dispose();
               
            }

            StateChangeCheck();
        }

        private double GetInterestRate()
        {
            return 2.00;
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