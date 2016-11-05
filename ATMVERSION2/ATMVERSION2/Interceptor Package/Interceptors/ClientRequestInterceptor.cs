using ATMVERSION2.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Interceptor_Package.Dispatchers;
using WindowsFormsApplication1.Interceptor_Package.Interceptors;

namespace WindowsFormsApplication1.Interceptor_Package
{
    public class ClientRequestInterceptor : Interceptor
    {
       // Logger logger = new Logger();
        internal void onDatabaseReadRequest(DataBaseReadRequest context)
        {
          //  logger.log(context);
            Debug.WriteLine(context.getObj());

        }

        internal void onDatabaseWriteRequest(DatabaseWriteRequest context)
        {
            //logger.log(context);
            Debug.WriteLine(context.getObj());
        }
    }
}
