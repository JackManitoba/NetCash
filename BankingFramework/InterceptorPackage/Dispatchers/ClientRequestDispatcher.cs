using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Interceptors;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BankingFramework.InterceptorPackage.Dispatchers
{
   public  class ClientRequestDispatcher : Dispatcher
    {
        protected readonly object _syncRoot = new Object();
        private static ClientRequestDispatcher _instanciatedObject;
        private List<Interceptor> _interceptorList;

        public ClientRequestDispatcher()
        {
            _interceptorList = new List<Interceptor>();
        }

        public void DispatchClientRequestInterceptorReadDatabaseRequest(DataBaseReadRequest context)
        {
            List<Interceptor> interceptors;

            // Clone vector.
            lock (_syncRoot)
            { 
                interceptors = (List<Interceptor>)_interceptorList; ;
            }

            for (int i = 0; i < interceptors.Count; ++i)
            {
                ClientRequestInterceptor ic = (ClientRequestInterceptor)interceptors[i];

                // Dispatch callback hook method.
                ic.OnDatabaseReadRequest(context);
            }
        }

        public void DispatchClientRequestInterceptorWriteDatabaseRequest(DatabaseWriteRequest context)
        {
            List<Interceptor> interceptors;

            // Clone vector.
            lock (_syncRoot)
            { 
                interceptors = (List<Interceptor>)_interceptorList; ;
            }

            for (int i = 0; i < interceptors.Count; ++i)
            {
                ClientRequestInterceptor ic = (ClientRequestInterceptor)interceptors[i];

                // Dispatch callback hook method.
                ic.OnDatabaseWriteRequest(context);
            }
        }

        public void DispatchClientRequestInterceptorTransactionAttempt(TransactionInfo context)
        {
            List<Interceptor> interceptors;

            // Clone vector.
            lock (_syncRoot)
            {
                interceptors = (List<Interceptor>)_interceptorList; ;
            }

            for (int i = 0; i < interceptors.Count; ++i)
            {
                ClientRequestInterceptor ic = (ClientRequestInterceptor)interceptors[i];

                // Dispatch callback hook method.
                ic.OnTransactionAttempted(context);
            }
        }

        public static ClientRequestDispatcher TheInstance()
        {
            if (_instanciatedObject == null)
            {
                Debug.WriteLine("Creating new ClientRequestDispatcher");

                //creating new Instanciated ClientRequestDispatcher
                _instanciatedObject = new ClientRequestDispatcher();
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
