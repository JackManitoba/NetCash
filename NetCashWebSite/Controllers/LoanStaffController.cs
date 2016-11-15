using System.Web.Mvc;
using NetCashWebSite.Models;
using BankingFramework.FacadeClasses;
using System.Collections.Generic;
using System;

namespace NetCashWebSite.Controllers
{
    public class LoanStaffController : Controller
    {
        [HttpGet]
        public ActionResult LoanStaffQuery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoanStaffQuery(Loan Loan)
        {
            WebSiteFacade webSiteFacade = new WebSiteFacade(Loan.AccountNumber);
            
            if(webSiteFacade.PendingApplicationExists())
            {
                webSiteFacade.GetLoanDataByAccountNumber();

                List<string> loanProperties = webSiteFacade.GetLoanDataByAccountNumber();

                Loan.LoanChoice = loanProperties[0];
                Loan.AmountRequired = loanProperties[1];
                Loan.PeriodOfRepayment = loanProperties[2];
                Loan.DateOfApplication = loanProperties[3];

                return View("DisplayLoan", Loan);
            }
            else
            {
                return View("ReviewLoanFailure", Loan);
            }
        }

        public ActionResult DiscussLoan(Loan Loan)
        {
            WebSiteFacade webSiteFacade = new WebSiteFacade(Loan.AccountNumber);
            webSiteFacade.MarkLoanAsDiscussed();

            return View();
        }
    }
}