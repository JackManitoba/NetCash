using BankingFramework.Utils;
using System.Diagnostics;
using BankingFramework.InterceptorPackage.ContextObjects;

namespace BankingFramework.InterceptorPackage.Interceptors
{
    public class ClientRequestInterceptor : Interceptor
    {
        private Logger _logger = new Logger();

        public void baseFunction(ContextObject e)
        {
            Debug.WriteLine(e.ToString());
        }

        internal void OnDatabaseReadRequest(DataBaseReadRequest context)
        {
            _logger.LogDatabaseInteractions(context);
            Debug.WriteLine(context.GetVerboseDescription());
        }

        internal void OnDatabaseWriteRequest(DatabaseWriteRequest context)
        {
            _logger.LogDatabaseInteractions(context);     
        }

        internal void OnTransactionAttempted(TransactionInfo context)
        {
            
            _logger.LogAccountTransactions(context);         
        }
    }
}
