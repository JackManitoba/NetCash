using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WindowsFormsApplication1.Interceptor_Package;
using WindowsFormsApplication1.Interceptor_Package.Dispatchers;

namespace ATMVERSION2.AccountManager
{
    public class BalancedState : State
    {
        private double InterestRate;

        public BalancedState(State state): this(state.account)
        {
        }

        public BalancedState(Account account)
        { 
            this.account = account;
            this.Balance = account.Balance;
            this.InterestRate = GetInterestRate();
        }

        public override void UpdateAmount(double _amount)
        {
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteRequest("BalancedState Class, updateAmount() method", "Attempt to write to Account database"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[Account] Set [Balance]=@b WHERE [AccountNumber] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = account.AccountNumber;
                cmd.Parameters
                    .Add(new SqlParameter("@b", SqlDbType.Money))
                    .Value = this.account.Balance + _amount;

                connection.Open();

                cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Dispose();
               
            }
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
            if(Balance < 0.0)
            {
                account.state = new OverdrawnState(this);
            }
        }
    }
}