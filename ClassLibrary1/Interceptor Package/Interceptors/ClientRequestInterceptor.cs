﻿
using Helpers.Interceptor_Package.Interceptors;
using Helpers.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Helpers.Interceptor_Package
{
    public class ClientRequestInterceptor : Interceptor
    {
       Logger logger = new Logger();
        internal void onDatabaseReadRequest(DataBaseReadRequest context)
        {
          logger.logDatabaseInteractions(context);
            Debug.WriteLine(context.getVerboseDescription());

        }

        internal void onDatabaseWriteRequest(DatabaseWriteRequest context)
        {
            logger.logDatabaseInteractions(context);
            
        }

        internal void onTransactionAttempted(TransactionInfo context)
        {
            logger.logAccountTransactions(context);
           

        }
    }
}