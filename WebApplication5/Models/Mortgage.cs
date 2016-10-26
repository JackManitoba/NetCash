using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetCash.Models
{
    public class Mortgage : Loan
    {
        string Title = "Mortgage";

        [Required]
        [Display(Name = "Price Of House")]
        public string PriceOfHouse { get; set; }

        [Required]
        [Display(Name = "Size Of House")]
        [DataType(DataType.Time)]
        public string SizeOfHouse { get; set; }


    }
}