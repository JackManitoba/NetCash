using Helpers.Interceptor_Package;
using Helpers.Interceptor_Package.Dispatchers;
using Helpers.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.BankTransactions
{
   public class Transfer : Transaction
    {
        AccountManager.Account IncomingTransferAccount;
        AccountManager.Account OutgoingTransferAccount;

        public Transfer(string accountNumber, string targetAccountNumber, string description, double amount)
        {
            this.CurrentAccountNumber = accountNumber;
            this.TargetAccountNumber = targetAccountNumber;
            this.TransferAmount = amount;

            IncomingTransferAccount = new AccountManager.Account(targetAccountNumber);
            OutgoingTransferAccount = new AccountManager.Account(accountNumber);

        }

        public string TargetAccountNumber { get; set; }

        public double TransferAmount { get; set; }

        public string CurrentAccountNumber { get; set; }

        public string type()
        {
            return "Transfer";
        }

        public int amount()
        {
            return Convert.ToInt32(TransferAmount);
        }

        public void PerformTransaction()
        {
            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(OutgoingTransferAccount, "Transfer from "+IncomingTransferAccount.AccountNumber, Convert.ToInt32(TransferAmount)));
            IncomingTransferAccount.IncreaseBalance(TransferAmount);
           



            ClientRequestDispatcher.theInstance().dispatchClientRequestInterceptorTransactionAttempt(new TransactionInfo(IncomingTransferAccount, "Transfer to " + OutgoingTransferAccount.AccountNumber, Convert.ToInt32(TransferAmount)));
            OutgoingTransferAccount.DecreaseBalance(TransferAmount);

            DatabaseManager.getInstance().addTransactionToDatabase(this);
        }

        public bool AreFundsAvailable()
        {
            return OutgoingTransferAccount.AreFundsAvailable(TransferAmount);
        }

        public string sourceAccount()
        {
            return this.IncomingTransferAccount.AccountNumber;
        }

        public string targetAccount()
        {
            return this.OutgoingTransferAccount.AccountNumber;
        }
    }
}
