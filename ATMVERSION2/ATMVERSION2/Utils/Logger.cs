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
            //file2 = File.AppendText("Log.txt");

        }

        internal void log(ContextObject context)
        {

            File.AppendAllText("/Log_Test.txt", context.getVerboseDescription() + Environment.NewLine);
            

        }

        }

       
    }
