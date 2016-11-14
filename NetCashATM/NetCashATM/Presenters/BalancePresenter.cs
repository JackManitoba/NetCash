using BankingFramework.FacadeClasses;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;

using System.Configuration;
using System.Diagnostics;

namespace NetCashATM.Presenters
{
    class BalancePresenter
    {
       
        private ATMFacade _atmFacade;

        /* public BalancePresenter(BalancePanel balancePanel)
         {
             Debug.WriteLine("BalancePresenter.BalancePresenter()");
             _balancePanel = balancePanel;

             
         }*/

        public string retrieveBalance()
        {
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
            return _atmFacade.ReturnAccountBalance();
            
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
