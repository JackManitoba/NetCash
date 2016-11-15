
namespace NetCashWebSite.Models.Insurance
{
    public class Persnal_Insurance : IInsuranceStrategy
    {
        public double calculatequote(int ageBracket, int Location)
        {
            return Location * 300 + ageBracket * 300;
        } 
    }
}