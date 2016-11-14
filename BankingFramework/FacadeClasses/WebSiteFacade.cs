using System;
using BankingFramework.AccountManager;
using BankingFramework.BankTransactions;
using System.Collections.Generic;
using BankingFramework.Statements;

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

        public List<List<string>> GetStatement()
        {
            Statement statement = new Statement(_account.AccountNumber);

            return statement.getListOfTransactions();
        }
    }
}
