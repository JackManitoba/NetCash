using BankingFramework.FacadeClasses;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace NetCashATM.Presenters
{
    public class  PrintInfoPresenter
    {
        private ATMFacade _atmFacade;

        public PrintInfoPresenter()
        {    
            _atmFacade = new ATMFacade(ConfigurationManager.AppSettings["CardNumber"]);
            PrintStatement();
        }

        public void PrintStatement()
        {
            var path = (AppDomain.CurrentDomain.BaseDirectory);
            int position = path.IndexOf("NetCash");
            var substring = path.Substring(0, position);

            List<List<string>> transactionlist = _atmFacade.GetStatement();
            path = substring + "NetCash\\logs\\TransactionsLog" + _atmFacade.GetAccountNumber().Trim() + ".txt";
            
            string Filename = @path;
            if (!File.Exists(path))
            {
                string createText = generateStatementText(transactionlist);
                File.WriteAllText(path, createText);
            }
            else
            {
                string createText = generateStatementText(transactionlist); 
                File.WriteAllText(path, createText);
            }

            Process.Start("notepad.exe", Filename);         
        }

        private string generateStatementText(List<List<string>> transactionlist)
        {
            string createText = "";
                   createText += "Outgoing||";
                   createText += "Incoming||";
                   createText += "Type\t||";
                   createText += "Amount||";
                   createText += "Date\t\t||";
                   createText += "Balance" + Environment.NewLine;
                   createText += "------------------------------------------------------------------" + Environment.NewLine;
            for (int i = 0; i < transactionlist.Count; i++)
            {
                for (int j = 0; j < transactionlist[i].Count; j++)
                {
                    if (j == 0)
                    {
                        if (transactionlist[i][j].Trim() == "")
                            createText += "\t||";
                        else
                            createText += transactionlist[i][j].Trim() + "||";
                    }
                    else if (j == 1)
                    {
                        if (transactionlist[i][j].Trim() == "")
                            createText += "\t  ||";
                        else
                            createText += transactionlist[i][j].Trim() + "||";
                    }
                    else
                        createText += transactionlist[i][j].Trim() + "\t||";
                }
                createText += Environment.NewLine;
            }
            return createText;
        }

        internal void GoToMainMenu()
        {
            NavigationRequestDispatcher.TheInstance()
                .DispatchNavigationRequestInterceptors(new NavigationContextObject("MainPanel"));
        }
    }
}
