using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication5.Models.Insurance;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace NetCash.Models
{
    public class Insurance
    {
        public string apply = " thank you ";
        public double quote { get; set; }

        public string AccountNumber { get; set; }

        public DateTime DateOfApplication { get; set; }

        public bool Discussed { get; set; }

        IInsuranceStrategy strategy { get; set; }

        internal bool PendingQueryExists()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT * FROM [dbo].[InsuranceQuery] " +
                                  @"WHERE [AccountNumber] = @an AND [Discussed] = 0";

                Debug.WriteLine(AccountNumber);
                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = this.AccountNumber;
                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
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

        internal bool PendingQueryExists(object AccountNumber)
        {
            this.AccountNumber = AccountNumber.ToString();
            return PendingQueryExists();
        }

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


        public void CalculatePremium()
        {
            quote = strategy.calculatequote(
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

        public class InsuranceCustomer
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


        public static List<LocationType> LocationTypeList = new List<LocationType>
           {
                new LocationType {LocationIntValue = 1, StringValue = "Connacht" },
                new LocationType {LocationIntValue = 2, StringValue = "Munster" },
                new LocationType {LocationIntValue = 3, StringValue = "Leinster" },
                new LocationType {LocationIntValue = 4, StringValue = "Ulster" }
           };

        [Required]
        [Display(Name = "In which province do you currently reside?")]
        public string LocationChoice { get; set; }

        public IEnumerable<LocationType> LocationOptions = LocationTypeList;



        public void SubmitApplication(string AccountNumber)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {

                string _sql = @"INSERT INTO [dbo].[InsuranceQuery] (GUID, AccountNumber, Insurancetype, AgeBracket, Location, Date, Discussed) VALUES (@id, @an,@in, @ab, @lc, @dt, @d)";

                var cmd = new SqlCommand(_sql, connection);

                cmd.Parameters
                    .Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier))
                    .Value = Guid.NewGuid();
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = AccountNumber.ToString();
                cmd.Parameters
                    .Add(new SqlParameter("@in", SqlDbType.NVarChar))
                    .Value = InsuranceTypeChoice;
                cmd.Parameters
                    .Add(new SqlParameter("@ab", SqlDbType.NVarChar))
                    .Value = AgeChoice;
                cmd.Parameters
                    .Add(new SqlParameter("@lc", SqlDbType.NVarChar))
                    .Value = LocationChoice;
                cmd.Parameters
                   .Add(new SqlParameter("@dt", SqlDbType.DateTime))
                   .Value = DateTime.Now;
                cmd.Parameters
                   .Add(new SqlParameter("@d", SqlDbType.Bit))
                   .Value = 0;

                connection.Open();

                cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Dispose();
            }
        }

        public void GetInsuranceByAccountNumber()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"SELECT * FROM [dbo].[InsuranceQuery] " +
                                  @"WHERE [AccountNumber] = @an";

                Debug.WriteLine(AccountNumber);
                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = this.AccountNumber;
                connection.Open();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Debug.WriteLine("well");
                    this.InsuranceTypeChoice = reader.GetString(reader.GetOrdinal("Insurancetype"));
                    this.AgeChoice = reader.GetString(reader.GetOrdinal("AgeBracket"));
                    this.LocationChoice = reader.GetString(reader.GetOrdinal("Location"));
                    this.DateOfApplication = reader.GetDateTime(reader.GetOrdinal("Date"));
                    reader.Dispose();
                    cmd.Dispose();
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                }
            }
        }

        public void MarkInsuranceAsDiscussed()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                string _sql = @"UPDATE [dbo].[InsuranceQuery] " + @"SET [Discussed] = 1 " +
                                  @"WHERE [AccountNumber] = @an";

                var cmd = new SqlCommand(_sql, connection);
                cmd.Parameters
                    .Add(new SqlParameter("@an", SqlDbType.NVarChar))
                    .Value = this.AccountNumber;
                connection.Open();

                var reader = cmd.ExecuteScalar();

                cmd.Dispose();
                connection.Close();
            }

        }
    }
}
