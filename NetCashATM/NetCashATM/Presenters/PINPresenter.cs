using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCashATM.UserInterface.Panels;
using BankingFramework.FacadeClasses;
using System.Configuration;
using BankingFramework.Interceptor_Package.Dispatchers;
using BankingFramework.Interceptor_Package.ContextObjects;

namespace NetCashATM.Presenters
{
    public class PINPresenter
    {
        private PinPanel _pinPanel;
        private ATMFacade _atmFacade;

        public PINPresenter(PinPanel pinPanel)
        {
            _pinPanel = pinPanel;
            NavigationRequestDispatcher.TheInstance().DispatchNavigationRequestInterceptors(new NavigationContextObject("PinPanel"));
        }

        public ATMPanel GetPinPanel()
        {
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
            NavigationRequestDispatcher.TheInstance().DispatchNavigationRequestInterceptors(new NavigationContextObject("PinPanel"));
            return _pinPanel;
        }

        public ATMPanel GetPinPanel(string pin)
        {
            if(_atmFacade.ValidateAccount(pin))
            {
                return MainPanel();
            }
            return new PinRetryPanel;
        }
    }
}
