using System;
using BankingFramework.AccountManager;
using BankingFramework.BankTransactions;
using System.Collections.Generic;
using BankingFramework.Statements;
using BankingFramework.DatabaseManagement;

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
            Transaction transfer = new Transfer(_account.GetAccountNumber(), targetAccountNumber, "TRANSFER", transferAmount);
            transfer.PerformTransaction();
        }

        public List<List<string>> GetStatement()
        {
            Statement statement = new Statement(_account.GetAccountNumber());
            return statement.getListOfTransactions();
        }

        public bool PendingApplicationExists()
        {
            return DatabaseManager.GetInstance().PendingApplicationExists(_account.GetAccountNumber());
        }

        public List<string> GetLoanDataByAccountNumber()
        {
            return DatabaseManager.GetInstance().GetLoanDataByAccountNumber(_account.GetAccountNumber());
        }

        public bool PendingInsuranceQueryExists()
        {
            return DatabaseManager.GetInstance().DoesPendingQueryExist(_account.GetAccountNumber());
        }

        public void SubmitInsuranceQuery(string insuranceType, string ageBracket, string location)
        {
            DatabaseManager.GetInstance().SubmitInsuranceQuery(_account.GetAccountNumber(), insuranceType, ageBracket, location);
        }

        public List<string> GetInsuranceByAccountNumber()
        {
            return DatabaseManager.GetInstance().GetInsuranceByAccountNumber(_account.GetAccountNumber());
        }

        public void MarkInsuranceAsDiscussed()
        {
            DatabaseManager.GetInstance().MarkInsuranceAsDiscussed(_account.GetAccountNumber());
        }

        public void MarkLoanAsDiscussed()
        {
            DatabaseManager.GetInstance().MarkLoanAsDiscussed(_account.GetAccountNumber());
        }

        public void SubmitLoanApplication(string loanType, string amountRequired, string repaymentPeriod)
        {
            DatabaseManager.GetInstance().SubmitLoanApplication(_account.GetAccountNumber(), loanType, amountRequired, repaymentPeriod);
        }
    }
}
