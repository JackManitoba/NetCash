
namespace BankingFramework.BankTransactions
{
   public interface Transaction
    {
        string GetType();
        double GetAmount();

        void PerformTransaction();
        bool AreFundsAvailable();

    }
}
