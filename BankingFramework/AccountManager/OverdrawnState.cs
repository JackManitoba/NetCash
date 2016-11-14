using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace BankingFramework.AccountManager
{
    public class OverdrawnState : State
    {
        private double InterestRate;

        public OverdrawnState(State State): this(State.Account)
        {
        }

        public OverdrawnState(Account account)
        {
            Account = account;
            Balance = account.Balance;
            InterestRate = GetInterestRate();
        }

        public override void UpdateAmount(double amount)
        {
            Balance += amount;
            ClientRequestDispatcher.TheInstance().DispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteRequest("OverdrawnState class, updateAmount() method", "Attempt to write to Account database"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[Account] Set [Balance]=@b WHERE [AccountNumber] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = Account.AccountNumber;
                cmd.Parameters
                    .Add(new SqlParameter("@b", SqlDbType.Money))
                    .Value = Balance;

                connection.Open();

                cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Dispose();
            }
            StateChangeCheck();
        }

        private double GetInterestRate()
        {
            return 0.1;
        }

        public override void PayInterest(double amount)
        {
            Balance -= amount * InterestRate;      
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