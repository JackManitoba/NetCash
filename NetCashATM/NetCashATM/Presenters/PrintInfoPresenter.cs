using BankingFramework.FacadeClasses;
using BankingFramework.Interceptor_Package.ContextObjects;
using BankingFramework.Interceptor_Package.Dispatchers;
using NetCashATM.Views.UserInterface.Panels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
