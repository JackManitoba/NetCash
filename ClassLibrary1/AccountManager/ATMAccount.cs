using Helpers.Interceptor_Package;
using Helpers.Interceptor_Package.Dispatchers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.AccountManager
{
    public class ATMAccount : Account
    {
        [Required]
        [Display(Name = "PIN")]
        public string pin { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        public string cardNumber { get; set; }

        public ATMAccount(string _accountnumber) : base(_accountnumber)
        {
            this.pin = GetPin();
            Debug.WriteLine("Account pin: " + pin);
        }

        private string GetPin()
        {

            string pin;
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("Account class, getPin() method", "Attempt to read ATMUsers database"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [PIN] From [dbo].[ATMUsers] WHERE [AccountNumber] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = AccountNumber;

                connection.Open();

                pin = Convert.ToString(cmd.ExecuteScalar());

                cmd.Dispose();
                connection.Dispose();
            }
            return pin;
        }

        public bool IsValid(string s)
        {
            string currentPin = GetPin();
            if (s == currentPin)
                return true;
            else
                return false;
        }

        public void updatePin(string newPin)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[ATMUsers] Set [PIN]=@b WHERE [AccountNumber] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = this.AccountNumber;
                cmd.Parameters
                    .Add(new SqlParameter("@b", SqlDbType.NVarChar))
                    .Value = newPin;
                this.pin = newPin;
                connection.Open();

                cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Dispose();
            }
        }

        public static string getAccountByCardNumber(string _cardNumber)
        {
            Debug.WriteLine("getAccountByCardNumber called, card number was: " + _cardNumber);
            string accountNo;
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("Account class, getAccountByCardNumber() method", "Attempt to read ATMUsers database"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [AccountNumber] From [dbo].[ATMUsers] WHERE [CardNumber] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = _cardNumber.Trim();

                connection.Open();

                accountNo = Convert.ToString(cmd.ExecuteScalar());

                cmd.Dispose();
                connection.Dispose();
            }
            Debug.WriteLine("Account number returned was: " + accountNo);
            return accountNo;
        }
    }
}
