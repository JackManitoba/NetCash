using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
            //this is probably not a good way to do this, but it seems to work

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var path = baseDir.Replace("\\ATMVERSION2\\WindowsFormsApplication1\\bin\\Debug", "");
            path += "\\WebApplication5\\App_Data";
            var fullPath = Path.GetFullPath(path);
            Debug.WriteLine("Connection String for ATM = " + fullPath);
            AppDomain.CurrentDomain.SetData("DataDirectory", fullPath);

            //////


            Debug.WriteLine("getAccountByCardNumber called, card number was: " + _cardNumber);
            string accountNo;
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("Account class, getAccountByCardNumber() method","Attempt to read ATMUsers database"));
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
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[AtmUsers] Set [PIN]=@b WHERE [AccountNumber] = @a ";

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
    }
}