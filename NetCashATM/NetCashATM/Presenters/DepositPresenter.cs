using BankingFramework.Interceptor_Package.ContextObjects;
using BankingFramework.Interceptor_Package.Dispatchers;
using NetCashATM.UserInterface.Panels;
using System;

namespace NetCashATM.Presenters
{
    public class DepositPresenter
    {
        private DepositPanel _DepositPanel;

        public DepositPresenter(DepositPanel depositPanel)
        {
            _DepositPanel = depositPanel;
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
