using BankingFramework.Utils;
using System.Diagnostics;
using BankingFramework.InterceptorPackage.ContextObjects;

namespace BankingFramework.InterceptorPackage.Interceptors
{
    public class ClientRequestInterceptor : Interceptor
    {
       

        public void consumeService(ContextObject e)
        {
            e.service();
        }   
    }
}
