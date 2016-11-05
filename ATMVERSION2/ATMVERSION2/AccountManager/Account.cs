using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using WindowsFormsApplication1.Interceptor_Package.Dispatchers;
using WindowsFormsApplication1.Interceptor_Package;


namespace ATMVERSION2.AccountManager
{
    public class Account
    {
        public State state;

        public string EncodingType;

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "PIN")]
        public string pin { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        public string cardNumber { get; set; }

        [Required]
        [Display(Name = "Balance")]
        public double Balance { get; set; }

        public Account(string _accountnumber)
        {
            this.AccountNumber = _accountnumber;
            this.Balance = GetBalance();
            this.state = GetState();
            this.EncodingType = GetEncodingType();
            this.pin = GetPin();
            Debug.WriteLine("Account constructor finished");
            Debug.WriteLine("Account number: "+ AccountNumber);
            Debug.WriteLine("Account balance: " + Balance);
            Debug.WriteLine("Account state: "+ state.ToString());
            Debug.WriteLine("Account pin: "+ pin);
        }



        public static string getAccountByCardNumber(string _cardNumber)
        {
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("Account Class","Attempt to retrieve Account number from Account database"));
            Debug.WriteLine("getAccountByCardNumber called, card number was: " + _cardNumber);
            string accountNo;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [AccountNumber] From [dbo].[ATMUsers] WHERE [CardNumber] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = _cardNumber;

                connection.Open();

                accountNo = Convert.ToString(cmd.ExecuteScalar());

                cmd.Dispose();
                connection.Dispose();
            }
            Debug.WriteLine("Account number returned was: " + accountNo);
            return accountNo;
        }

        public void updatePin(string newPin)
        {
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteRequest("Account Class", "Attempt to update user pin from ATMUsers database"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[AtmUser] Set [PIN]=@b WHERE [AccountNumber] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = this.AccountNumber;
                cmd.Parameters
                    .Add(new SqlParameter("@b", SqlDbType.Money))
                    .Value = newPin;
                this.pin = newPin;
                connection.Open();

                cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Dispose();
            }
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
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("Account Class", "Attempt to retrieve balance from Account database"));
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


        public bool IsValid(string s)
        {
            string currentPin = GetPin();
            if (s == currentPin)
                return true;
            else
                return false;
        }


        private string GetPin()
        {
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("Account Class", "Attempt to retrieve pin from ATMUser database"));
            string pin;

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