using Helpers.Interceptor_Package;
using Helpers.Interceptor_Package.Dispatchers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Helpers.AccountManager
{
    public class OverdrawnState : State
    {
        private double InterestRate;

        public OverdrawnState(State state): this(state.account)
        {
        }

        public OverdrawnState(Account account)
        {
            this.account = account;
            this.Balance = account.Balance;
            this.InterestRate = GetInterestRate();
        }

        public override void UpdateAmount(double _amount)
        {
            Balance += _amount;
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteRequest("OverdrawnState class, updateAmount() method", "Attempt to write to Account database"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[Account] Set [Balance]=@b WHERE [AccountNumber] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = account.AccountNumber;
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
            return 2.00;
        }

        public override void PayInterest()
        {
            throw new NotImplementedException();
        }

        public override void StateChangeCheck()
        {
            if (Balance > 0.0)
            {
                account.state = new OverdrawnState(this);
            }
        }
    }
}