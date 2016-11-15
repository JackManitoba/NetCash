using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Interceptors;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BankingFramework.InterceptorPackage.Dispatchers
{
   public  class LoggingInfoDispatcher : Dispatcher
    {
        private readonly object _syncRoot = new Object();
        private static LoggingInfoDispatcher _instanciatedObject;
        private List<Interceptor> _interceptorList;

        public LoggingInfoDispatcher()
        {
            _interceptorList = new List<Interceptor>();
        }

        public void DispatchClientRequestInterceptorReadDatabaseRequest(DataBaseReadContextObject context)
        {
            List<Interceptor> interceptors;

            // Clone vector.
            lock (_syncRoot)
            { 
                interceptors = (List<Interceptor>)_interceptorList; ;
            }

            for (int i = 0; i < interceptors.Count; ++i)
            {
                LoggingInfoInterceptor ic = (LoggingInfoInterceptor)interceptors[i];

                // Dispatch callback hook method.
                ic.ConsumeService(context);
            }
        }

        public void DispatchClientRequestInterceptorWriteDatabaseRequest(DatabaseWriteContextObject context)
        {
            List<Interceptor> interceptors;

            // Clone vector.
            lock (_syncRoot)
            { 
                interceptors = (List<Interceptor>)_interceptorList; ;
            }

            for (int i = 0; i < interceptors.Count; ++i)
            {
                LoggingInfoInterceptor ic = (LoggingInfoInterceptor)interceptors[i];

                // Dispatch callback hook method.
                ic.ConsumeService(context);
            }
        }

        public void DispatchClientRequestInterceptorTransactionAttempt(TransactionInfoContextObject context)
        {
            List<Interceptor> interceptors;

            // Clone vector.
            lock (_syncRoot)
            {
                interceptors = (List<Interceptor>)_interceptorList; ;
            }

            for (int i = 0; i < interceptors.Count; ++i)
            {
                LoggingInfoInterceptor ic = (LoggingInfoInterceptor)interceptors[i];

                // Dispatch callback hook method.
                ic.ConsumeService(context);
            }
        }

        public static LoggingInfoDispatcher TheInstance()
        {
            if (_instanciatedObject == null)
            {
                Debug.WriteLine("Creating new ClientRequestDispatcher");

                //creating new Instanciated ClientRequestDispatcher
                _instanciatedObject = new LoggingInfoDispatcher();
                return _instanciatedObject;
            }
            else
            {
                //returning previously made instanciated ClientRequestDispatcher
                return _instanciatedObject;
            }
        }

        public void RegisterClientInterceptor(Interceptor i)
        {   
            //adding clientInterceptor
            _interceptorList.Add(i);
        }

        public void UnregisterClientInterceptor(Interceptor i)
        {   
            //removing clientInterceptor
            _interceptorList.Remove(i);
        }
    }
}
