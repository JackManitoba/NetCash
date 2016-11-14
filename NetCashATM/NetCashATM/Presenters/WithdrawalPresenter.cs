﻿using BankingFramework.FacadeClasses;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using NetCashATM.ATMHardware;
using NetCashATM.UserInterface.Panels;
using System;
using System.Configuration;

namespace NetCashATM.Presenters
{
    public  class WithdrawalPresenter
    {
        private WithdrawalPanel _WithdrawalPanel;
        private ATMFacade _atmFacade;
        private ATMCashManager _cashManager;

        public WithdrawalPresenter(WithdrawalPanel depositPanel)
        {
            _WithdrawalPanel = depositPanel;

            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
        }

        public void Withdraw()
        {
            if (_WithdrawalPanel.GetInput().Text != "")
            {
                if (((Convert.ToInt32(_WithdrawalPanel.GetInput().Text)) % 10) != 0)
                {
                    _WithdrawalPanel.SetErrorMessage("THIS MACHINE DOES DISPENSE NOTES LESS THAN €10");
                }
                else
                {
                    double amount = double.Parse(_WithdrawalPanel.GetInput().Text);
                    _cashManager = new ATMCashManager();

                    if (_atmFacade.AreFundsAvailable(amount) && _cashManager.IsWithdrawable(amount))
                    {
                        _atmFacade.PerformWithdraw(amount);
                        _cashManager.UpdateAmountWithdrawal(amount);

                        LogOut();
                    }
                    else
                    _WithdrawalPanel.SetErrorMessage("THE WITHDRAWAL WAS UNSUCCESSFUL");

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