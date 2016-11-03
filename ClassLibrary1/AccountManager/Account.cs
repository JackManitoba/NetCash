using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Helpers.AccountManager
{
    public class Account
    {
        public State state;

        public string EncodingType;

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
            this.EncodingType = GetEncodingType();
        }

        private string GetEncodingType()
        {
            return "SHA1";
        }

        private State GetState()
        {
            if (Balance >= 0.0) return new BalancedState(this);
            else return new OverdrawnState(this);
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
                if (GetBalance() + 100 >= Balance) return true;
                else return false;         
        }
    }
}