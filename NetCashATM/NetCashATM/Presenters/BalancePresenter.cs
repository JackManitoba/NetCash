using BankingFramework.FacadeClasses;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using NetCashATM.UserInterface.Panels;
using System.Configuration;
using System.Diagnostics;

namespace NetCashATM.Presenters
{
    class BalancePresenter
    {
        private BalancePanel _balancePanel;
        private ATMFacade _atmFacade;

        public BalancePresenter(BalancePanel balancePanel)
        {
            Debug.WriteLine("BalancePresenter.BalancePresenter()");
            _balancePanel = balancePanel;

            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
            _balancePanel.ShowBalance(_atmFacade.ReturnAccountBalance());
        }
 
        public void GoToMain()
        {

            Debug.WriteLine("BalancePresenter.GoToMain()");
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
