using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetCash.Models
{
    public class Insurance { 
        public string apply = " thank you ";

        public class InsuranceCustomer 
    {
        public int InsuranceRisk { get; set; }
        public string Value { get; set; }
        }

        [Required]
        [Display(Name = "Customers age")]
        public int age { get; set; }

        [Required]
        [Display(Name = "What is your job risk low to high (1-4)")]
        public string risk { get; set; }
       

        public IEnumerable<InsuranceCustomer> InsuranceRisk =
            new List<InsuranceCustomer>
            {
                new InsuranceCustomer {InsuranceRisk = 0, Value = "1" },
                new InsuranceCustomer {InsuranceRisk = 1, Value = "2" },
                new InsuranceCustomer {InsuranceRisk = 2, Value = "3" },
                new InsuranceCustomer {InsuranceRisk = 3, Value = "4" },
            };

    }
}