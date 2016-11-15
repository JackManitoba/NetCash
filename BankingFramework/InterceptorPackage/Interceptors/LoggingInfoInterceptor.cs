using BankingFramework.InterceptorPackage.ContextObjects;

namespace BankingFramework.InterceptorPackage.Interceptors
{
    public class LoggingInfoInterceptor : Interceptor
    {
        public void ConsumeService(ContextObject e)
        {
            e.Service();
        }   
    }
}
