using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication5.Models.ATMModels
{
    public class ATMUser
    {
        [Required]
        [Display(Name = "Account Number")]
        public string accountNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "PIN")]
        public string PIN { get; set; }

        [Required]
        [Display(Name = "Balance")]
        public double balance { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        public string cardNumber { get; set; }


        public ATMUser(string cardnumber)
        {
            cardNumber = cardnumber;
            

        }

        internal void setBalance()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [Balance] FROM [dbo].[Account] " +
                       @"WHERE [AccountNumber] = @u ";
                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                   .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                   .Value = accountNumber;

                connection.Open();
                //connection.Open();
                Debug.WriteLine("Account number=" + accountNumber);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    balance= reader.GetSqlMoney(reader.GetOrdinal("Balance")).ToDouble();
                    Debug.WriteLine("Balance = " + balance);
                    reader.Dispose();
                    cmd.Dispose();
                  
                }
                else
                {
                    //balance = reader.GetSqlMoney(reader.GetOrdinal("Balance")).ToDouble();
                    reader.Dispose();
                    cmd.Dispose();
                    
                }

            }
        }


        public void retrieveBalance()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [AccountNumber] FROM [dbo].[ATMUsers] " +
                       @"WHERE [CardNumber] = @u;";


                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = cardNumber;

                connection.Open();
                //connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    reader.Dispose();
                    cmd.Dispose();
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                }
            }
        }

        public bool IsValid(string _cardNumber, string _pin)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [AccountNumber] FROM [dbo].[ATMUsers] " +
                       @"WHERE [CardNumber] = @u AND [PIN] = @p ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = _cardNumber;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = _pin;
                connection.Open();
                //connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    accountNumber = reader.GetString(reader.GetOrdinal("AccountNumber"));
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

        public bool executeATMTransaction(ATMTransaction transaction)
        {
            if (transaction.type == "Withdrawal") { balance -= transaction.amount; }
            else if (transaction.type == "Deposit") { balance += transaction.amount; }
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[Account] Set [Balance]=@b WHERE [AccountNumber] = @a ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = accountNumber;
                cmd.Parameters
                    .Add(new SqlParameter("@b", SqlDbType.Money))
                    .Value = balance;

                connection.Open();

                cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Dispose();
                return true;
            }
        }
























    }
}