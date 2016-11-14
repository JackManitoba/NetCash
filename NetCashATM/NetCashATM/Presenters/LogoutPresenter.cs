using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using NetCashATM.UserInterface.Panels;

namespace NetCashATM.Presenters
{
    public class LogoutPresenter
    {
        private LogoutPanel _logoutPanel;

        public LogoutPresenter(LogoutPanel logoutPanel)
        {
            _logoutPanel = logoutPanel;
        }

        public void Logout()
        {
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject("PinPanel"));
        }


    }
}
