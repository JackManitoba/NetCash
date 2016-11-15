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

            LoggingInfoDispatcher.TheInstance()
                                   .DispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteContextObject("DatabaseManager, retrieveDenominationAmounts()", "Attempt to read ATMCash database"));

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
            LoggingInfoDispatcher.TheInstance()
                                   .DispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteContextObject("DatabaseManager, updateATMPinNumber()", "Attempt to read ATMUsers database"));

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
            LoggingInfoDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfoContextObject(originatingAccount, " Withdrawal ", amount));
            writeTransactionToDatabase(originatingAccount,""," Withrawal ", amount);
        }

        internal void AddDepositToDatabase(string originatingAccount, double amount)
        {
            LoggingInfoDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfoContextObject(originatingAccount, " Deposit ", amount));
            writeTransactionToDatabase("", originatingAccount, " Deposit ", amount);
        }
       
        internal void AddTransferToDatabase(string originatingAccount, string RecipientAccount, string type, double amount)
        {
             LoggingInfoDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfoContextObject(originatingAccount, " Transfer to " + RecipientAccount, amount));
             LoggingInfoDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfoContextObject(RecipientAccount, "Transfer from " + originatingAccount, amount));
             writeTransactionToDatabase(originatingAccount, RecipientAccount, " Transfer ", amount);
        }

        private void writeTransactionToDatabase(string originatingAccountNumber, string recipientAccountNumber, string type, double amount)
        {
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
                    .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                    .Value = DateTime.Now.ToString("dd/MM/yyyy");
                
                connection.Open();
                cmd.ExecuteScalar();
                cmd.Dispose();
                connection.Dispose();
            }
        }


        public void UpdateATMPinNumber(string accountNumber, string newPin)
        {
            LoggingInfoDispatcher.TheInstance()
                                   .DispatchClientRequestInterceptorWriteDatabaseRequest(new DatabaseWriteContextObject("DatabaseManager, updateATMPinNumber()", "Attempt to read ATMUsers database"));

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

            LoggingInfoDispatcher.TheInstance()
                                   .DispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadContextObject("DatabaseManager, getAccountByATMCardNumber() method", "Attempt to read ATMUsers database"));

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

            return accountNumber;
        }


        public double GetAccountBalance(string accountNumber)
        {
            double balance;

            LoggingInfoDispatcher.TheInstance()
                                   .DispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadContextObject("DatabaseManager, getAccountBalance() method", "Attempt to read Account database"));

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [Balance] From [dbo].[Account] WHERE [AccountNumber] = @a ";
                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = accountNumber;

                connection.Open();
                balance = Convert.ToDouble(cmd.ExecuteScalar());
                cmd.Dispose();
                connection.Dispose();
            }

            return balance;
        }

        public string GetATMAccountPin(string accountNumber)
        {
            string pin;

            LoggingInfoDispatcher.TheInstance()
                                   .DispatchClientRequestInterceptorReadDatabaseRequest(new DataBaseReadContextObject("DatabaseManager, getATMAccountPin() method", "Attempt to read ATMUsers database"));

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [PIN] From [dbo].[Users] WHERE [AccountNumber] = @a ";
                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = accountNumber;

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

        public void UpdateBalance(string accountNumber, double _amount)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[Account] Set [Balance]=@b WHERE [AccountNumber] = @a ";
                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = accountNumber;
                cmd.Parameters
                    .Add(new SqlParameter("@b", SqlDbType.Float))
                    .Value = GetAccountBalance(accountNumber) + _amount;

                connection.Open();
                cmd.ExecuteScalar();
                cmd.Dispose();
                connection.Dispose();
            }
        }

        public bool PendingApplicationExists(string accountNumber)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT * FROM [dbo].[LoanApplications] WHERE [AccountNumber] = @an AND [Discussed] = 0 ";

                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = accountNumber;

                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cmd.Dispose();
                    connection.Dispose();
                    return true;
                }
                else
                {
                    cmd.Dispose();
                    connection.Dispose();
                    return false;
                }
            }
        }

        public List<string> GetLoanDataByAccountNumber(string accountNumber)
        {
            List<string> loanApplication = new List<string>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT * FROM [dbo].[LoanApplications] " +
                                  @"WHERE [AccountNumber] = @an AND [Discussed] = 0";              
                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = accountNumber;
                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    loanApplication.Add(reader.GetString(reader.GetOrdinal("LoanType")));
                    loanApplication.Add(reader.GetString(reader.GetOrdinal("AmountRequired")));
                    loanApplication.Add(reader.GetString(reader.GetOrdinal("RepaymentPeriod")));

                    string date = reader.GetString(reader.GetOrdinal("DateOfApplication"));
                    date = date.Substring(0, 10);
                    loanApplication.Add(date);

                    reader.Dispose();
                    cmd.Dispose();
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                }
            }

            return loanApplication;
        }

        public bool DoesPendingQueryExist(string accountNumber)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT * FROM [dbo].[InsuranceQuery] " +
                                  @"WHERE [AccountNumber] = @an AND [Discussed] = 0";
                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = accountNumber;
                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return true;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return false;
                }
            }
        }

        public void SubmitInsuranceQuery(string AccountNumber, string insuranceType, string ageBracket, string location)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                string _sql = @"INSERT INTO [dbo].[InsuranceQuery] (GUID, AccountNumber, Insurancetype, AgeBracket, Location, Date, Discussed) VALUES (@id, @an,@in, @ab, @lc, @dt, @d)";

                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier))
                    .Value = Guid.NewGuid();
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = AccountNumber.ToString();
                cmd.Parameters
                    .Add(new SqlParameter("@in", SqlDbType.NVarChar))
                    .Value = insuranceType;
                cmd.Parameters
                    .Add(new SqlParameter("@ab", SqlDbType.NVarChar))
                    .Value = ageBracket;
                cmd.Parameters
                    .Add(new SqlParameter("@lc", SqlDbType.NVarChar))
                    .Value = location;
                cmd.Parameters
                     .Add(new SqlParameter("@dt", SqlDbType.NVarChar))
                     .Value = DateTime.Now.ToString("dd/MM/yyyy");
                cmd.Parameters
                    .Add(new SqlParameter("@d", SqlDbType.Bit))
                    .Value = 0;

                connection.Open();
                cmd.ExecuteScalar();
                cmd.Dispose();
                connection.Dispose();
            }
        }

        public List<string> GetInsuranceByAccountNumber(string accountNumber)
        {

            List<string> insuranceQuery = new List<string>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT * FROM [dbo].[InsuranceQuery] " +
                                  @"WHERE [AccountNumber] = @an";
                
                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = accountNumber;
                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    insuranceQuery.Add(reader.GetString(reader.GetOrdinal("Insurancetype")));
                    insuranceQuery.Add(reader.GetString(reader.GetOrdinal("AgeBracket")));
                    insuranceQuery.Add(reader.GetString(reader.GetOrdinal("Location")));

                    string date = reader.GetString(reader.GetOrdinal("Date"));
                    date = date.Substring(0, 10);
                    insuranceQuery.Add(date);

                    reader.Dispose();
                    cmd.Dispose();
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                }
            }
            return insuranceQuery;
        }

        public void MarkInsuranceAsDiscussed(string accountNumber)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[InsuranceQuery] " + @"SET [Discussed] = 1 " +
                                  @"WHERE [AccountNumber] = @an";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = accountNumber;
                connection.Open();

                var reader = cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Close();
            }
        }

        public void MarkLoanAsDiscussed(string accountNumber)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[LoanApplications] " + @"SET [Discussed] = 1 " +
                                  @"WHERE [AccountNumber] = @an";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = accountNumber;
                connection.Open();

                var reader = cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Close();
            }
        }

        public void SubmitLoanApplication(string accountNumber, string loanType, string amountRequired, string repaymentPeriod)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"INSERT INTO [dbo].[LoanApplications] (GUID, AccountNumber, LoanType, AmountRequired, RepaymentPeriod, DateOfApplication, Discussed) VALUES (@id, @an, @lt, @ar, @rp, @doa, @d) ";

                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier))
                    .Value = Guid.NewGuid();
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = accountNumber;
                cmd.Parameters
                    .Add(new SqlParameter("@lt", SqlDbType.NVarChar))
                    .Value = loanType;
                cmd.Parameters
                    .Add(new SqlParameter("@ar", SqlDbType.NVarChar))
                    .Value = amountRequired;
                cmd.Parameters
                    .Add(new SqlParameter("@rp", SqlDbType.NVarChar))
                    .Value = repaymentPeriod;
                cmd.Parameters
                     .Add(new SqlParameter("@doa", SqlDbType.NVarChar))
                     .Value = DateTime.Now.ToString("dd/MM/yyyy");
                cmd.Parameters
                   .Add(new SqlParameter("@d", SqlDbType.Bit))
                   .Value = 0;

                connection.Open();
                cmd.ExecuteScalar();
                cmd.Dispose();
                connection.Dispose();
            }
        }
    }
}
