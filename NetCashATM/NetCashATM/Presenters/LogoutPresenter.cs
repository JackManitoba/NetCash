using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;

namespace NetCashATM.Presenters
{
    public class LogoutPresenter
    {
        

        public LogoutPresenter()
        {
            
        }

        public void Logout()
        {
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject("PinPanel"));
        }


    }
}
