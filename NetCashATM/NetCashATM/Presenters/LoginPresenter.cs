using BankingFramework.FacadeClasses;
using System.Configuration;
using BankingFramework.InterceptorPackage.Dispatchers;
using BankingFramework.InterceptorPackage.ContextObjects;

namespace NetCashATM.Presenters
{
    public class LoginPresenter
    {
        private static int _remainingAttempts = 3;
        private ATMFacade _atmFacade;

        public LoginPresenter()
        {
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
        }

        public void Login(string pin)
        { 
            if (!_atmFacade.IsCardCancelled())
            {
                if (_atmFacade.ValidateAccount(pin))
                {
                    _remainingAttempts = 3;
                    NavigationRequestDispatcher.TheInstance()
                        .DispatchNavigationRequestInterceptors(new NavigationContextObject("MainPanel"));
                }
                else
                {
                    if (_remainingAttempts != 0)
                    {
                        _remainingAttempts--;
                        NavigationRequestDispatcher.TheInstance()
                       .DispatchNavigationRequestInterceptors(new NavigationContextObject("PinRetryPanel"));
                    }
                    else
                    {
                        _atmFacade.CancelCard(ConfigurationManager.AppSettings["CardNumber"]);

                        NavigationRequestDispatcher.TheInstance()
                        .DispatchNavigationRequestInterceptors(new NavigationContextObject("CardCancelledPanel"));
                    }
                }
            }
            else
            {
                NavigationRequestDispatcher.TheInstance()
                            .DispatchNavigationRequestInterceptors(new NavigationContextObject("CardCancelledPanel"));
            }
        }
    }
}
