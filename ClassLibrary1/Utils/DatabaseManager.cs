using Helpers.Interceptor_Package;
using Helpers.Interceptor_Package.Dispatchers;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Helpers.BankTransactions;

namespace Helpers.Utils
{
  public  class DatabaseManager
    {

        private static DatabaseManager instance;

        public static DatabaseManager getInstance()
        {
            if (instance == null)
            {
                instance = new DatabaseManager();
                return instance;
            }
            else return instance;
        }
       private DatabaseManager() { }
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public int retrieveDenominationAmounts(string current)
        {
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteRequest("DatabaseManager, retrieveDenominationAmounts()", "Attempt to read ATMCash database"));
            int returnValue;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [Amount] From [dbo].[ATMCash] WHERE [Note] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = current;

                connection.Open();
                returnValue = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                Debug.WriteLine(current + " : " + returnValue);
                cmd.Dispose();
                connection.Dispose();
            }
            return returnValue;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void updateATMCashAmount(string note, int amount)
        {
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteRequest("DatabaseManager, updateATMPinNumber()", "Attempt to read ATMUsers database"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[ATMCash] Set [Amount]=@b WHERE [Note] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = note;
                cmd.Parameters
                   .Add(new SqlParameter("@b", SqlDbType.Int))
                   .Value = amount;
                connection.Open();
                Debug.WriteLine("+++note=" + note + "  amount = " + amount);

                cmd.ExecuteScalar();
                cmd.Dispose();
                connection.Dispose();
            }
        }

        internal void addTransactionToDatabase(Transaction t)
        {
            Debug.WriteLine(t.sourceAccount()+ "lol");
            Debug.WriteLine(t.targetAccount());
           
            var dateAndTime = DateTime.Now;
            
            Debug.WriteLine(dateAndTime.ToString("dd/MM/yyyy"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"INSERT INTO [dbo].[BankTransactions] VALUES (@origAccount,@targetAccount,@type,@amount,@date) ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@origAccount", SqlDbType.NVarChar))
                    .Value = t.sourceAccount();
                cmd.Parameters
                    .Add(new SqlParameter("@targetAccount", SqlDbType.NVarChar))
                    .Value = t.targetAccount();
                cmd.Parameters
                     .Add(new SqlParameter("@type", SqlDbType.NVarChar))
                     .Value = t.type();
                cmd.Parameters
                    .Add(new SqlParameter("@amount", SqlDbType.Money))
                    .Value = Convert.ToInt32(t.amount());
                cmd.Parameters
                    .Add(new SqlParameter("@date", SqlDbType.NVarChar))
                    .Value = dateAndTime.ToString("dd/MM/yyyy");

                connection.Open();

                cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Dispose();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void updateATMPinNumber(string accountNumber, string newPin)
        {
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteRequest("DatabaseManager, updateATMPinNumber()","Attempt to read ATMUsers database"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[Users] Set [PIN]=@b WHERE [AccountNumber] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = accountNumber;
                cmd.Parameters
                    .Add(new SqlParameter("@b", SqlDbType.NVarChar))
                    .Value = newPin;

                connection.Open();

                cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Dispose();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static string getAccountByATMCardNumber(string _cardNumber)
        {
           
            string accountNo;
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("DatabaseManager, getAccountByATMCardNumber() method", "Attempt to read ATMUsers database"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [AccountNumber] From [dbo].[Users] WHERE [CardNumber] = @a ";

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

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public double GetAccountBalance(string AccountNumber)
        {
            double balance;
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("DatabaseManager, getAccountBalance() method", "Attempt to read Account database"));
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

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public string GetATMAccountPin(string AccountNumber)
        {

            string pin;
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("DatabaseManager, getATMAccountPin() method", "Attempt to read ATMUsers database"));
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [PIN] From [dbo].[Users] WHERE [AccountNumber] = @a ";

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

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}
