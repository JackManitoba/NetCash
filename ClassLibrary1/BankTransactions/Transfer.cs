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
    }
}
