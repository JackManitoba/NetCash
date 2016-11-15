
namespace NetCashWebSite.Models.Insurance
{
    public class Car_Insurance : IInsuranceStrategy
    {
        public double calculatequote(int ageBracket, int Location)
        {
            return Location * 100 + ageBracket * 100;
        }
    }
}