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

        public Transfer()
        {

        }

        [Required]
        [Display(Name = "Target Account Number:")]
        public string TargetAccountNumber { get; set; }

        [Required]
        [Display(Name = "Transfer Amount:")]
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
