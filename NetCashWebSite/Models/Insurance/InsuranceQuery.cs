using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace NetCashWebSite.Models.Insurance
{
    public class InsuranceQuery
    { 
        public double Quote { get; set; }

        [Required(ErrorMessage = "Please specifiy an account number")]
        [Display(Name = "Account Number: ")]
        public string AccountNumber { get; set; }

        [Display(Name = "Date of Application: ")]
        public string DateOfApplication { get; set; }

        IInsuranceStrategy strategy { get; set; }

        public static List<InsuranceType> InsuranceTypeList = new List<InsuranceType>
            {
                new InsuranceType {TypeIntValue = 1, StringValue = "Travel" },
                new InsuranceType {TypeIntValue = 2, StringValue = "Car" },
                new InsuranceType {TypeIntValue = 3, StringValue = "Personal" },
                new InsuranceType {TypeIntValue = 4, StringValue = "Home" },
            };

        public static List<AgeType> AgeTypeList = new List<AgeType>
           {
                new AgeType {ageBracketIntValue = 1, StringValue = "0-30" },
                new AgeType {ageBracketIntValue = 2, StringValue = "30-65" },
                new AgeType {ageBracketIntValue = 3, StringValue = "65+" },
           };

        public static List<LocationType> LocationTypeList = new List<LocationType>
           {
                new LocationType {LocationIntValue = 1, StringValue = "Connacht" },
                new LocationType {LocationIntValue = 2, StringValue = "Munster" },
                new LocationType {LocationIntValue = 3, StringValue = "Leinster" },
                new LocationType {LocationIntValue = 4, StringValue = "Ulster" }
           };

        public void CalculatePremium()
        {
            Quote = strategy.calculatequote(
                LocationTypeList.Find(locationType => String.Equals(locationType.StringValue, LocationChoice)).LocationIntValue,
                AgeTypeList.Find(AgeType => String.Equals(AgeType.StringValue, AgeChoice)).ageBracketIntValue);
        }

        public class InsuranceType
        {
            public string StringValue { get; set; }
            public int TypeIntValue { get; set; }
        }

        public class AgeType
        {
            public string StringValue { get; set; }
            public int ageBracketIntValue { get; set; }
        }

        public class LocationType
        {
            public int LocationIntValue { get; set; }
            public string StringValue { get; set; }
        }

        public class InsuranceQuote
        {
            public int InsuranceType { get; set; }
            public int ageBracket { get; set; }
            public int Location { get; set; }
            public string Value { get; set; }

        }

        internal void SetStrategy()
        {
            switch (InsuranceTypeChoice)
            {
                case "Travel":
                    strategy = new Travel_Insurance();
                    break;
                case "Car":
                    strategy = new Car_Insurance();
                    break;
                case "Personal":
                    strategy = new Persnal_Insurance();
                    break;
                case "Home":
                    strategy = new Home_insurance();
                    break;
            }
        }

        [Required]
        [Display(Name = "What type of insurance are you looking for?")]
        public string InsuranceTypeChoice { get; set; }

        public IEnumerable<InsuranceType> InsuranceTypeOptions = InsuranceTypeList;
        [Required]
        [Display(Name = "What is your current age bracket?")]
        public string AgeChoice { get; set; }

        public IEnumerable<AgeType> ageBracketOptions = AgeTypeList;   

        [Required]
        [Display(Name = "In which province do you currently reside?")]
        public string LocationChoice { get; set; }

        public IEnumerable<LocationType> LocationOptions = LocationTypeList;

    }
}
