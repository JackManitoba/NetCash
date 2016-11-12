using BankingFramework.AccountManager;
using BankingFramework.Interceptor_Package;
using BankingFramework.Interceptor_Package.Dispatchers;
using System;

namespace BankingFramework.BankTransactions
{
   public class Transfer : Transaction
    {
        Account IncomingTransferAccount;
        Account OutgoingTransferAccount;

        public string TargetAccountNumber { get; set; }

        public double TransferAmount { get; set; }

        public string CurrentAccountNumber { get; set; }


        public Transfer(string accountNumber, string targetAccountNumber, string description, double amount)
        {
            CurrentAccountNumber = accountNumber;
            TargetAccountNumber = targetAccountNumber;
            TransferAmount = amount;

            IncomingTransferAccount = new Account(targetAccountNumber);
            OutgoingTransferAccount = new Account(accountNumber);

        }

        public string GetType()
        {
            return "Transfer";
        }

        public int GetAmount()
        {
            return Convert.ToInt32(TransferAmount);
        }

        public void PerformTransaction()
        {
            ClientRequestDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(OutgoingTransferAccount, "Transfer to "+IncomingTransferAccount.AccountNumber, Convert.ToInt32(TransferAmount)));
            IncomingTransferAccount.IncreaseBalance(TransferAmount);

            ClientRequestDispatcher.TheInstance().DispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(IncomingTransferAccount, "Transfer to " + OutgoingTransferAccount.AccountNumber, Convert.ToInt32(TransferAmount)));
            OutgoingTransferAccount.DecreaseBalance(TransferAmount);
        }

        public bool AreFundsAvailable()
        {
            return OutgoingTransferAccount.AreFundsAvailable(TransferAmount);
        }
    }
}
