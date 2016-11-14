using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingFramework.Utils
{
    public class Statement
    {
        //Order of items in each list:
        //DebitAccount, CreditAccount, Type, Amount, Date, DebitBalance, CreditBalance
        //this is the same as the order in the database Table BankTransactions
        private List<List<string>> _listOfTransactions = new List<List<string>>();


        public Statement(string accountNumber)
        {
            populateListOfTransactions(accountNumber);            
        }

        public List<List<string>> getListOfTransactions()
        {
            return _listOfTransactions;
        }

        private void populateListOfTransactions(string accountNumber)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT * From [dbo].[BankTransactions] WHERE [DebitAccount] = @a OR [CreditAccount] = @a ORDER BY [Date]";
                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = accountNumber;
                

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                { 
                    int DebitAccount = reader.GetOrdinal("DebitAccount");
                    int CreditAccount = reader.GetOrdinal("CreditAccount");
                    int Type = reader.GetOrdinal("Type");
                    int Amount = reader.GetOrdinal("Amount");
                    int Date = reader.GetOrdinal("Date");
                    int DebitBalance = reader.GetOrdinal("DebitBalance");
                    int CreditBalance = reader.GetOrdinal("CreditBalance");
                    
                    while (reader.Read())
                    {
                        List<string> transactionList = new List<string>();
                        transactionList.Add(reader.GetString(DebitAccount));
                        transactionList.Add(reader.GetString(CreditAccount));
                        transactionList.Add(reader.GetString(Type));
                        transactionList.Add(Convert.ToString(reader.GetInt32(Amount)));

                        string date = Convert.ToString(reader.GetSqlDateTime(Date));
                        date = date.Substring(0, 10);
                        transactionList.Add(date);

                        transactionList.Add(Convert.ToString(reader.GetInt32(DebitBalance)));                        
                        transactionList.Add(Convert.ToString(reader.GetInt32(CreditBalance)));

                        _listOfTransactions.Add(transactionList);
                    }
                }
                cmd.Dispose();
                connection.Dispose();
            }
        }
    }
}
