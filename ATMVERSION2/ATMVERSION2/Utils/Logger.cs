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
        StreamWriter file2;
        
        public Logger()
        {
            file2 = File.AppendText("Log.txt");

        }

        internal void log(ContextObject context)
        {
            
            Debug.WriteLine("written to log file"+context.getObj());
            file2.WriteLine(DateTime.Now.ToString("HH:mm:ss tt")+context.getObj());
            
        }

       
    }
}
