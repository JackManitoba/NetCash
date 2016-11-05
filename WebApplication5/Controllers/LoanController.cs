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
        // GET: Loan
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoanApplication()
        {
            return View(new Models.Loan());
        }

        [HttpPost]
        public ActionResult LoanApplication(Models.Loan LoanApplication)
        {
            LoanApplication.SubmitApplication(Session["AccountNumber"]);
            return RedirectToAction("LoanResult", LoanApplication);
        }

        public ActionResult LoanResult(Models.Loan LoanResult)
        {
            return View(LoanResult);
        }
       
    }
}