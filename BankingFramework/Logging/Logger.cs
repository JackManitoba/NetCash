using BankingFramework.InterceptorPackage.ContextObjects;
using System;
using System.IO;

namespace BankingFramework.Logging
{
    public class Logger
    {   
        internal void LogDatabaseInteractions(string contextInfo)
        {
            var path = (AppDomain.CurrentDomain.BaseDirectory);
           
            int position = path.IndexOf("NetCash");
            var substring = path.Substring(0, position);
            path = substring + "NetCash\\logs\\DatabaseInteractionsLog.txt";

            if (!File.Exists(path))
            {
                string createText = contextInfo + Environment.NewLine;
                File.WriteAllText(path, createText);
            }
            else
            {
                string appendText = contextInfo + Environment.NewLine;
                File.AppendAllText(path, appendText);
            }
        }

        internal void LogAccountTransactions(string accountNumber, string contextInfo)
        {
           

            var path = (AppDomain.CurrentDomain.BaseDirectory);
            int position = path.IndexOf("NetCash");
            var substring = path.Substring(0, position);
            path = substring + "NetCash\\logs\\TransactionsLog";

            path = path + accountNumber.Trim() + ".txt";
            if (!File.Exists(path))
            {
                string createText = contextInfo + Environment.NewLine;
                File.WriteAllText(path, createText);
            }
            else
            {
                string appendText = contextInfo + Environment.NewLine;
                File.AppendAllText(path, appendText);
            }
        }
    }
}

