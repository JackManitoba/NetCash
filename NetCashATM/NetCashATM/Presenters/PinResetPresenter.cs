using BankingFramework.FacadeClasses;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;

using System.Configuration;

namespace NetCashATM.Presenters
{
    class PinResetPresenter
    {
       
        private ATMFacade _atmFacade;

        public PinResetPresenter()
        {
            
         
        }

        public void ResetPin(string newPin)
        {
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
            

            if(newPin.Length == 4)
            {
                _atmFacade.UpdateAccountPinNumber(newPin);
                GoToMainMenu();
            }
        }

        public void GoToMainMenu()
        {
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject("MainPanel"));
        }
    }
}
