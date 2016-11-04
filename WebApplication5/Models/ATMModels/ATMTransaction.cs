using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication5.Models.ATMModels
{
    public class ATMTransaction
    {
        [Required]
        public string accountNumber { get; set; }

        [Required]
        public string type { get; set; }

        [Required]
        public int amount { get; set; }

        public ATMTransaction(string accNo, string t, double a)
        {
           accountNumber = accNo;
            type = t;
           amount = Convert.ToInt32(a);
        }
    }
}