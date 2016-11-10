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
            IncomingTransferAccount.IncreaseBalance(TransferAmount);
            OutgoingTransferAccount.DecreaseBalance(TransferAmount);
        }

        public bool AreFundsAvailable()
        {
            return OutgoingTransferAccount.AreFundsAvailable(TransferAmount);
        }
    }
}
