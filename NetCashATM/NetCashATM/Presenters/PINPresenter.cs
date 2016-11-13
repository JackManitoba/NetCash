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
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
        }

        public void GetPinPanel()
        {
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject("PinPanel"));
        }

        public void GetPinPanel(string pin)
        {
            if (_atmFacade.ValidateAccount(pin))
            {
                NavigationRequestDispatcher.TheInstance()
                    .DispatchNavigationRequestInterceptors(new NavigationContextObject("MainPanel"));
            }
            else
            {
                for(int i = 0; i < 3; i++)
                {
                    NavigationRequestDispatcher.TheInstance()
                        .DispatchNavigationRequestInterceptors(new NavigationContextObject("PinRetryPanel"));
                }
            }
        }
    }
}
