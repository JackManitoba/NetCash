
using BankingFramework.Interceptor_Package.Interceptors;
using BankingFramework.Utils;
using System.Diagnostics;



namespace BankingFramework.Interceptor_Package
{
    public class ClientRequestInterceptor : Interceptor
    {
        private Logger _logger = new Logger();

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
