using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication5.Models.Insurance;

namespace NetCash.Models
{
    public class Insurance { 
        public string apply = " thank you ";
        public double quote { get; set; }

         IInsuranceStrategy strategy { get; set; }



        public void CalculatePremium()
        {
            quote = strategy.calculatequote(LocationChoice, AgeChoice);
        }

        public class InsuranceCustomer 
    {
        public int InsuranceType { get; set; }
        public int ageBracket { get; set; }
        public int Location { get; set; }
        public string Value { get; set; }

        }

        internal void SetStrategy()
        {
            switch(InsuranceTypeChoice)
            {
                case 1:
                    strategy = new Travel_Insurance();
                    break;
                case 2:
                    strategy = new Car_Insurance();
                    break;
                case 3:
                    strategy = new Persnal_Insurance();
                    break;
                case 4:
                    strategy = new Home_insurance();
                    break;
            }
        }

        [Required]
        [Display(Name = "What type of insurance are you looking for?")]
        public int InsuranceTypeChoice { get; set; }
       

        public IEnumerable<InsuranceCustomer> InsuranceTypeOptions =
            new List<InsuranceCustomer>
            {
                new InsuranceCustomer {InsuranceType = 1, Value = "Travel" },
                new InsuranceCustomer {InsuranceType = 2, Value = "Car" },
                new InsuranceCustomer {InsuranceType = 3, Value = "Personal" },
                new InsuranceCustomer {InsuranceType = 4, Value = "Home" },
            };

        [Required]
        [Display(Name = "What is your current age bracket?")]
        public int AgeChoice { get; set; }

        public IEnumerable<InsuranceCustomer> ageBracketOptions =
           new List<InsuranceCustomer>
           {
                new InsuranceCustomer {ageBracket = 1, Value = "0-30" },
                new InsuranceCustomer {ageBracket = 2, Value = "30-65" },
                new InsuranceCustomer {ageBracket = 3, Value = "65+" },
           };

        [Required]
        [Display(Name = "In which province do you currently reside?")]
        public int LocationChoice { get; set; }

        public IEnumerable<InsuranceCustomer> LocationOptions =
           new List<InsuranceCustomer>
           {
                new InsuranceCustomer {Location = 1, Value = "Connacht" },
                new InsuranceCustomer {Location = 2, Value = "Munster" },
                new InsuranceCustomer {Location = 3, Value = "Leinster" },
                new InsuranceCustomer {Location = 4, Value = "Ulster" }
           };
    }
}