using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetCashWebSite.Models
{
    public class Transfer
    {
        [Required]
        [Display(Name = "Target Account Number:")]
        public string TargetAccountNumber { get; set; }

        [Required]
        [Display(Name = "Transfer Amount:")]
        public double TransferAmount { get; set; }

        public string CurrentAccountNumber { get; set; }

    }
}