using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NetCashWebSite.Models
{
    public class User
    {
        public string EncryptionType;

        [Required]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }
        
        public string AccountNumber { get; private set; }

        public string UserName { get; private set; }

        public bool IsValid(string _email, string _password)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [AccountNumber], [UserName] FROM [dbo].[Users] " +
                                  @"WHERE [Email] = @u AND [Password] = @p ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = _email;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = _password;
                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    AccountNumber = reader.GetString(reader.GetOrdinal("AccountNumber"));
                    UserName = reader.GetString(reader.GetOrdinal("UserName"));
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
    }
}
