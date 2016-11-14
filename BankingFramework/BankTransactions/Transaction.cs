
namespace BankingFramework.BankTransactions
{
   public interface Transaction
    {
        string GetType();
        int GetAmount();

        void PerformTransaction();
        bool AreFundsAvailable();

    }
}
