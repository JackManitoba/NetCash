
namespace NetCashWebSite.Models.Insurance
{
    public class Home_insurance : IInsuranceStrategy
    {
        public double calculatequote(int ageBracket, int Location)
        {
            return Location * 400 + ageBracket * 400;
        }
    }
}