using BankingFramework.Interceptor_Package;
using System;
using System.Diagnostics;
using System.IO;

namespace BankingFramework.Utils
{
    public class Logger
    {   
        internal void LogDatabaseInteractions(ContextObject context)
        {
            //file2.WriteLine(DateTime.Now.ToString("HH:mm:ss tt")+context.getObj());
            var path = (AppDomain.CurrentDomain.BaseDirectory);
           
            int position = path.IndexOf("NetCash");
            Debug.WriteLine("+++" + path + "  " + position);
            var substring = path.Substring(0, position);
            path = substring + "NetCash\\logs\\DatabaseInteractionsLog.txt";

            if (!File.Exists(path))
            {
                string createText = context.GetVerboseDescription() + Environment.NewLine;
                File.WriteAllText(path, createText);
            }
            else
            {
                string appendText = context.GetVerboseDescription() + Environment.NewLine;
                File.AppendAllText(path, appendText);
            }
        }

        internal void LogAccountTransactions(ContextObject context)
        {
            TransactionInfo transactionInfo = (TransactionInfo)context;

            var path = (AppDomain.CurrentDomain.BaseDirectory);
            int position = path.IndexOf("NetCash");
            var substring = path.Substring(0, position);
            path = substring + "NetCash\\logs\\TransactionsLog";

            path = path + transactionInfo.GetAccountNumber().Trim() + ".txt";
            if (!File.Exists(path))
            {
                string createText = context.GetVerboseDescription() + Environment.NewLine;
                File.WriteAllText(path, createText);
            }
            else
            {
                string appendText = context.GetVerboseDescription() + Environment.NewLine;
                File.AppendAllText(path, appendText);
            }
        }
    }
}

