using BankingFramework.InterceptorPackage.ContextObjects;

namespace BankingFramework.InterceptorPackage.Interceptors
{
    public interface Interceptor
    {
        void consumeService(ContextObject e);
    }
}
