using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetCash.Controllers
{
    public class LoanController : Controller
    {
        Models.Loan Loan;
        string Choice;
        // GET: Loan
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoanApplication()
        {
            return View(new Models.LoanSelector());
        }

        [HttpPost]
        public ActionResult LoanApplication(Models.LoanSelector LoanSelector)
        {
            Choice = LoanSelector.LoanChoice;

            if (Choice == "0")
                return RedirectToAction("CarLoanForm");
            else if (Choice == "1")
                return RedirectToAction("MortgageForm");
            else return RedirectToAction("MortgageForm");
        }
       
        public ActionResult CarLoanForm()
        {
            return View();
        }

        public ActionResult MortgageForm()
        {
            return View();
        }
    }
}