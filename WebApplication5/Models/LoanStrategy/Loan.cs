using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCash.Models
{
    public class Loan
    {  
        string Title { get; set; }

        public class LoanType
        {
            public int LoanTypeID { get; set; }
            public string Value { get; set; }
        }

        [Required(ErrorMessage = "You mest provide a reason for your Loan")]
        [Display(Name = "Reason for Loan")]
        public string LoanChoice { get; set; }

        public IEnumerable<LoanType> LoanTypeOptions =
            new List<LoanType>
            {
                new LoanType {LoanTypeID = 0, Value = "Mortgage" },
                new LoanType {LoanTypeID = 1, Value = "Car Loan" },
                new LoanType {LoanTypeID = 2, Value = "Education Costs" },
                new LoanType {LoanTypeID = 3, Value = "Other" },
            };

        [Required(ErrorMessage = "Please specifiy an amount required")]
        [Display(Name = "Amount Required")]
        public string AmountRequired { get; set; }

        [Required]
        [Display(Name = "Period Of Repayment")]
        [DataType(DataType.Time)]
        public string PeriodOfRepayment { get; set; }

        public void SubmitApplication(object AccountNumber)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                string _sql = @"INSERT INTO [dbo].[Loans] (AccountNumber, LoanType, AmountRequired, RepaymentPeriod) VALUES (@an, @lt, @ar, @rp)";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = AccountNumber.ToString();

                cmd.Parameters
                    .Add(new SqlParameter("@lt", SqlDbType.NVarChar))
                    .Value = LoanChoice;

                cmd.Parameters
                    .Add(new SqlParameter("@ar", SqlDbType.NVarChar))
                    .Value = AmountRequired;

                cmd.Parameters
                    .Add(new SqlParameter("@rp", SqlDbType.NVarChar))
                    .Value = PeriodOfRepayment;

                connection.Open();

                cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Dispose();
            }
        }
    }
}