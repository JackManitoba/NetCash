using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace NetCashWebSite.Models
{
    public class Loan
    {
        [Required(ErrorMessage = "Please specifiy an account number")]
        [Display(Name = "Account Number: ")]
        public string AccountNumber { get; set; }
        
        [Display(Name = "Date of Application: ")]
        public string DateOfApplication { get; set; }

        public class LoanType
        {
            public int LoanTypeID { get; set; }
            public string Value { get; set; }
        }


        [Required(ErrorMessage = "Please specify a reason for your loan")]
        [Display(Name = "Reason for Loan:")]
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
        [Display(Name = "Amount Required:")]
        public string AmountRequired { get; set; }

        [Required(ErrorMessage = "Please specifiy a period Of repayment")]
        [Display(Name = "Period Of Repayment: ")]
        [DataType(DataType.Time)]
        public string PeriodOfRepayment { get; set; }

    }
}