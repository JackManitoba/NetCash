using BankingFramework.FacadeClasses;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using NetCashATM.UserInterface.Panels;
using System.Configuration;

namespace NetCashATM.Presenters
{
    class PinResetPresenter
    {
        private PinResetPanel _pinResetPanel;
        private ATMFacade _atmFacade;

        public PinResetPresenter(PinResetPanel pinResetPanel)
        {
            _pinResetPanel = pinResetPanel;
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
        }

        public void ResetPin()
        {
            string newPin = _pinResetPanel.GetInput().Text;

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
