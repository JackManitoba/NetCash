using BankingFramework.FacadeClasses;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using NetCashATM.UserInterface.Panels;
using System;
using System.Configuration;
using System.Diagnostics;

namespace NetCashATM.Presenters
{
    public class  PrintInfoPresenter
    {
        private PrintInfo _printInfo;
        private ATMFacade _atmFacade;

        public PrintInfoPresenter(PrintInfo printInfo)
        {
            _printInfo = printInfo;
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
        }

        public void PrintStatement()
        {
            var path = (AppDomain.CurrentDomain.BaseDirectory);
            int position = path.IndexOf("NetCash");
            var substring = path.Substring(0, position);
            path = substring + "NetCash\\logs\\TransactionsLog" + _atmFacade.GetAccountNumber().Trim() + ".txt";

            string Filename = @path;
            Process.Start("notepad.exe", Filename);
            _printInfo.SetMessage("Printing Complete, press enter to continue");
        }

        internal void GoToMainMenu()
        {
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject("MainPanel"));
        }
    }
}
