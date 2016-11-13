using BankingFramework.FacadeClasses;
using BankingFramework.Interceptor_Package.ContextObjects;
using BankingFramework.Interceptor_Package.Dispatchers;
using NetCashATM.ATMHardware;
using NetCashATM.UserInterface.Panels;
using System;
using System.Configuration;

namespace NetCashATM.Presenters
{
    public class DepositPresenter
    {
        private DepositPanel _DepositPanel;
        private ATMFacade _atmFacade;
        private ATMCashManager _cashManager = new ATMCashManager();

        public DepositPresenter(DepositPanel depositPanel)
        {
            _DepositPanel = depositPanel;

            _atmFacade  = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
        }

        public void Deposit()
        {
            if (_DepositPanel.GetInput().Text != "")
            { 
                if (((Convert.ToInt32(_DepositPanel.GetInput().Text)) % 10) != 0)
                {
                    _DepositPanel.SetErrorMessage("THIS MACHINE DOES NOT ACCEPT ANY CURRECY LESS THAN €10");
                }
                else
                {
                    double amount = double.Parse(_DepositPanel.GetInput().Text);
                    _atmFacade.PerformDeposit(amount);
                    _cashManager.UpdateAmountDeposit(amount);

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
