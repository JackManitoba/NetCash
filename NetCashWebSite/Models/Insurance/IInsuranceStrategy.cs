
namespace NetCashWebSite.Models.Insurance
{
    interface IInsuranceStrategy
    {
        double calculatequote(int ageBracket , int Location);
    }
}
