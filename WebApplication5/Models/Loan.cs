using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetCash.Models
{
    public abstract class Loan
    {
        string Title { get; set; }

        [Required]
        [Display(Name = "Amount Required")]
        public string AmountRequired { get; set; }

        [Required]
        [Display(Name = "Period Of Repayment")]
        [DataType(DataType.Time)]
        public string PeriodOfRepayment { get; set; }

    }
}