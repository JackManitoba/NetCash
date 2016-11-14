using BankingFramework.InterceptorPackage.Dispatchers;
using BankingFramework.InterceptorPackage.Interceptors;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NetCash
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ClientRequestInterceptor myInterceptor = new ClientRequestInterceptor();
            ClientRequestDispatcher.TheInstance().RegisterClientInterceptor(myInterceptor);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }
    }
}
