using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetCash.Models
{
    public class LoanSelector
    {
        public class LoanType
        {
            public int LoanTypeID { get; set; }
            public string Value { get; set; }
        }

        [Required]
        [Display(Name = "What is the loan for?")]
        public string LoanChoice { get; set; }

        public IEnumerable<LoanType> LoanTypeOptions =
            new List<LoanType>
            {
                new LoanType {LoanTypeID = 0, Value = "Mortgage" },
                new LoanType {LoanTypeID = 1, Value = "Buying a Car" },
            };
    }
}