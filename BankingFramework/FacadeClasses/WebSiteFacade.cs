using BankingFramework.AccountManager;
using BankingFramework.BankTransactions;

namespace BankingFramework.FacadeClasses
{
    public class WebSiteFacade
    {
        private Account _account;

        public WebSiteFacade(string accountNumber)
        {
            _account = new Account(accountNumber);
        }

        public bool AreFundsAvailable(double transferAmount)
        {
            return _account.AreFundsAvailable(transferAmount);
        }

        public void PerformTransaction(string targetAccountNumber, double transferAmount)
        {   
            Transaction transfer = new Transfer(_account.AccountNumber, targetAccountNumber, "TRANSFER", transferAmount);
            transfer.PerformTransaction();
        }
    }
}
