using BankingFramework.AccountManager;
using BankingFramework.DatabaseManagement;
using BankingFramework.InterceptorPackage.ContextObjects;
using BankingFramework.InterceptorPackage.Dispatchers;
using System;

namespace BankingFramework.BankTransactions
{
    public class Transfer : Transaction
    {
        private  Account _incomingTransferAccount;
        private  Account _outgoingTransferAccount;

        public string TargetAccountNumber { get; set; }

        public double TransferAmount { get; set; }

        public string CurrentAccountNumber { get; set; }


        public Transfer(string accountNumber, string targetAccountNumber, string description, double amount)
        {
            CurrentAccountNumber = accountNumber;
            TargetAccountNumber = targetAccountNumber;
            TransferAmount = amount;

            _incomingTransferAccount = new Account(targetAccountNumber);
            _outgoingTransferAccount = new Account(accountNumber);
        }

        public new string GetType()
        {
            return "Transfer";
        }

        public int GetAmount()
        {
            return Convert.ToInt32(TransferAmount);
        }

        public void PerformTransaction()
        {
            ClientRequestDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(_outgoingTransferAccount, "Transfer to "+_incomingTransferAccount.AccountNumber, Convert.ToInt32(TransferAmount)));
            _incomingTransferAccount.IncreaseBalance(TransferAmount);

            ClientRequestDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(_incomingTransferAccount, "Transfer from " + _outgoingTransferAccount.AccountNumber, Convert.ToInt32(TransferAmount)));
            _outgoingTransferAccount.DecreaseBalance(TransferAmount);
            DatabaseManager.GetInstance().AddTransactionToDatabase(_outgoingTransferAccount.AccountNumber, _incomingTransferAccount.AccountNumber, GetType(), TransferAmount);
        }

        public bool AreFundsAvailable()
        {
            return _outgoingTransferAccount.AreFundsAvailable(TransferAmount);
        }
    }
}
