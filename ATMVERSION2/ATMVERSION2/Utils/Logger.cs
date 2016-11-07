using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Interceptor_Package;

namespace ATMVERSION2.Utils
{
    class Logger
    {
        
        
        public Logger()
        {

        }

        internal void logDatabaseInteractions(ContextObject context)
        {
          
            
            //file2.WriteLine(DateTime.Now.ToString("HH:mm:ss tt")+context.getObj());
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"DatabaseInteractionsLog.txt", true))
            {
                file.WriteLine(context.getVerboseDescription()+"\n");
                
            }
        }

        internal void logAccountTransactions(ContextObject context)
        {
            using (System.IO.StreamWriter file =
              new System.IO.StreamWriter(@"TransactionsLog.txt", true))
            {
                file.WriteLine(context.getVerboseDescription() + "\n");
                
            }

        }
    }

       
    }

