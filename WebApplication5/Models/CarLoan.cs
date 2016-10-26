using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetCash.Models
{
    public class CarLoan : Loan
    {
        string Title = "Car Loan";

        [Required]
        [Display(Name = "Price Of Car")]
        public string PriceOfCar { get; set; }

        [Required]
        [Display(Name = "Type Of Car")]
        public string TypeOfCar { get; set; }
    }
}