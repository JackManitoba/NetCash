using BankingFramework.FacadeClasses;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using System.Configuration;

namespace NetCashATM.Presenters
{
    public class BalancePresenter
    {     
        private ATMFacade _atmFacade;

        public string retrieveBalance()
        {
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
            return _atmFacade.ReturnAccountBalance();       
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
