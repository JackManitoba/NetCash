﻿using BankingFramework.Interceptor_Package;
using BankingFramework.Interceptor_Package.Dispatchers;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using BankingFramework.BankTransactions;

namespace BankingFramework.Utils
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


        public int RetrieveDenominationAmounts(string current)
        {
            int returnValue;

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
                returnValue = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cmd.Dispose();
                connection.Dispose();
            }
            return returnValue;
        }


        public void UpdateATMCashAmount(string note, int amount)
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
                   .Add(new SqlParameter("@b", SqlDbType.Int))
                   .Value = amount;

                connection.Open();
                cmd.ExecuteScalar();
                cmd.Dispose();
                connection.Dispose();
            }
        }


        internal void AddTransactionToDatabase(Transaction t)
        {
            var dateAndTime = DateTime.Now;
            
            Debug.WriteLine(dateAndTime.ToString("dd/MM/yyyy"));
                        using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                            {
                string _sql = @"INSERT INTO [dbo].[BankTransactions] VALUES (@origAccount,@targetAccount,@type,@amount,@date) ";
                
                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                                    .Add(new SqlParameter("@origAccount", SqlDbType.NVarChar))
                                    .Value = t.SourceAccount();
                cmd.Parameters
                                    .Add(new SqlParameter("@targetAccount", SqlDbType.NVarChar))
                                    .Value = t.TargetAccount();
                cmd.Parameters
                                     .Add(new SqlParameter("@type", SqlDbType.NVarChar))
                                     .Value = t.GetType();
                cmd.Parameters
                                    .Add(new SqlParameter("@amount", SqlDbType.Money))
                                    .Value = Convert.ToInt32(t.GetAmount());
                cmd.Parameters
                                    .Add(new SqlParameter("@date", SqlDbType.NVarChar))
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
    }
}