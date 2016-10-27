using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace NetCash.Models.AccountStates
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
        }

        private State GetState()
        {
            return new BalancedState(this);
            //read from database
        }

        private double GetBalance()
        {
            double balance;

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
        }

        public bool AreFundsAvailable(double Balance)
        {
                if (GetBalance() >= Balance) return true;
                else return false;

            
        }
    }
}