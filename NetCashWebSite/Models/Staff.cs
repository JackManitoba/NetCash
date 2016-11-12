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
    public class Staff
    {      
         [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }
                
        [Required]
        [Display(Name = "Username :")]
        public string UserName { get; set; }

        public bool IsValid(string _Username, string _Password)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT [UserName] FROM [dbo].[BankStaff] " +
                                  @"WHERE [Username] = @u AND [Password] = @p ";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = _Username;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = _Password;
                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    UserName = reader.GetString(reader.GetOrdinal("Username"));
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
