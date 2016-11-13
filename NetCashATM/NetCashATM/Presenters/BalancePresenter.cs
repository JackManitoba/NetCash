using BankingFramework.FacadeClasses;
using BankingFramework.Interceptor_Package.ContextObjects;
using BankingFramework.Interceptor_Package.Dispatchers;
using NetCashATM.UserInterface.Panels;
using System.Configuration;

namespace NetCashATM.Presenters
{
    class BalancePresenter
    {
        private BalancePanel _balancePanel;
        private ATMFacade _atmFacade;

        public BalancePresenter(BalancePanel balancePanel)
        {
            _balancePanel = balancePanel;

            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
            _balancePanel.ShowBalance(_atmFacade.ReturnAccountBalance());
        }
 
        public void GoToMain()
        {
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject("MainPanel"));
        }

        public void LogOut()
        {
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject("LogoutPanel"));
        }
    }
}
