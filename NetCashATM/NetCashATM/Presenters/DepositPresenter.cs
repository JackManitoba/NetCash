using BankingFramework.FacadeClasses;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using NetCashATM.ATMHardware;
using System;
using System.Configuration;

namespace NetCashATM.Presenters
{
    public class DepositPresenter
    {
        private ATMFacade _atmFacade;
        private ATMCashManager _cashManager = new ATMCashManager();

        public DepositPresenter()
        {
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
        }

        public void Deposit(string amount)
        {
            if (amount != "")
            { 
                if (((Convert.ToInt32(amount)) % 10) != 0)
                {
                    NavigationRequestDispatcher.TheInstance()
                        .DispatchNavigationRequestInterceptors(new NavigationContextObject("DepositErrorPanel"));
                }
                else
                {
                    double depositAmount = double.Parse(amount);
                    _atmFacade.PerformDeposit(depositAmount);
                    _cashManager.UpdateAmountDeposit(depositAmount);

                    NavigationRequestDispatcher.TheInstance()
                        .DispatchNavigationRequestInterceptors(new NavigationContextObject("MainPanel"));
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
