using NetCashATM.UserInterface.Panels;
using BankingFramework.FacadeClasses;
using System.Configuration;
using System.Diagnostics;
using BankingFramework.InterceptorPackage.Dispatchers;
using BankingFramework.InterceptorPackage.ContextObjects;

namespace NetCashATM.Presenters
{
    public class LoginPresenter
    {
        private PinPanel _pinPanel;
        private ATMFacade _atmFacade;

        public LoginPresenter(PinPanel pinPanel)
        {
            Debug.WriteLine("PinPresenter.PinPresenter()");
            _pinPanel = pinPanel;
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
        }

        public void Login(string pin)
        {
            if (!_atmFacade.IsCardCancelled())
            {
                if (_atmFacade.ValidateAccount(pin))
                {
                    Debug.WriteLine("PinPanel.GetPinPanel");
                    NavigationRequestDispatcher.TheInstance()
                        .DispatchNavigationRequestInterceptors(new NavigationContextObject("MainPanel"));
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int remainingAttempts = 3 - i;
                        _pinPanel.DisplayMessage("INCORRECT PIN. ATTEMPTS REMAINING: " + remainingAttempts);
                    }

                    _pinPanel.DisplayMessage("YOUR CARD HAS BEEN CANCELLED");
                    _atmFacade.CancelCard(ConfigurationManager.AppSettings["CardNumber"]);
                }
            }
            else
            {
                _pinPanel.DisplayMessage("THIS CARD HAS BEEN CANCELLED");
            }
        }

        public void Logout()
        {
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject("LogoutPanel"));
        }
    }
}
