using BankingFramework.FacadeClasses;
using BankingFramework.Interceptor_Package.ContextObjects;
using BankingFramework.Interceptor_Package.Dispatchers;
using NetCashATM.UserInterface.Panels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
