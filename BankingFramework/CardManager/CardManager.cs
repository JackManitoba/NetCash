﻿using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BankingFramework.CardManager
{
    public static class CardManager
    {
        public static void CancelCard(string cardNumber)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[Users] Set [Cancelled]=@b WHERE [CardNumber] = @a ";
                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                    .Value = cardNumber;
                cmd.Parameters
                    .Add(new SqlParameter("@b", SqlDbType.Int))
                    .Value = 1;
                connection.Open();
                cmd.ExecuteScalar();
                cmd.Dispose();
                connection.Dispose();
            }
        }
    }
}
