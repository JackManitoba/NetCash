
namespace BankingFramework.InterceptorPackage.ContextObjects
{
   public interface ContextObject
    {
        string GetObj();
        string GetVerboseDescription();
        string GetShortDescription();

        void service();
    }
}
