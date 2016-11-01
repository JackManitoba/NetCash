using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NetCash.Models
{
    public class User
    {
        Helpers.IEncryptionHelper EncryptionHelper;
        public string EncryptionType;

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        public bool IsValid(string _email, string _password)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [AccountNumber] FROM [dbo].[Users] " +
                       @"WHERE [Email] = @u AND [Password] = @p ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = _email;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = EncryptionHelper.Encode(_password);
                connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    AccountNumber = reader.GetString(reader.GetOrdinal("AccountNumber"));
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
