using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using Helpers.Interceptor_Package.Dispatchers;
using Helpers.Interceptor_Package;


namespace Helpers.AccountManager
{
    public class Account
    {
        public State state;

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Balance")]
        public double Balance { get; set; }

        public Account(string _accountnumber)
        {
            this.AccountNumber = _accountnumber;
            this.Balance = GetBalance();
            this.state = GetState();
          
            Debug.WriteLine("Account constructor finished");
            Debug.WriteLine("Account number: "+ AccountNumber);
            Debug.WriteLine("Account balance: " + Balance);
            Debug.WriteLine("Account state: "+ state.ToString());
        }

        public void IncreaseBalance(double transferAmount)
        {
            UpdateAmount(transferAmount);
            
        }

        public void DecreaseBalance(double transferAmount)
        {
            UpdateAmount(-transferAmount);
            state.PayInterest(transferAmount);
        }

        private State GetState()
        {
            if (Balance >= 0.0) return new BalancedState(this);
            else return new OverdrawnState(this);
        }

        private double GetBalance()
        {
            double balance;
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("Account class, getBalance() method", "Attempt to read Account database"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [Balance] From [dbo].[Account] WHERE [AccountNumber] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = AccountNumber;

                connection.Open();

                balance = Convert.ToDouble(cmd.ExecuteScalar());

                cmd.Dispose();
                connection.Dispose();
            }
            return balance;
        }

        public void UpdateAmount(double _amount)
        {          
            state.UpdateAmount(_amount);
           this.Balance =  GetBalance();     
        }

        public bool AreFundsAvailable(double Balance)
        {
                if (GetBalance() + 100 >= Balance) return true;
                else return false;         
        }
        public bool AreFullFundsAvailable(double WithdrawAttempt)
        {
            if (GetBalance() >= WithdrawAttempt)
                return true;
            else
                return false;
        }
    }
}