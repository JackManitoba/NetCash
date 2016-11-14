using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using BankingFramework.InterceptorPackage.Dispatchers;
using BankingFramework.InterceptorPackage.ContextObjects;
using System.Collections.Generic;

namespace BankingFramework.DatabaseManagement
{
  public class DatabaseManager
    {
        private static DatabaseManager _instance;

        public static DatabaseManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DatabaseManager();
                return _instance;
            }
            else return _instance;
        }

        private DatabaseManager() { }


        public double RetrieveDenominationAmounts(string current)
        {
            double returnValue;

            ClientRequestDispatcher.TheInstance()
                                   .DispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteRequest("DatabaseManager, retrieveDenominationAmounts()", "Attempt to read ATMCash database"));

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string sql = @"SELECT [Amount] From [dbo].[ATMCash] WHERE [Note] = @a ";
                var cmd = new SqlCommand(sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = current;

                connection.Open();
                returnValue = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                cmd.Dispose();
                connection.Dispose();
            }
            return returnValue;
        }

        public bool IsCardCancelled(string cardNumber)
        {
            bool Cancelled = true;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [Cancelled] From [dbo].[Users] WHERE [CardNumber] = @cn ";
                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@cn", SqlDbType.NVarChar))
                    .Value = cardNumber;

                connection.Open();
                Cancelled = Convert.ToBoolean(cmd.ExecuteScalar());
                cmd.Dispose();
                connection.Dispose();
            }

            return Cancelled;

        }

        public void UpdateATMCashAmount(string note, double amount)
        {
            ClientRequestDispatcher.TheInstance()
                                   .DispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteRequest("DatabaseManager, updateATMPinNumber()", "Attempt to read ATMUsers database"));

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[ATMCash] Set [Amount]=@b WHERE [Note] = @a ";
                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = note;
                cmd.Parameters
                   .Add(new SqlParameter("@b", SqlDbType.Float))
                   .Value = amount;

                connection.Open();
                cmd.ExecuteScalar();
                cmd.Dispose();
                connection.Dispose();
            }
        }

        internal void AddWithdrawalToDatabase(string originatingAccount, double amount)
        {
            ClientRequestDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(originatingAccount, " Withdrawal ", amount));
            writeTransactionToDatabase(originatingAccount,""," Withrawal ", amount);
        }

        internal void AddDepositToDatabase(string originatingAccount, double amount)
        {
            ClientRequestDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(originatingAccount, " Deposit ", amount));
            writeTransactionToDatabase("", originatingAccount, " Deposit ", amount);
        }
       
        internal void AddTransferToDatabase(string originatingAccount, string RecipientAccount, string type, double amount)
        {
             ClientRequestDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(originatingAccount, " Transfer to " + RecipientAccount, amount));
             ClientRequestDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(RecipientAccount, "Transfer from " + originatingAccount, amount));
             writeTransactionToDatabase(originatingAccount, RecipientAccount, " Transfer ", amount);
        }

        private void writeTransactionToDatabase(string originatingAccountNumber, string recipientAccountNumber, string type, double amount)
        {
            var dateAndTime = DateTime.Now;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"INSERT INTO [dbo].[BankTransactions](Id, OutgoingAccount, IncomingAccount, Type, Amount, Date, OutgoingAccountBalance, IncomingAccountBalance) VALUES (@id, @oa, @ra, @t, @a, @d, @oab, @rab )";

                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier))
                    .Value = Guid.NewGuid();

                cmd.Parameters
                                    .Add(new SqlParameter("@oa", SqlDbType.NVarChar))
                                    .Value = originatingAccountNumber;
                cmd.Parameters
                                    .Add(new SqlParameter("@ra", SqlDbType.NVarChar))
                                    .Value = recipientAccountNumber;
                cmd.Parameters
                                   .Add(new SqlParameter("@oab", SqlDbType.Float))
                                   .Value = GetAccountBalance(originatingAccountNumber);
                cmd.Parameters
                                    .Add(new SqlParameter("@rab", SqlDbType.Float))
                                    .Value = GetAccountBalance(recipientAccountNumber);
                cmd.Parameters
                                     .Add(new SqlParameter("@t", SqlDbType.NVarChar))
                                     .Value = type;
                cmd.Parameters
                                    .Add(new SqlParameter("@a", SqlDbType.Float))
                                    .Value = amount;
                cmd.Parameters
                                    .Add(new SqlParameter("@d", SqlDbType.NVarChar))//JACK SEE THIS
                                    .Value = dateAndTime.ToString("dd/MM/yyyy");
                
                connection.Open();

                cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Dispose();
            }
        }


        public void UpdateATMPinNumber(string accountNumber, string newPin)
        {
            ClientRequestDispatcher.TheInstance()
                                   .DispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteRequest("DatabaseManager, updateATMPinNumber()", "Attempt to read ATMUsers database"));

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


        public static string GetAccountByATMCardNumber(string cardNumber)
        {
            string accountNumber;

            ClientRequestDispatcher.TheInstance()
                                   .DispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("DatabaseManager, getAccountByATMCardNumber() method", "Attempt to read ATMUsers database"));

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [AccountNumber] From [dbo].[Users] WHERE [CardNumber] = @a ";
                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = cardNumber.Trim();

                connection.Open();
                accountNumber = Convert.ToString(cmd.ExecuteScalar());
                cmd.Dispose();
                connection.Dispose();
            }
            Debug.WriteLine("Account number returned was: " + accountNumber);
            return accountNumber;
        }


        public double GetAccountBalance(string AccountNumber)
        {
            double balance;
            ClientRequestDispatcher.TheInstance()
                                   .DispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("DatabaseManager, getAccountBalance() method", "Attempt to read Account database"));
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


        public string GetATMAccountPin(string AccountNumber)
        {
            string pin;

            ClientRequestDispatcher.TheInstance()
                                   .DispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadRequest("DatabaseManager, getATMAccountPin() method", "Attempt to read ATMUsers database"));

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

        public List<List<string>> PopulateStatement(string accountNumber)
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT * From [dbo].[BankTransactions] WHERE [OutgoingAccount] = @a OR [IncomingAccount] = @a ORDER BY [Date]";
                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                                .Value = accountNumber;

                List<List<string>> transactionList = new List<List<string>>();
                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int DebitAccount = reader.GetOrdinal("OutgoingAccount");
                    int CreditAccount = reader.GetOrdinal("IncomingAccount");
                    int Type = reader.GetOrdinal("Type");
                    int Amount = reader.GetOrdinal("Amount");
                    int Date = reader.GetOrdinal("Date");
                    int DebitBalance = reader.GetOrdinal("OutgoingAccountBalance");
                    int CreditBalance = reader.GetOrdinal("IncomingAccountBalance");

                    while (reader.Read())
                    {
                        List<string> transaction = new List<string>();
                        transaction.Add(reader.GetString(DebitAccount));
                        transaction.Add(reader.GetString(CreditAccount));
                        transaction.Add(reader.GetString(Type));
                        transaction.Add(Convert.ToString(reader.GetDouble(Amount)));

                        string date = reader.GetString(Date);
                        date = date.Substring(0, 10);
                        transaction.Add(date);

                        if(accountNumber == reader.GetString(DebitAccount))
                        {

                            transaction.Add(Convert.ToString(reader.GetDouble(DebitBalance)));
                        }
                        else
                        {

                            transaction.Add(Convert.ToString(reader.GetDouble(CreditBalance)));
                        }

                        transactionList.Add(transaction);
                    }
                }
                cmd.Dispose();
                connection.Dispose();

                return transactionList;
            }
        }
    }
}
