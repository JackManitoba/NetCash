using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetCash.Models
{
    public class LoanFactory
    {
        static public Loan CreateAndReturnLoan(int Choice)
        {
            Loan LoanSelector = null;

            switch(Choice)
            {
                case 0:
                    LoanSelector = new Mortgage();
                    break;

                case 1:
                    LoanSelector = new CarLoan();
                    break;
            }

            return LoanSelector;
        }
    }
}