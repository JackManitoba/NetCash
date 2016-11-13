using BankingFramework.Interceptor_Package.ContextObjects;
using BankingFramework.Interceptor_Package.Dispatchers;
using NetCashATM.UserInterface.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
