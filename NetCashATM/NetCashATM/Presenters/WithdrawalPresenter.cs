using BankingFramework.FacadeClasses;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using NetCashATM.ATMHardware;
using System;
using System.Configuration;

namespace NetCashATM.Presenters
{
    public  class WithdrawalPresenter
    {  
        private ATMFacade _atmFacade;
        private ATMCashManager _cashManager;

        public void Withdraw(string input)
        {   
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);

            if (input != "")
            {
                if (((Convert.ToInt32(input)) % 10) != 0)
                {
                    NavigationRequestDispatcher.TheInstance()
                        .DispatchNavigationRequestInterceptors(new NavigationContextObject("WithdrawalPanelError1"));
                }
                else
                {
                    double amount = double.Parse(input);
                    _cashManager = new ATMCashManager();

                    if (_atmFacade.AreFundsAvailable(amount) && _cashManager.IsWithdrawable(amount))
                    {
                        _atmFacade.PerformWithdraw(amount);
                        _cashManager.UpdateAmountWithdrawal(amount);

                        LogOut();
                    }
                    else
                    {
                        NavigationRequestDispatcher.TheInstance()
                            .DispatchNavigationRequestInterceptors(new NavigationContextObject("WithdrawalPanelError2"));
                    }
                }
            }
        }

        public void LogOut()
        {
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject("LogoutPanel"));
        }
    }
}
