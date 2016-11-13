using BankingFramework.Interceptor_Package;
using BankingFramework.Interceptor_Package.ContextObjects;
using BankingFramework.Interceptor_Package.Interceptors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingFramework.Interceptor_Package.Dispatchers
{
   public class NavigationRequestDispatcher : Dispatcher
    {
        protected readonly object _syncRoot = new Object();
        private static NavigationRequestDispatcher _instanciatedObject;
        private List<Interceptor> _interceptorList;

        public NavigationRequestDispatcher()
        {
            _interceptorList = new List<Interceptor>();
        }

        public void DispatchNavigationRequestInterceptors(NavigationContextObject context)
        {
            List<Interceptor> interceptors;

            // Clone vector.
            lock (_syncRoot)
            { 
                interceptors = (List<Interceptor>)_interceptorList; ;
            }

            for (int i = 0; i < interceptors.Count; ++i)
            {
                Interceptor ic = interceptors[i];

                // Dispatch callback hook method.
                ic.baseFunction(context);
            }
        }

      
        public static NavigationRequestDispatcher TheInstance()
        {
            if (_instanciatedObject == null)
            {
                Debug.WriteLine("Creating new NavigationRequestDispatcher");

                //creating new Instanciated ClientRequestDispatcher
                _instanciatedObject = new NavigationRequestDispatcher();
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
