
using BankingFramework.InterceptorPackage.Interceptors;

namespace BankingFramework.InterceptorPackage
{
   public interface Dispatcher
    {
        void RegisterClientInterceptor(Interceptor i);
        void UnregisterClientInterceptor(Interceptor i);
    }
}
