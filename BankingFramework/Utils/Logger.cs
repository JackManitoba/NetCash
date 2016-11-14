using BankingFramework.InterceptorPackage.ContextObjects;
using System;
using System.IO;

namespace BankingFramework.Utils
{
    public class Logger
    {   
        internal void LogDatabaseInteractions(string contextInfo)
        {
            //file2.WriteLine(DateTime.Now.ToString("HH:mm:ss tt")+context.getObj());
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

